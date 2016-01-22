using System;
using System.Drawing;
using System.Windows.Forms;

namespace CSharpApplication.WindowsApplicationExample
{
    // Игра "Пятнашки"
    class Game : Form
    {
        // Размер стороны поля, если изменить игра станет более веселой :)
        const int Side = 4;
        // Номер "пустышки"
        const int Void = Side * Side;
        // Начальные координаты пустышки
        int Voidx = Side - 1, Voidy = Side - 1;
        // Массив кнопок
        Button[,] Field;
        // Массив значений кнопок
        int[,] Numbers;
        // Количество проделанных ходов
        int Moves;
        // Индикатор запуска игры
        bool IsGameRun;

        // Надпись для отображения прошедшего времени
        Label clock = new Label();
        // Объект таймера
        Timer timer = new Timer();
        // Инициализация генератора случайных чисел
        Random Randomize = new Random();

        // Время игры
        TimeSpan time;

        static void Main()
        {
            // Запуск приложения
            Application.Run(new Game());
        }

        // Конструктор - инициализация игры
        Game()
        {
            // Заголовок формы
            Text = "Пятнашки";
            // Стиль рамки для формы
            FormBorderStyle = FormBorderStyle.Fixed3D;
            // Выключение кнопки для развертывания окна
            MaximizeBox = false;
            // Вычисление размера клиентской области окна
            ClientSize = new Size(Side * 50 + 20, Side * 50 + 50);
            // Цвет фона
            BackColor = Color.Silver;

            // Массив кнопок
            Field = new Button[Side, Side];
            // Массив чисел
            Numbers = new int[Side, Side];

            /*************************************************************/
            /* Добавление пунктов меню
            /*************************************************************/
            MenuItem miNewGame = new MenuItem("Новая игра",
                new EventHandler(OnMenuStart), Shortcut.F2);
            MenuItem miSeparator = new MenuItem("-");
            MenuItem miExit = new MenuItem("Выход",
                new EventHandler(OnMenuExit), Shortcut.CtrlX);
            MenuItem miGame = new MenuItem("&Игра",
                new MenuItem[] { miNewGame, miSeparator, miExit });
            

            // Создание меню и его привязка к форме
            Menu = new MainMenu(new MenuItem[] { miGame });
            

            // Игра не запущена
            IsGameRun = false;

            // Таймер будет срабатывать каждую секунду
            timer.Interval = 1000;
            // Подключение обработчика таймера
            timer.Tick += new EventHandler(OnTimer);

            // Размещение надписи
            clock.Location = new Point(10, 10);
            // Ширина надписи
            clock.Width = Side * 50;
            // Высота надписи
            clock.Height = 20;
            // Родитель надписи (форма)
            clock.Parent = this;
            // Тонкая рамка
            clock.BorderStyle = BorderStyle.FixedSingle;
            // Цвет фона
            clock.BackColor = Color.DarkGray;
            // Текст выравнивается по центру надписи
            clock.TextAlign = ContentAlignment.MiddleCenter;
            // Шифт надписи
            clock.Font = new Font("Century", 14, FontStyle.Bold);
            // Текст надписи
            clock.Text = "00:00:00";

            int i, j;
            // Инициализация поля
            for (i = 0; i < Side; i++)
            {
                for (j = 0; j < Side; j++)
                {
                    // Создание новой кнопки
                    Field[i, j] = new Button();
                    // Указываем родителя для кнопки (форма)
                    Field[i, j].Parent = this;
                    // Задаем очередное число
                    Numbers[i, j] = i * Side + j + 1;
                    // Если не "пустышка"
                    if (Numbers[i, j] != Void)
                        // Отображаем число на кнопке
                        Field[i, j].Text = Convert.ToString(Numbers[i, j]);

                    // Вычисляем координаты очередной кнопки
                    Field[i, j].Left = 10 + j * 50;
                    Field[i, j].Top = 40 + i * 50;
                    Field[i, j].Width = 50;
                    Field[i, j].Height = 50;
                    // Шрифт кнопки
                    Field[i, j].Font = new Font("Century", 12, FontStyle.Bold);
                    // Ассоциируем с кнопкой ее координаты в массиве
                    Field[i, j].Tag = new Point(i, j);
                    // Добавляем обработчик нажатия на кнопку
                    Field[i, j].Click += new EventHandler(OnCellClick);
                    // Цвет текста
                    Field[i, j].ForeColor = Color.Yellow;
                    // Цвет фона
                    Field[i, j].BackColor = Color.Gray;
                }
            }

            // Отображаем форму по центру экрана
            CenterToScreen();
        }

        // Обработчик пункта меню "Выход"
        void OnMenuExit(object obj, EventArgs ea)
        {
            // Закрываем форму
            Close();
        }

