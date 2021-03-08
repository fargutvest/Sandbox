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
        public ContextMenuResult Result { get; }

        public ContextMenuForm()
        {
            InitializeComponent();
            Result = new ContextMenuResult();
        }

        public void Fill(int x, int y)
        {
            var itemHeight = 20;
            var itemIndent = 10;


            Location = new Point(x, y);

            var uiElement = AutomationElement.FromPoint(new System.Windows.Point(x, y));

            var controls = new List<Control>();

            var label = new Label()
            {
                Text = $"{uiElement.Current.GetType().Name}: {uiElement.Current.Name}",
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(itemIndent, controls.Count * itemHeight + itemIndent)
            };

            controls.Add(label);

            var supportedPatterns = uiElement.GetSupportedPatterns();
            var controlsCount = controls.Count;
            var radioButtons = supportedPatterns.Select(_ =>
            {
               var rb = new RadioButton()
                {
                    Text = $"{_.ProgrammaticName}",
                    AutoSize = true,
                    Location = new Point(itemIndent, controlsCount * itemHeight + itemIndent)
                };
                controlsCount += 1;
                return rb;
            }).ToList();
            
            if (radioButtons.Count == 1)
            {
                radioButtons[0].Checked = true;
            }
            controls.AddRange(radioButtons);

            var buttonOk = new Button()
            {
                Text = $"Record",
                AutoSize = true,
                Location = new Point(itemIndent, controls.Count * itemHeight + itemIndent)
            };

            var buttonCancel = new Button()
            {
                Text = $"Cancel",
                AutoSize = true,
                Location = new Point(buttonOk.Width + itemIndent, controls.Count * itemHeight + itemIndent)
            };

            var buttonEnd = new Button()
            {
                Text = $"End",
                AutoSize = true,
                Location = new Point(buttonOk.Width + itemIndent + buttonCancel.Width, controls.Count * itemHeight + itemIndent)
            };

            buttonOk.Click += (sender, e) =>
            {
                var process = Detect(uiElement.Current.ProcessId);
                Result.Pattern = controls.Where(_ => _.GetType() == typeof(RadioButton) && (_ as RadioButton)?.Checked == true).Select(_ => _.Text).FirstOrDefault();
                Result.Locator = new Locator()
                {
                    FllePath = OnSafe(() => process?.MainModule?.FileName),
                    Point = new Point(x, y),
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

            Controls.AddRange(controls.ToArray());
            Height = controls.Sum(_ => _.Height);
            Width = controls.Max(_ => _.Width);

            BringToFront();

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
