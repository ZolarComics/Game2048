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
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#72E260"));
        private static SolidColorBrush StaleFor4 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#60E2B3"));
        private static SolidColorBrush StaleFor8 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#47DBD2"));
        private static SolidColorBrush StaleFor16 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#47AFDB"));
        private static SolidColorBrush StaleFor32 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4794DB"));
        private static SolidColorBrush StaleFor64 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#314FEF"));
        private static SolidColorBrush StaleFor128 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4B31EF"));
        private static SolidColorBrush StaleFor256 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6E31EF"));
        private static SolidColorBrush StaleFor512 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B231EF"));
        private static SolidColorBrush StaleFor1024 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EF318C"));
        private static SolidColorBrush StaleFor2048 = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EF313C"));

        private static SolidColorBrush Empty = 
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));

        public static SolidColorBrush GetBlokColor(Model pin)//Метод подбора цвета для лейблов
        {
            switch (pin.number)
            {
                case 2: return StaleFor2;
                case 4: return StaleFor4;
                case 8: return StaleFor8;
                case 16: return StaleFor16;
                case 32: return StaleFor32;
                case 64: return StaleFor64;
                case 128: return StaleFor128;
                case 256: return StaleFor256;
                case 512: return StaleFor512;
                case 1024: return StaleFor1024;
                case 2048: return StaleFor2048;
                default: return Empty;
            }
            
        }

        public static SolidColorBrush GotEmptyBlokColor()// Цвет для пустых лейблов
        {
            return Empty;
        }
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
            int second = 1;
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
                    else
                    {
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
            var random = new Random();
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

        public static void Coppy(Model[,] clone, Model[,] live)// Копирование игрового поля
        {
            for (int row = 0; row < 4; row++)
            {
                for (int clm = 0; clm < 4; clm++)
                {
                    clone[row, clm].number = live[row, clm].number;
                    clone[row, clm].isJoin = live[row, clm].isJoin;
                    clone[row, clm].isAddLable = live[row, clm].isAddLable;
                }
            }
        }

        public static bool TryUp(Model[,] pin, Model[,] oldBlocks, int score) // Проверка для кнопки вверх
        {
            Model[,] TempBlocks = new Model[4, 4];
            FillGameMap(TempBlocks);
            Coppy(TempBlocks, pin);

            bool BlocksChanged = false;
            for (int clm = 0; clm < 4; clm++)
            {
                List<int> list = new List<int>();
                for (int row = 0; row < 4; row++)
                {
                    list.Add(pin[row, clm].number);
                    pin[row, clm].isJoin = false;
                    pin[row, clm].isAddLable = false;
                }
                List<int> ChangedList = LIstOfProc(list, score);
                if (ChangedList.Count != 0)
                {
                    BlocksChanged = true;
                    int l_ls = ChangedList.Count;
                    for (int i = 0; i < l_ls; i++)
                    {
                        if (ChangedList[i] != -1)
                            pin[ChangedList[i], clm].isJoin = true;
                    }
                    int ls_k = 0;
                    for (int row = 0; row < 4; row++)
                    {
                        pin[row, clm].number = list[ls_k];
                        ls_k++;
                    }
                    list.Clear();
                }
            }

            if (BlocksChanged == true)
            {
                Coppy(oldBlocks, TempBlocks);
            }
            return BlocksChanged;
        }

        public static bool TryDown(Model[,] pin, Model[,] oldBlocks, int score)
        {
            Model[,] TempBlocks = new Model[4, 4];
            FillGameMap( TempBlocks);
            Coppy( TempBlocks, pin);

            bool BlocksChanged = false;
            for (int clm = 0; clm < 4; clm++)
            {
                List<int> list = new List<int>();
                for (int row = 3; row >= 0; row--)
                {
                    list.Add(pin[row, clm].number);
                    pin[row, clm].isJoin = false;
                    pin[row, clm].isAddLable = false;
                }
                List<int> ChangedList = LIstOfProc(list, score);
                if (ChangedList.Count != 0) 
                {
                    BlocksChanged = true;
                    int l_ls = ChangedList.Count;
                    for (int i = 0; i < l_ls; i++)
                    {
                        if (ChangedList[i] != -1)
                            pin[3 - ChangedList[i], clm].isJoin = true;
                    }
                    int ls_k = 0;
                    for (int row = 3; row >= 0; row--)
                    {
                        pin[row, clm].number = list[ls_k];
                        ls_k++;
                    }
                }
                list.Clear();
            }

            if (BlocksChanged == true)
            {
                Coppy(oldBlocks, TempBlocks);
            }
            return BlocksChanged;
        }

        public static bool TryLeft(Model[,] pin, Model[,] oldBlocks, int score)
        {
            Model[,] TempBlocks = new Model[4, 4];
            FillGameMap(TempBlocks);
            Coppy(TempBlocks, pin);

            bool BlocksChanged = false;
            for (int row = 0; row < 4; row++)
            {
                List<int> list = new List<int>();
                for (int clm = 0; clm < 4; clm++)
                {
                    list.Add(pin[row, clm].number);
                    pin[row, clm].isJoin = false;
                    pin[row, clm].isAddLable = false;
                }
                List<int> ChangedList = LIstOfProc(list, score);
                if (ChangedList.Count != 0) 
                {
                    BlocksChanged = true;
                    int l_ls = ChangedList.Count;
                    for (int i = 0; i < l_ls; i++)
                    {
                        if (ChangedList[i] != -1)
                            pin[row, ChangedList[i]].isJoin = true;
                    }
                    int ls_k = 0;
                    for (int clm = 0; clm < 4; clm++)
                    {
                        pin[row, clm].number = list[ls_k];
                        ls_k++;
                    }
                    list.Clear();
                }
            }

            if (BlocksChanged == true) 
            {
                Coppy(oldBlocks, TempBlocks);
            }
            return BlocksChanged;
        }

        public static bool TryRight(Model[,] pin, Model[,] oldBlocks, int score) 
        {
            Model[,] TempBlocks = new Model[4, 4];
            FillGameMap(TempBlocks);
            Coppy(TempBlocks, pin);

            bool BlocksChanged = false;
            for (int row = 0; row < 4; row++)
            {
                List<int> list = new List<int>();
                for (int clm = 3; clm >= 0; clm--)
                {
                    list.Add(pin[row, clm].number);
                    pin[row, clm].isJoin = false;
                    pin[row, clm].isAddLable = false;
                }
                List<int> ChangedList = LIstOfProc(list, score);
                if (ChangedList.Count != 0) 
                {
                    BlocksChanged = true;
                    int l_ls = ChangedList.Count;
                    for (int i = 0; i < l_ls; i++)
                    {
                        if (ChangedList[i] != -1)
                            pin[row, 3 - ChangedList[i]].isJoin = true;
                    }
                    int ls_k = 0;
                    for (int clm = 3; clm >= 0; clm--)
                    {
                        pin[row, clm].number = list[ls_k];
                        ls_k++;
                    }
                    list.Clear();
                }
            }

            if (BlocksChanged == true)
            {
                Coppy(oldBlocks, TempBlocks);
            }
            return BlocksChanged;
        }
    }
}
