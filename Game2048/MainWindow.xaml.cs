using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game2048
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int score;
        Model[,] pins = new Model[4, 4];
        Model[,] oldPins = new Model[4, 4];
        Label[,] labels = new Label[4, 4];


        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
           // Model model = new Model();
            score = 0;
            Model.AddNewGameLables( pins);//!!!!!!!
            Model.FillGameMap(oldPins);
            Score.Text = score.ToString();
            AddNewLable();//!!!!!!!!!!!!!!!!!!!!!!
            
        }

        private void AddNewLable()// Метод для отрисовки нового блока
        {
            Clear();
            for (int row = 0; row < 4; row++)
            {
                for (int clm = 0; clm < 4; clm++)
                {
                    labels[row, clm] = new Label();
                    labels[row, clm].BorderThickness = new Thickness(4);
                    labels[row, clm].FontStretch = new FontStretch();
                    if (pins[row, clm].number != 0)
                    {
                        labels[row, clm].Background = Model.GetBlokColor(pins[row, clm]);
                        labels[row, clm].Content = pins[row, clm].number.ToString();
                        labels[row, clm].FontSize = 35;
                    }
                    else
                    {
                        labels[row, clm].Background = Model.GotEmptyBlokColor();
                    }

                    Grid.SetColumn(labels[row, clm], clm);
                    Grid.SetRow(labels[row, clm], row);
                    grid.Children.Add(labels[row, clm]);
                }
            }
        }

        private void Clear()//Очистка игрового поля
        {
            for (int i = 0; i < grid.Children.Count; i++)
            {
                if ((Grid.GetColumn(grid.Children[i]) != 4))
                {
                    grid.Children.Remove(grid.Children[i]);
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: GoUp(); break;
                case Key.Down: GpDown(); break;
                case Key.Left: GoLeft(); break;
                case Key.Right: GoRight(); break;
                default: break;
            }
            if (GameOver(pins))
            {
                MessageBox.Show("Игра окончена! Счет: " + score.ToString(), "");
            }
        }

        private bool GameOver(Model[,] pin)//Проверка на статус игры
        {
            for (int row = 0; row < 4; row++)
            {
                for (int clm = 0; clm < 4; clm++)
                {
                    if (pin[row, clm].number == 0)
                    {
                        return false;
                    }
                }
            }

            for (int row = 0; row < 4; row++)
            {
                for (int clm = 0; clm < 3; clm++)
                {
                    if (pin[row, clm].number == pin[row, clm + 1].number)
                    {
                        return false;
                    }
                }
            }
            for (int clm = 0; clm < 4; clm++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if (pin[row + 1, clm].number == pin[row, clm].number)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        private void GoUp()
        {
            if (Model.TryUp(pins, oldPins, score) == true)
            {
                Model.RandomAddLable(pins);
                AddNewLable();
                Score.Text = score.ToString();
            }
        }

        private void GpDown()
        {
            if (Model.TryDown(pins, oldPins, score) == true)
            {
                Model.RandomAddLable(pins);
                AddNewLable();
                Score.Text = score.ToString();
            }
        }
        private void GoLeft()
        {
            if (Model.TryLeft(pins, oldPins, score) == true)
            {
                Model.RandomAddLable(pins);
                AddNewLable();
                Score.Text = score.ToString();
            }
        }

        private void GoRight()
        {
            if (Model.TryRight(pins, oldPins, score) == true)
            {
                Model.RandomAddLable(pins);
                AddNewLable();
                Score.Text = score.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
    }
}
