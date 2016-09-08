using System;
using System.Windows;
using GameOfLife.Models;
using GameOfLife.Views;

namespace GameOfLife
{
    public partial class MainWindow : Window
    {
        MainModel mm = new MainModel();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = mm;
            mm.X = 10;
            mm.Y = 10;
            mm.Number = 20;
            mm.Delay = 100;
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            PlayView pv = new PlayView(mm);
            pv.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
