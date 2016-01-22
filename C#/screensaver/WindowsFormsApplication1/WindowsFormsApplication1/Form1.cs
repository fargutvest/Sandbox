using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
// Для класса ArrayList
using System.Collections;
// Для работы с реестром
using Microsoft.Win32;
using System.Diagnostics;

// Класс, реализующий экранную заставку

namespace CSharpApplication.ScreenSaverExample
{
    class ScreenSaver : Form
    {
        // Позиция курсора на момент запуска программы
        Point StartCursorPosition = Control.MousePosition;
        // Таймер для смены изображений
        Timer tm = new Timer();
        // Изображение
        Image im = null;
        // Список путей к изображениям
        ArrayList PathList;
        // Размеры графической поверхности
        RectangleF ScreenRect;
        // Объект класса, который предоставляет методы
        // для рисования объектов на графическом устройстве
        Graphics gr = null;

        // Мютекс для предотвращения запусков
        // нескольких экземпляров данного приложения
        static System.Threading.Mutex mutex;

        // Путь к каталогу с изображениями
        static string Path;

        // Дескриптор окна препросмотра экранных
        // заставок Windows
        static IntPtr handle = IntPtr.Zero;

        static void Main(string[] args /* параметры командной строки */)
        {
            // Заходим в ветку реестра HKEY_CURRENT_USER
            RegistryKey rk = Registry.CurrentUser;
            // Создаем или открываем подраздел реестра
            rk = rk.CreateSubKey(@"Software\CSharpApplications\ScreenSaver");
            // Считываем данные из подраздела или null по умолчанию
            Path = (string)rk.GetValue("PathToImages", null);
            // Закрываем ветку
            rk.Close();

            // Если присутствуют аргументы командной строки      
            if (args.Length > 0)
            {
                // Настройка экранной заставки
                if (args[0].StartsWith("/c"))
                {
                    // Создаем и показываем диалог настройки
                    Settings set = new Settings(Path);
                    set.ShowDialog();
                    return;
                }
                // Препросмотр экранной заставки
                if (args[0].StartsWith("/p"))
                {
                    // Создаем мютекс
                    mutex = new System.Threading.Mutex(false, "ScreenSaver.MyMutex");
                    // Проверяем не запущен ли уже экземпляр этого процесса
                    if (mutex.WaitOne(0, false) == false)
                    {
                        // Уже запущен
                        return;
                    }
                    // Записываем дескриптор окна, на котором
                    // Windows осуществляет препросмотр
                    // экранных заставок
                    handle = (IntPtr)Convert.ToInt32(args[1]);
                }
            }

            // Запуск приложения
            Application.Run(new ScreenSaver());

            // Освобождаем мютекс
            if (mutex != null)
                mutex.ReleaseMutex();
        }

        // Конструктор
        ScreenSaver()
        {
            // Цвет фона формы
            this.BackColor = Color.Black;
            // Убираем у формы рамку
            this.FormBorderStyle = FormBorderStyle.None;

            // Прячем курсор
            Cursor.Hide();

            // Растягиваем форму на весь экран
            this.ClientSize = Screen.GetBounds(this).Size;
            // Установка расположения формы
            this.Location = new Point(0, 0);

            // Режим препросмотра
            if (handle != IntPtr.Zero)
            {
                // Убираем приложение из панели задач
                this.ShowInTaskbar = false;
                // Делаем форму прозрачной
                this.TransparencyKey = Color.Black;
            }

            // Обработчик таймера
            tm.Tick += new EventHandler(tm_Tick);
            tm.Interval = 1000;
        }

        // Обработчик нажатия клавиш
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Закрываем приложение
            Application.Exit();
            base.OnKeyDown(e);
        }

        // Обработчик движения мыши
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Если с момента запуска приложения мышь дернулась
            if (StartCursorPosition != Control.MousePosition)
            {
                // Закрываем приложение
                Application.Exit();
            }

