using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Game2048
{
    class Model
    {
        public int number = 0;
        public bool isJoin = false;
        public bool isAddLable = false;

        private static SolidColorBrush StaleFor2 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString(" "));
        private static SolidColorBrush StaleFor4 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString(" "));
        private static SolidColorBrush StaleFor8 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString(" "));
        private static SolidColorBrush StaleFor16 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString(" "));
        private static SolidColorBrush StaleFor32 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString(" "));
        private static SolidColorBrush StaleFor64 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString(" "));
        private static SolidColorBrush StaleFor128 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString(" "));
        private static SolidColorBrush StaleFor256 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString(" "));
        private static SolidColorBrush StaleFor512 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString(" "));
        private static SolidColorBrush StaleFor1024 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString(" "));
        private static SolidColorBrush StaleFor2048 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString(" "));

        private static SolidColorBrush Empty = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString(" "));

        public static int CountZeros(Model[,] pin)// Подсчет пустых лейблов
        {
            int zeros = 0;
            for (int row = 0; row < 4; row++)
            {
                for (int clm = 0; clm < 4; clm++)
                {
                    if (pin[row, clm].number == 0)
                    {
                        zeros++;
                    }
                }
            }
            return zeros;
        }

        public static void AddNewGameLables(Model[,] pin)// Дейсвия при запуске новой игры
        {
            FillGameMap(pin);
            RandomAddLable(pin);
            RandomAddLable(pin);
        }

        public static List<int> LIstOfProc(List<int> list, int score)// Создание списка изменений 
        {
            List<int> resalt = new List<int>();
            int first = 0;
            int second = -1;
            while (second <= 3 && first <= 2)
            {
                if (list[first] == 0)
                {
                    first++;
                    second = first + 1;
                }
                else if (list[second] == 0)
                {
                    second++;
                }
                else
                {
                    if (list[first] == list[second])
                    {
                        resalt.Add(first);
                        list[first] = list[first] * 2;
                        list[second] = 0;
                        score = score + list[first];
                        first++;
                        second = first + 1;
                    }
                }
            }

            int zerosIndexses = -1;
            for (int i = 0; i < 4; i++)
            {
                if (list[i] == 0)
                {
                    if (zerosIndexses == -1)
                    {
                        zerosIndexses = i;
                    }
                }
                else
                {
                    if (zerosIndexses != -1)
                    {
                        if (resalt.Contains(i))
                        {
                            resalt[resalt.IndexOf(i)] = zerosIndexses;
                        }
                        list[zerosIndexses] = list[i];
                        list[i] = 0;
                        zerosIndexses = -1;
                        i = zerosIndexses + 1;
                        resalt.Add(-1);
                    }
                }
            }
            return resalt;
        }

        public static void FillGameMap(Model[,] pin)// Заполнение полей модели пустыми лейблами
        {
            for (int row = 0; row < 4; row++)
            {
                for (int clm = 0; clm < 4; clm++)
                {
                    pin[row, clm] = new Model();
                }
            }
        }

        public static void RandomAddLable(Model[,] pin)// Добавление блока в рандомном месте с рандомным значением
        {
            int zeros = Model.CountZeros(pin);
            Random random = new Random();
            int randomPosition = random.Next(0, zeros);
            int randomNamber = random.Next(1, 2) * 2;
            int step = -1;

            for (int row = 0; row < 4; row++)
            {
                for (int clm = 0; clm < 4; clm++)
                {
                    if (pin[row, clm].number == 0)
                    {
                        step++;
                        if (step == randomPosition)
                        {
                            pin[row, clm].number = randomNamber;
                            pin[row, clm].isAddLable = true;
                        }

                    }
                }
            }
        }

    }
}
