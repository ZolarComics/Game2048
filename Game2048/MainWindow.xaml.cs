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
        Label[,] labels = new Label[4, 4];

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            score = 0;
            Model.AddNewGameLables(pins);//!!!!!!!
            Score.Text = score.ToString();
            AddNewLable();//!!!!!!!!!!!!!!!!!!!!!!
            
        }

        private void AddNewLable()
        {
            throw new NotImplementedException();
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
        }

        private void GoUp()
        {
            score = 2048;
            Score.Text = score.ToString();
        }

        private void GpDown()
        {
            throw new NotImplementedException();
        }
        private void GoLeft()
        {
            throw new NotImplementedException();
        }

        private void GoRight()
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
    }
}
