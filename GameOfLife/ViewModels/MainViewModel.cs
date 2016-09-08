using System;
using System.Data;
using System.Linq;
using System.Threading;
using GameOfLife.Models;
using GameOfLife.Views;

namespace GameOfLife.ViewModels
{
    public class MainViewModel
    {
        private MainModel mm;
        private DataTable tempField;
        private PlayView playView;
        private int prevHash = 0;
        private int currHash = 0;

        public MainViewModel(MainModel mm, PlayView playView)
        {
            this.mm = mm;
            this.playView = playView;
        }

        internal void Play()
        {
            int alives;
            int equal = 0;
            while (true)
            {
                alives = 0;

                //обнулили временное поле
                tempField = mm.Field.Clone();

                for (int i = 0; i < mm.Y; i++)
                {
                    tempField.Rows.Add(tempField.NewRow());
                }

                //основной алгоритм
                alives = MainAlgorithm(alives);

                //check if field not changing
                if (prevHash==currHash)
                {
                    equal++;
                }
                else
                {
                    equal = 0;
                    mm.Field = tempField.Copy();
                }
                prevHash = currHash;
                currHash = 0;

                Thread.Sleep(mm.Delay);

                //game over
                if (alives == 0)
                {
                    global::System.Windows.MessageBox.Show("All are dead");
                    break;
                }

                if (equal >= 3)
                {
                    global::System.Windows.MessageBox.Show("Final generation. Equilibrium reached");
                    break;
                }
            }
        }

        private int MainAlgorithm(int alives)
        {
            for (int i = 0; i < mm.X; i++)
            {
                for (int j = 0; j < mm.Y; j++)
                {
                    int alive = AliveCount(i, j);

                    //проверяем мертвые клетки
                    if (mm.Field.Rows[i][j] == DBNull.Value)
                    {
                        //если рядом ровно три живых - оживает
                        if (alive == 3)
                        {
                            tempField.Rows[i][j] = 1;
                            currHash += j + i * mm.X;
                        }
                    }
                    //проверяем живые клетки
                    else
                    {
                        //если рядом 2 или 3 живых - остается жить
                        if ((alive == 2) || (alive == 3))
                        {
                            tempField.Rows[i][j] = 1;
                            currHash += j + i * mm.X;
                        }
                        alives++;
                    }
                }
            }
            return alives;
        }

        internal void Init()
        {
            mm.Field = new DataTable();

            for (int i = 0; i < mm.X; i++)
            {
                mm.Field.Columns.Add();
            }
            for (int i = 0; i < mm.Y; i++)
            {
                mm.Field.Rows.Add();
            }

            //fill
            Random r = new Random();
            for (int i = 0; i < mm.Number; i++)
            {
                int rX = r.Next(0, mm.X);
                int rY = r.Next(0, mm.Y);

                while (mm.Field.Rows[rX][rY] != DBNull.Value)
                {
                    rX = r.Next(0, mm.X);
                    rY = r.Next(0, mm.Y);
                }

                mm.Field.Rows[rX][rY] = 1;
            }
        }

        private int AliveCount(int i, int j)
        {
            int aliveCount = 0;
            if (mm.Field.Rows[(i == 0 ? mm.X - 1 : i) - 1][(j == 0 ? mm.Y - 1 : j) - 1] != DBNull.Value)
                aliveCount++;
            if (mm.Field.Rows[(i == 0 ? mm.X - 1 : i) - 1][j] != DBNull.Value)
                aliveCount++;
            if (mm.Field.Rows[(i == 0 ? mm.X - 1 : i) - 1][(j == mm.Y - 1 ? 0 : j) + 1] != DBNull.Value)
                aliveCount++;
            if (mm.Field.Rows[i][(j == 0 ? mm.Y - 1 : j) - 1] != DBNull.Value)
                aliveCount++;
            if (mm.Field.Rows[i][(j == mm.Y - 1 ? 0 : j) + 1] != DBNull.Value)
                aliveCount++;
            if (mm.Field.Rows[(i == mm.X - 1 ? 0 : i) + 1][(j == 0 ? mm.Y - 1 : j) - 1] != DBNull.Value)
                aliveCount++;
            if (mm.Field.Rows[(i == mm.X - 1 ? 0 : i) + 1][j] != DBNull.Value)
                aliveCount++;
            if (mm.Field.Rows[(i == mm.X - 1 ? 0 : i) + 1][(j == mm.Y - 1 ? 0 : j) + 1] != DBNull.Value)
                aliveCount++;

            return aliveCount;
        }
    }
}
