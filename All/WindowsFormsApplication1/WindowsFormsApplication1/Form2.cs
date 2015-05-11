using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        Generator refGenerator;
        public Form2(Generator generator)
        {
            refGenerator = generator;
            refGenerator.evCheckPort += refGenerator_evCheckPort;
            refGenerator.evConnected += refGenerator_evConnected;
            InitializeComponent();
        }

        void refGenerator_evConnected()
        {
            this.DialogResult = DialogResult.OK;
        }

        void refGenerator_evCheckPort(string portName)
        {
            label1.BeginInvoke(new Action(()=> label1.Text= portName));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != string.Empty)
            {
                //SettingsProgramm.Instance.TypeGen = (CBIthem.TypesGenerator)combobobx.Text; смысл
                refGenerator.SetSelectTypeGen();
            }
        }
    }
}