        // Обработчик пункта меню "Новая игра"
        void OnMenuStart(object obj, EventArgs ea)
        {
            int i, j, k;
            int direction;
            /************************/
            /* Перемешивание поля  
            /************************/
            int Times = Side * 100;
            for (k = 0; k < Times; k++)
            {
                // Направление движения
                direction = Randomize.Next(4);

                if (direction == 0) // Вверх
                {
                    // Кнопка сверху существует
                    if (Voidx - 1 >= 0)
                    {
                        Numbers[Voidx, Voidy] = Numbers[Voidx - 1, Voidy];
                        Voidx--;
                    }
                    else
                    {
                        for (i = 0; i < Side - 1; i++)
                        {
                            Numbers[i, Voidy] = Numbers[i + 1, Voidy];
                        }
                        Voidx = Side - 1;
                    }
                }
                else if (direction == 1) // Вниз
                {
                    // Кнопка снизу существует
                    if (Voidx + 1 < Side)
                    {
                        Numbers[Voidx, Voidy] = Numbers[Voidx + 1, Voidy];
                        Voidx++;
                    }
                    else
                    {
                        for (i = Side - 1; i > 0; i--)
                        {
                            Numbers[i, Voidy] = Numbers[i - 1, Voidy];
                        }
                        Voidx = 0;
                    }
                }
                else if (direction == 2) // Влево
                {
                    // Кнопка слева существует
                    if (Voidy - 1 >= 0)
                    {
                        Numbers[Voidx, Voidy] = Numbers[Voidx, Voidy - 1];
                        Voidy--;
                    }
                    else
                    {
                        for (j = 0; j < Side - 1; j++)
                        {
                            Numbers[Voidx, j] = Numbers[Voidx, j + 1];
                        }
                        Voidy = Side - 1;
                    }
                }
                else // Вправо
                {
                    // Кнопка справа существует
                    if (Voidy + 1 < Side)
                    {
                        Numbers[Voidx, Voidy] = Numbers[Voidx, Voidy + 1];
                        Voidy++;
                    }
                    else
                    {
                        for (j = Side - 1; j > 0; j--)
                        {
                            Numbers[Voidx, j] = Numbers[Voidx, j - 1];
                        }
                        Voidy = 0;
                    }
                }

                // Новая позиция "пустышки"
                Numbers[Voidx, Voidy] = Void;
            }

            // Отображение перемешанных чисел на кнопках
            for (i = 0; i < Side; i++)
            {
                for (j = 0; j < Side; j++)
                {
                    if (Numbers[i, j] != Void)
                    {
                        Field[i, j].Text = Convert.ToString(Numbers[i, j]);
                    }
                    else
                    {
                        Field[i, j].Text = "";
                    }
                }
            }

            Moves = 0;
            // Игра запущена
            IsGameRun = true;
            // Начальное время
            time = new TimeSpan(0, 0, 0);
            clock.Text = "00:00:00";
            // Запуск таймера
            timer.Start();
        }

        // Обработчик нажатия кнопки (ход)
        void OnCellClick(object obj, EventArgs ea)
        {
            // Если игра не запущена
            if (IsGameRun == false)
                return;

            // Вынимаем "нажатый" объект
            Button btn = (Button)obj;
            // Определяем его месторасположение в массиве
            // по ассоциированным координатам
            int i = ((Point)btn.Tag).X;
            int j = ((Point)btn.Tag).Y;

            // Если нажатая кнопка расположена
            // слева, или снизу, или справа, или сверху от "пустышки"
            if (Math.Abs(i - Voidx) + Math.Abs(j - Voidy) == 1)
            {
                // Ход
                Numbers[Voidx, Voidy] = Numbers[i, j];
                Field[Voidx, Voidy].Text = Field[i, j].Text;
                // Новые координаты "пустышки"
                Voidx = i;
                Voidy = j;
                Numbers[Voidx, Voidy] = Void;
                Field[Voidx, Voidy].Text = "";

                // Ход сделан
                Moves++;
            }

            // Если "пустышка" в нижнем правом углу
            if (Voidx == Side - 1 && Voidy == Side - 1)
            {
                // Если победа
                if (IsWinner() == true)
                {
                    // Остановка таймера
                    timer.Stop();

                    string msg = "Поздравляем!!!\nВы достигли успеха за ";
                    msg += Moves;
                    if (Moves % 10 > 1 && Moves % 10 < 5 && Moves % 100 / 10 != 1)
                        msg += " хода.";
                    else if (Moves % 10 == 1 && Moves % 100 / 10 != 1)
                        msg += " ход.";
                    else
                        msg += " ходов.";
                    // Остановка игры
                    IsGameRun = false;
                    // Отображение информационного окна
                    MessageBox.Show(msg, "Победа!!!",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        // Определение выигрышной позиции
        bool IsWinner()
        {
            int i, j, k = 1;
            for (i = 0; i < Side; i++)
            {
                for (j = 0; j < Side; j++)
                {
                    // Если очередное число не совпадает с порядковым
                    if (Numbers[i, j] != k)
                        return false;

                    k++;
                }
            }
            // Выигрыш
            return true;
        }

        // Обработчик событий таймера
        void OnTimer(object obj, EventArgs ea)
        {
            // Увеличиваем время на секунду
            time += new TimeSpan(0, 0, 1);
            // Отображаем полученное время
            clock.Text = time.ToString();
        }

        // Обработчик активизации формы (получение фокуса приложением)
        protected override void OnActivated(EventArgs ea)
        {
            // Вызов базового обработчика
            base.OnActivated(ea);

            // Если игра запущена
            if (IsGameRun == true)
                // Запуск таймера
                timer.Start();
        }

        // Обработчик деактивизации формы (потеря фокуса приложением)
        protected override void OnDeactivate(EventArgs ea)
        {
            // Вызов базового обработчика
            base.OnDeactivate(ea);

            // Если игра запущена
            if (IsGameRun == true)
                // Остановка таймера
                timer.Stop();
        }
    }
}