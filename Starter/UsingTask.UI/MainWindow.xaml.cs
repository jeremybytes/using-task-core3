using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using UsingTask.Library;
using UsingTask.Shared;

namespace UsingTask.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void FetchWithTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FetchWithAwaitButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearListBox()
        {
            PersonListBox.Items.Clear();
        }
    }
}
