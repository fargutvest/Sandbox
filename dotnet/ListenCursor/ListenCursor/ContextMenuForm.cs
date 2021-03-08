using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Forms;

namespace ListenCursor
{
    public partial class ContextMenuForm : Form
    {
        private int _itemHeight = 20;
        private int _itemIndent = 10;

        public ContextMenuResult Result { get; }

        public ContextMenuForm()
        {
            InitializeComponent();
            Result = new ContextMenuResult();
        }

        public void Fill(int x, int y)
        {
            Location = new Point(x, y);

            var uiElement = AutomationElement.FromPoint(new System.Windows.Point(x, y));

            var controls = new List<Control>();

            var label = new Label()
            {
                Text = $"{uiElement.Current.GetType().Name}: {uiElement.Current.Name}",
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(_itemIndent, controls.Count * _itemHeight + _itemIndent)
            };

            controls.Add(label);

            var supportedPatterns = uiElement.GetSupportedPatterns();
            var controlsCount = controls.Count;
            foreach (var item in supportedPatterns)
            {
                PatternToControls(controls, item);
            }

            AddButtons(controls, uiElement);

            Controls.AddRange(controls.ToArray());
            Height = controls.Sum(_ => _.Height);
            Width = controls.Max(_ => _.Width);
            Width = Width >= 300 ? Width : 300;

            BringToFront();

        }

        private void PatternToControls(List<Control> formControls, AutomationPattern automationPattern)
        {
            formControls.Add(new RadioButton()
            {
                Name = automationPattern.ProgrammaticName,
                Text = automationPattern.ProgrammaticName,
                AutoSize = true,
                Location = new Point(_itemIndent, formControls.Count * _itemHeight + _itemIndent)
            });

            switch (automationPattern.ProgrammaticName)
            {
                case "TextPatternIdentifiers.Pattern":
                    formControls.Add(new TextBox()
                    {
                        Name = automationPattern.ProgrammaticName,
                        Width = 200,
                        Location = new Point(_itemIndent, formControls.Count * _itemHeight + _itemIndent)
                    });
                    break;
            }
        }


        private void AddButtons(List<Control> controls, AutomationElement uiElement)
        {
            var buttonOk = new Button()
            {
                Text = $"Record",
                AutoSize = true,
                Location = new Point(_itemIndent, controls.Count * _itemHeight + _itemIndent)
            };

            var buttonCancel = new Button()
            {
                Text = $"Cancel",
                AutoSize = true,
                Location = new Point(buttonOk.Width + _itemIndent, controls.Count * _itemHeight + _itemIndent)
            };

            var buttonEnd = new Button()
            {
                Text = $"End",
                AutoSize = true,
                Location = new Point(buttonOk.Width + _itemIndent + buttonCancel.Width, controls.Count * _itemHeight + _itemIndent)
            };

            buttonOk.Click += (sender, e) =>
            {
                var process = Detect(uiElement.Current.ProcessId);

                var checkedRb = controls.Where(_ => _.GetType() == typeof(RadioButton) && (_ as RadioButton)?.Checked == true).FirstOrDefault();

                Result.Pattern = checkedRb.Text;
                Result.Value = controls.FirstOrDefault(_ => _.Name == checkedRb.Name && _ is TextBox)?.Text;
                Result.Locator = new Locator()
                {
                    FllePath = OnSafe(() => process?.MainModule?.FileName),
                    Point = Location,
                    AutomationId = uiElement.Current.AutomationId,
                    LocalizedControlType = uiElement.Current.LocalizedControlType,
                    Name = uiElement.Current.Name
                };
                DialogResult = DialogResult.OK;
                Close();
            };

            buttonCancel.Click += (sender, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            buttonEnd.Click += (sender, e) =>
            {
                DialogResult = DialogResult.Yes;
            };

            controls.Add(buttonOk);
            controls.Add(buttonCancel);
            controls.Add(buttonEnd);
        }

        private Process Detect(int pid)
        {
            var allProcesses = Process.GetProcesses();
            AutomationElement root = null;
            foreach (var item in allProcesses)
            {
                try
                {
                    if (item.Id == pid)
                    {
                        return item;
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return null;
        }

        private T OnSafe<T>(Func<T> toDo) where T: class
        {
            try
            {
                return toDo?.Invoke();
            }
            catch (Exception ex)
            {
            }
            return default(T);
        }
        
    }
}
