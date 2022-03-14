﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using RPA.Report;

namespace RPA
{
    internal class AutomationProvider
    {
        private IReport _report;
        private string _findingTemplate = "Finding {0}";
        private string _foundTemplate = "Found '{0}' ({1})";
        private RetryHelper _retryHelper;

        internal AutomationProvider(IReport report)
        {
            _report = report;
            _retryHelper = new RetryHelper(_report);
        }

        #region AutomationElement

        internal AutomationElement GetRootAutomationElement(ExectutionContext context)
        {
            AutomationElement root = null;

            return _retryHelper.Retry(() =>
            {
                _report?.WriteLine(string.Format(_findingTemplate, $"{nameof(ExectutionContext.ExecutableFilePath)}:'{context.ExecutableFilePath}'"));
                root = FromFilePath(context);

                if (root == null)
                {
                    _report?.WriteLine(string.Format(_findingTemplate, $"{nameof(ExectutionContext.MainWindowTitle)}:'{context.MainWindowTitle}'"));
                    root = FromMainWindowTitle(context);
                }
                return root;
            }, new CancellationToken());
        }

        internal AutomationElement FindChildAutomationElement(Locator locator, int timeoutMilliseconds = 0)
        {
            return FindChildAutomationElement(AutomationElement.RootElement, new PropertyCondition(locator.Type, locator.Value), timeoutMilliseconds);
        }

        internal AutomationElement FindChildAutomationElement(AutomationElement automationElement, Locator locator, int timeoutMilliseconds = 0)
        {
            return FindChildAutomationElement(automationElement, LocatorToCondition(locator), timeoutMilliseconds);
        }

        internal AutomationElement FindChildAutomationElement(AutomationElement automationElement, int position)
        {
            var walker = new TreeWalker(Condition.TrueCondition);

            var firstChild = walker.GetFirstChild(automationElement);

            if (position > 1)
            {
                return FindSiblingAutomationElement(automationElement, position);
            }
            else
            {
                return firstChild;
            }
        }

        internal AutomationElement FindSiblingAutomationElement(AutomationElement automationElement, int position)
        {
            var walker = new TreeWalker(Condition.TrueCondition);

            AutomationElement element = automationElement;

            for (int i = 0; i < position; i++)
            {
                element = walker.GetNextSibling(element);
            }
            return element;
        }

        #endregion


        #region Pattern

        internal void SetValue(AutomationElement automationElement, Locator locator, string value)
        {
            var pattern = GetCurrentPattern(automationElement, locator, ValuePattern.Pattern) as ValuePattern;
            SetValue(pattern, value);
        }

        internal void SetValue(AutomationElement automationElement, string value)
        {
            var pattern = GetCurrentPattern(automationElement, ValuePattern.Pattern) as ValuePattern;
            SetValue(pattern, value);
        }

        internal void Invoke(AutomationElement automationElement, Locator locator)
        {
            var pattern = GetCurrentPattern(automationElement, locator, InvokePattern.Pattern) as InvokePattern;
            Invoke(pattern, locator);
        }

        internal void Select(AutomationElement automationElement, params Locator[] locators)
        {
            var condition = new AndCondition(locators.Select(_ => new PropertyCondition(_.Type, _.Value)).ToArray());
            var pattern = GetCurrentPattern(automationElement, condition, SelectionItemPattern.Pattern) as SelectionItemPattern;
            Select(pattern);
        }

        internal void Select(AutomationElement automationElement, Locator locator)
        {
            var condition = LocatorToCondition(locator);
            var pattern = GetCurrentPattern(automationElement, condition, SelectionItemPattern.Pattern) as SelectionItemPattern;
            Select(pattern);
        }

        internal void Toggle(AutomationElement automationElement)
        {
            var pattern = GetCurrentPattern(automationElement, TogglePattern.Pattern) as TogglePattern;
            Toggle(pattern);
        }

        #endregion


        #region private 

        private void Select(SelectionItemPattern selectionItemPattern)
        {
            selectionItemPattern.Select();
            _report.WriteLine($"Select '{selectionItemPattern.Current.SelectionContainer.Current.AutomationId}'");
        }

        private void Toggle(TogglePattern togglePattern)
        {
            togglePattern.Toggle();
            _report.WriteLine($"Toggle '{togglePattern.Current.ToString()}'");
        }

        private void Invoke(InvokePattern invokePattern, Locator locator)
        {
            invokePattern.Invoke();
            _report?.WriteLine($"'{locator}' clicked");
        }

        private void SetValue(ValuePattern valuePattern, string value)
        {
            valuePattern.SetValue(value);
            _report?.WriteLine($"{nameof(SetValue)} '{value}'");
        }

        private BasePattern GetCurrentPattern(AutomationElement automationElement, Locator locator, AutomationPattern pattern)
        {
            return GetCurrentPattern(automationElement, LocatorToCondition(locator), pattern);
        }

        private BasePattern GetCurrentPattern(AutomationElement automationElement, Condition condition, AutomationPattern pattern)
        {
            var elementResult = FindChildAutomationElement(automationElement, condition);
            return GetCurrentPattern(elementResult, pattern);
        }

