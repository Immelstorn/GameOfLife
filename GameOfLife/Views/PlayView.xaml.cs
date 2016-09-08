using System.Threading;
using System.Windows;
using System.Windows.Controls;
using GameOfLife.Models;
using GameOfLife.ViewModels;

namespace GameOfLife.Views
{
    public partial class PlayView : Window
    {
        private MainModel mm;
        private Thread thr;

        public PlayView(MainModel mainModel)
        {
            mm = mainModel;
            this.DataContext = mm;

            MainViewModel mvm = new MainViewModel(mm, this);

            mvm.Init();

            InitializeComponent();

            Dg.HeadersVisibility = DataGridHeadersVisibility.None;

            thr = new Thread(mvm.Play) { Name = "PlayThread" };
            thr.Start();
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            thr.Abort();
        }

    }
}
