using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Forms;

namespace ListenCursor
{
    public partial class ContextMenuForm : Form
    {
        private Dictionary<string, string> _result;
        public Dictionary<string, string> Result => _result;

        public ContextMenuForm()
        {
            InitializeComponent();
            _result = new Dictionary<string, string>();
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
            foreach (var item in supportedPatterns)
            {
                var key = $"{item.GetType().Name}: {item.ProgrammaticName}";
                _result[key] = false.ToString();
                var checkBox = new CheckBox()
                {
                    Text = key,
                    AutoSize = true,
                    Location = new Point(itemIndent, controls.Count * itemHeight + itemIndent)
                };
                checkBox.CheckedChanged += (sender, e) => 
                {
                    _result[key] = checkBox.Checked.ToString(); 
                };

                controls.Add(checkBox);
            }

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

            buttonOk.Click += (sender, e) =>
            {
                DialogResult = DialogResult.OK;
                Close();
            };

            buttonCancel.Click += (sender, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            controls.Add(buttonOk);
            controls.Add(buttonCancel);

            Controls.AddRange(controls.ToArray());
            Height = controls.Sum(_ => _.Height);
            Width = controls.Max(_ => _.Width);

            BringToFront();

        }
    }
}