        private BasePattern GetCurrentPattern(AutomationElement automationElement, AutomationPattern pattern)
        {
            BasePattern patternResult = null;
            try
            {
                return _retryHelper.Retry(() => automationElement.GetCurrentPattern(pattern) as BasePattern, new CancellationToken());
            }
            finally
            {
                if (patternResult != null)
                {
                    _report.WriteLine(string.Format(_foundTemplate, patternResult.GetType().Name, automationElement.Current.Name));
                }
            }
        }

        private AutomationElement FromMainWindowTitle(ExectutionContext context)
        {
            var allProcesses = Process.GetProcesses();
            AutomationElement root = null;
            foreach (var item in allProcesses)
            {
                try
                {
                    if (item.MainWindowTitle.Contains(context.MainWindowTitle))
                    {
                        root = AutomationElement.FromHandle(item.MainWindowHandle);
                        break;
                    }
                }
                catch (Exception) { }
            }
            if (Check(root) == false)
            {
                root = null;
            }

            if (root != null)
            {
                _report.WriteLine(string.Format(_foundTemplate, root.Current.Name, $"{nameof(context.MainWindowTitle)} {context.MainWindowTitle}"));
            }

            return root;
        }

        private AutomationElement FromFilePath(ExectutionContext context)
        {
            AutomationElement root = null;
            var allProcesses = Process.GetProcesses();
            var map = allProcesses.Select(_ => _.MainWindowTitle).ToList();
            foreach (var item in allProcesses)
            {
                try
                {
                    if (item.MainModule.FileName == context.ExecutableFilePath)
                    {
                        root = AutomationElement.FromHandle(item.MainWindowHandle);
                        break;
                    }
                }
                catch (Exception) { }
            }
            if (Check(root) == false)
            {
                root = null;
            }

            if (root != null)
            {
                _report.WriteLine(string.Format(_foundTemplate, root.Current.Name, $"{nameof(context.ExecutableFilePath)} {context.ExecutableFilePath}"));
            }

            return root;
        }

        private AutomationElement FindChildAutomationElement(AutomationElement automationElement, Condition condition, int timeoutMilliseconds = 0)
        {
            var reportInfo = "";

            switch (condition)
            {
                case PropertyCondition propertyCondition:
                    reportInfo = $"{propertyCondition.Property.ProgrammaticName.Replace("AutomationElementIdentifiers.", "") }: {propertyCondition.Value}";
                    break;
                case AndCondition andCondition:
                    break;
            }

            AutomationElement elementResult = null;
            try
            {
                var cts = new CancellationTokenSource();
                var taskTimeout = Task.Run(() =>
                {
                    Task.Delay(timeoutMilliseconds == 0 ? Timeout.Infinite : timeoutMilliseconds).Wait();
                    cts.Cancel();
                    _report.WriteLine($"Timeout {timeoutMilliseconds}ms expired.");
                });

                elementResult = _retryHelper.Retry(() =>
                {
                    AutomationElement foundElement = null;
                    OnSafe(() =>
                    {
                        var taskResult = Task.Run(() => { foundElement = automationElement.FindFirst(TreeScope.Descendants, condition); });
                        Task.WaitAny(new Task[] { taskResult, taskTimeout });
                    });

                    return foundElement;
                }, cts.Token);

                return elementResult;
            }
            finally
            {
                if (elementResult != null)
                {
                    var elementName = "";
                    OnSafe(() =>
                    {
                        elementName = elementResult.Current.Name;
                    });

                    _report.WriteLine(string.Format(_foundTemplate, elementName, reportInfo));
                }
            }
        }

        private AutomationElement FindInSubtree(AutomationElement automationElement, int deep)
        {
            var walker = new TreeWalker(Condition.TrueCondition);

            AutomationElement element = automationElement;


            for (int i = 0; i < deep; i++)
            {
                element = walker.GetFirstChild(element);
            }
            return element;
        }

        private bool Check(AutomationElement root = null)
        {
            var result = false;
            try
            {
                if (root != null)
                {
                    var name = root.Current.Name;
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private void OnSafe(Action toDo)
        {
            try
            {
                toDo?.Invoke();
            }
            catch (Exception ex)
            {
                _report.WriteLine(ex);
            }
        }


        private Condition LocatorToCondition(Locator locator)
        {
            Condition result = new PropertyCondition(locator.Type, locator.Value);

            if (locator.GetAndLocators().Any())
            {
                var list = locator.GetAndLocators().Select(_ => new PropertyCondition(_.Type, _.Value) as Condition).ToList();
                list.Insert(0, result);
                result = new AndCondition(list.ToArray());
            }


            if (locator.GetOrLocators().Any())
            {
                var list = locator.GetOrLocators().Select(_ => new PropertyCondition(_.Type, _.Value) as Condition).ToList();
                list.Insert(0, result);
                result = new OrCondition(list.ToArray());
            }

            return result;
        }

        #endregion
    }
}