            base.OnMouseMove(e);
        }

        // Обработчик отрисовки поверхности формы
        protected override void OnPaint(PaintEventArgs e)
        {
            if (handle == IntPtr.Zero)
            {
                // Графическая поверхность формы
                gr = e.Graphics;
            }
            else
            {
                try
                {
                    // Получаем графическую поверхность
                    // окна препросмотра экранных заставок Windows
                    gr = Graphics.FromHwnd(handle);
                }
                catch
                {
                    // Не сложилось
                    Application.Exit();
                    return;
                }
            }

            // Определяем размеры видимой поверхности
            ScreenRect = gr.VisibleClipBounds;

            // Режим препросмотра
            if (handle != IntPtr.Zero)
            {
                // Создаем черную кисть
                Brush br = new SolidBrush(Color.Black);
                // Заливаем поверхность 
                // (стираем предыдущее изображение)
                gr.FillRectangle(br, ScreenRect);
            }
            // Если список файлов пуст
            if (PathList != null && PathList.Count == 0)
            {
                // Создаем синюю кисть
                Brush br = new SolidBrush(Color.Blue);
                // Создаем шрифт
                Font font = new Font("Times New Roman", 12);
                // Отрисовываем строку
                gr.DrawString("Каталог\n" + Path + "\nпуст!!!", font, br, 0, 0);
            }
            else if (PathList != null)
            {
                // Вычисляем коэффициент пропорциональности
                // для показа изображения на весь экран
                SizeF sizef = new SizeF(im.Width / im.HorizontalResolution,
                                        im.Height / im.VerticalResolution);
                float Scale = Math.Min(ScreenRect.Width / sizef.Width,
                                       ScreenRect.Height / sizef.Height);
                // Пропорционально изменяем размер изображения
                sizef.Width *= Scale;
                sizef.Height *= Scale;

                // Отрисовываем изображение
                gr.DrawImage(im,
                   ScreenRect.X + (ScreenRect.Width - sizef.Width) / 2,
                   ScreenRect.Y + (ScreenRect.Height - sizef.Height) / 2,
                   sizef.Width, sizef.Height);

                tm.Interval = 3000;
            }

            base.OnPaint(e);

            // Запуск таймера
            tm.Start();
        }

        // Обработчик таймера
        private void tm_Tick(object sender, EventArgs e)
        {
            // Останавливаем таймер
            tm.Stop();

            // Если режим препросмотра, обновляем данные из реестра
            if (handle != IntPtr.Zero)
            {
                string temppath;
                RegistryKey rk = Registry.CurrentUser;
                rk = rk.CreateSubKey(@"Software\CSharpApplications\ScreenSaver");
                temppath = (string)rk.GetValue("PathToImages", null);
                rk.Close();

                // Если данные изменились
                if (Path != temppath)
                {
                    Path = temppath;
                    PathList = null;
                }
            }

            // Запускаем поиск изображений в указанном 
            // пользоателем каталоге
            if (PathList == null)
                SearchPictures();

            // Текущее изображение
            string picture = null;

            // Если каталог существует
            if (PathList != null)
            {
                // Если в каталоге присутствуют изображения
                if (PathList.Count == 0)
                {
                    // Перерисовываемся (на экран выводится строка текста)
                    Invalidate();
                    return;
                }

                // Инициализируем генератор случайных чисел
                Random rand = new Random();
                // Выбираем случайное изображение из списка
                picture = (string)PathList[rand.Next(PathList.Count)];
            }
            else  // Указанный каталог не доступен
            {
                // Подставляем стандартный каталог Windows
                // для хранения изображений
                Path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                tm.Interval = 100;
                // Запуск таймера
                tm.Start();
                return;
            }

            try
            {
                // Считываем картинку из файла
                im = Image.FromFile(picture);
            }
            catch
            {
                // Неудача
                im = null;
                tm.Interval = 100;
                // Запуск таймера (повторный вызов функции)
                tm.Start();
                return;
            }

            // Перерисовываемся
            Invalidate();
        }

        // Функция поиска изображений
        void SearchPictures()
        {
            try
            {
                // Ищем в каталоге картинки
                string[] files1 = Directory.GetFiles(Path, "*.jpg");
                string[] files2 = Directory.GetFiles(Path, "*.bmp");

                // Создаем общий список найденных файлов
                // и сгружаем в него найденный пути
                PathList = new ArrayList(files1.Length + files2.Length);
                PathList.AddRange(files1);
                PathList.AddRange(files2);
            }
            catch
            {
                // Проблемы
                PathList = null;
            }
        }
    }

    // Диалог настроек
    class Settings : Form
    {
        Button Browse = new Button();
        TextBox PathToImages = new TextBox();
        Label Prompt = new Label();

        FolderBrowserDialog fbd = new FolderBrowserDialog();

        public Settings(string Path /* Предыдущий путь из реестра */)
        {
            this.ClientSize = new Size(260, 90);
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Browse.Parent = this;
            Browse.Dock = DockStyle.Top;
            Browse.Text = "Выбор каталога";

            Browse.Click += new EventHandler(Browse_Click);

            PathToImages.Parent = this;
            PathToImages.Dock = DockStyle.Top;
            PathToImages.ReadOnly = true;
            PathToImages.Size = new Size(0, 50);
            PathToImages.Multiline = true;
            PathToImages.Text = Path;

            Prompt.Parent = this;
            Prompt.Dock = DockStyle.Top;
            Prompt.AutoSize = true;
            Prompt.Text = "Введите путь к каталогу с изображениями:";

            fbd.SelectedPath = Path;

            this.Text = "Настройки";
            this.ShowInTaskbar = false;
            this.CenterToScreen();
        }

        // Обработчик нажатия кнопки
        private void Browse_Click(object sender, EventArgs e)
        {
            // Если пользователь нажал OK
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                // Записываем в реестр новый путь
                RegistryKey rk = Registry.CurrentUser;
                rk = rk.CreateSubKey(@"Software\CSharpApplications\ScreenSaver");
                // Установка нового значения параметра
                rk.SetValue("PathToImages", fbd.SelectedPath);
                rk.Close();
                // Отображаем выбранный путь в текстовом поле
                PathToImages.Text = fbd.SelectedPath;
            }
        }
    }
}