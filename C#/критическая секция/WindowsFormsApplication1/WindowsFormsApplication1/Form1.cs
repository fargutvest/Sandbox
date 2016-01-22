using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

// Пространство, в котором находятся потоковые классы
using System.Threading;
namespace CriticalSection
{
	/// 
	/// Summary description for Form1.
	/// 
	public class CriticalSection : System.Windows.Forms.Form
	{
		// Массив данных
		private int[] DataArray;
		// Переменная целого типа определяет нужное действие
		int Action = 0;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox Count;
		private System.Windows.Forms.ListBox DataListBox;
		private System.Windows.Forms.Label Summa;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label MaxInfo;
		private System.Windows.Forms.Label SummaInfo;
		private System.Windows.Forms.Label MaxResult;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox listBox1;
		/// 
		/// Required designer variable.
		/// 
		private System.ComponentModel.Container components = null;

		public CriticalSection()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// 
		/// Clean up any resources being used.
		/// 
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// 
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// 
		private void InitializeComponent()
		{
			this.DataListBox = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Count = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.Summa = new System.Windows.Forms.Label();
			this.SummaInfo = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.MaxInfo = new System.Windows.Forms.Label();
			this.MaxResult = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// DataListBox
			// 
			this.DataListBox.Location = new System.Drawing.Point(8, 40);
			this.DataListBox.Name = "DataListBox";
			this.DataListBox.Size = new System.Drawing.Size(192, 199);
			this.DataListBox.TabIndex = 0;
			
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 7;
			this.label1.Text = "Данные";
			// 
			// Count
			// 
			this.Count.Location = new System.Drawing.Point(16, 272);
			this.Count.Name = "Count";
			this.Count.Size = new System.Drawing.Size(104, 20);
			this.Count.TabIndex = 0;
			this.Count.Text = "10";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(136, 272);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(136, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Генерировать массив";
			this.button1.Click += new System.EventHandler(this.OnGenerateClick);
			// 
			// Summa
			// 
			this.Summa.Location = new System.Drawing.Point(224, 8);
			this.Summa.Name = "Summa";
			this.Summa.TabIndex = 4;
			this.Summa.Text = "Сумма элементов";
			// 
			// SummaInfo
			// 
			this.SummaInfo.Location = new System.Drawing.Point(224, 40);
			this.SummaInfo.Name = "SummaInfo";
			this.SummaInfo.TabIndex = 5;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(224, 120);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(256, 48);
			this.button2.TabIndex = 6;
			this.button2.Text = "Запустить потоки";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// MaxInfo
			// 
			this.MaxInfo.Location = new System.Drawing.Point(392, 8);
			this.MaxInfo.Name = "MaxInfo";
			this.MaxInfo.Size = new System.Drawing.Size(100, 24);
			this.MaxInfo.TabIndex = 7;
			this.MaxInfo.Text = "Максимальное значение";
			
			// 
			// MaxResult
			// 
			this.MaxResult.Location = new System.Drawing.Point(392, 40);
			this.MaxResult.Name = "MaxResult";
			this.MaxResult.TabIndex = 8;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.TabIndex = 7;
			this.label2.Text = "Данные";
			// 
			// listBox1
			// 
			this.listBox1.Location = new System.Drawing.Point(8, 40);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(192, 199);
			this.listBox1.TabIndex = 0;
			// 
			// CriticalSection
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 309);
			this.Controls.Add(this.MaxResult);
			this.Controls.Add(this.MaxInfo);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.SummaInfo);
			this.Controls.Add(this.Summa);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.Count);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.DataListBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listBox1);
			this.Name = "CriticalSection";
			this.Text = "Критическая секция";
			
			this.ResumeLayout(false);

		}
		#endregion

		/// 
		/// The main entry point for the application.
		/// 

		static void Main() 
		{
			Application.Run(new CriticalSection());
		}

		private void OnGenerateClick(object sender, System.EventArgs e)
		{
			try
			{
				DataListBox.Items.Clear();
				Random rnd = new Random();
				int result = Convert.ToInt32(Count.Text);
				DataArray = new int[result];
				for(int i = 0;i<DataArray.Length;i++)
				{
					DataArray[i] = rnd.Next(0,100);
					DataListBox.Items.Add(DataArray[i]);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message,"Ошибка !!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			// Создание потоков и их запуск !!!
			Thread thread1 = new Thread(new ThreadStart(MakeAction));
			Thread thread2 = new Thread(new ThreadStart(MakeAction));
			Thread thread3 = new Thread(new ThreadStart(MakeAction));
			// Запуск потоков
			thread1.Start();
			thread2.Start();
			thread3.Start();
		}

		/// 
		/// Выполняет одно из трех действий (сложение,поиск максимума,установка всех элементов в какое-то  значение)
		/// 
		private void MakeAction()
		{
			// Критическая секция начало
			lock(this)
			{
				int Result = 0;
				int CurMax = DataArray[0];
				Random rnd = new Random();
				int SetVal = rnd.Next(0,10);
				switch(Action)
				{
					case 0:
						for(int i = 0;i<DataArray.Length;i++)
						{
							Result+=DataArray[i];
						}
						SummaInfo.Text = Result.ToString();				
						break;
					case 1:
						for(int i = 1;i<DataArray.Length;i++)
						{
							if(DataArray[i]>CurMax)
								CurMax = DataArray[i];
						}
						MaxResult.Text = CurMax.ToString();				
						break;
					case 2:
						DialogResult res = MessageBox.Show("Нажмите на OK и все элементы  массива будут равны = "+SetVal,"Вопрос",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
						if(res==DialogResult.OK){
							for(int i = 0;i<DataArray.Length;i++)
							{
								DataListBox.Items[i] = DataArray[i] = SetVal;
							}
						}
						break;
					default:
						MessageBox.Show("Неизвестное значение","Неизвестное значение  !!!",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
						break;;
				}
				Action++;
				if(Action>2)
					Action = 0;
			}// Критическая секция конец
		}

	}
}