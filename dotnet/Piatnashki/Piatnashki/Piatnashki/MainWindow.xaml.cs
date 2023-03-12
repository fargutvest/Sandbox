using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Piatnashki
{
    public partial class MainWindow : Window
    {
        private List<SquareViewModel> squares;
        
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void InitXYCoordinates(int index, out int x, out int y)
        {
            int[][] matrixToInit =
            {
                new [] {1,2,3,4},
                new [] {5,6,7,8},
                new [] {9,10,11,12},
                new [] {13,14,15,0},
            };

            x = 0;
            y = 0;

            for (int i = 0; i < matrixToInit.Length; i++)
            {
                int[] row = matrixToInit[i];

                for (int j = 0; j < row.Length; j++)
                {
                    if (row[j] == index)
                    {
                        x = j;
                        y = i;
                    }
                }
            }
        }

        private void Init()
        {
            squares = new List<SquareViewModel>();

            for (int index = 0; index <= 15; index++)
            {
                InitXYCoordinates(index, out int x, out int y);

                var view = new Square();
                var viewModel = new SquareViewModel
                {
                    Name = index.ToString(),
                    IsZero = index == 0,
                    X = x,
                    Y = y
                };
                view.DataContext = viewModel;
                view.MouseDown += OnClicked;
                squares.Add(viewModel);

                if (viewModel.IsZero == false)
                {
                    MyGrid.Children.Add(view);
                    Grid.SetRow(view, y);
                    Grid.SetColumn(view, x);
                }
            }
        }

        private void OnClicked(object sender, System.EventArgs e)
        {
            var clicked = sender as Square;
            TryShiftSquare(clicked);
        }

        private void TryShiftSquare(Square clicked)
        {
            SquareViewModel clickedViewModel = squares.First(_ => _.Equals(clicked.DataContext));
            SquareViewModel zero = squares.First(_ => _.IsZero);
            int deltaX = Math.Abs(clickedViewModel.X - zero.X);
            int deltaY = Math.Abs(clickedViewModel.Y - zero.Y);

            if ((deltaY == 1 && deltaX == 1) == false && deltaY <= 1 && deltaX <= 1)
            {
                Grid.SetColumn(clicked, zero.X);
                Grid.SetRow(clicked, zero.Y);

                int tempX = clickedViewModel.X;
                int tempY = clickedViewModel.Y;
                clickedViewModel.X = zero.X;
                clickedViewModel.Y = zero.Y;
                zero.X = tempX;
                zero.Y = tempY;
            }
        }
    }
}
