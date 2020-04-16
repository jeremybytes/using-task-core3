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
        PersonReader reader = new PersonReader();
        CancellationTokenSource tokenSource;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void FetchWithTaskButton_Click(object sender, RoutedEventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            FetchWithTaskButton.IsEnabled = false;
            ClearListBox();

            Task<List<Person>> peopleTask = reader.GetAsync(tokenSource.Token);
            peopleTask.ContinueWith(task =>
            {
                switch (task.Status)
                {
                    case TaskStatus.RanToCompletion:
                        List<Person> people = task.Result;
                        foreach (var person in people)
                            PersonListBox.Items.Add(person);
                        break;
                    case TaskStatus.Canceled:
                        MessageBox.Show("CANCELED");
                        break;
                    case TaskStatus.Faulted:
                        foreach (var ex in task.Exception.Flatten().InnerExceptions)
                            MessageBox.Show($"ERROR\n{ex.Message}");
                        break;
                }

                FetchWithTaskButton.IsEnabled = true;
            },
            TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void FetchWithAwaitButton_Click(object sender, RoutedEventArgs e)
        {
            tokenSource = new CancellationTokenSource();
            FetchWithAwaitButton.IsEnabled = false;
            try
            {
                ClearListBox();

                List<Person> people = await reader.GetAsync(tokenSource.Token);
                foreach (var person in people)
                    PersonListBox.Items.Add(person);
            }
            catch (OperationCanceledException ex)
            {
                MessageBox.Show($"CANCELED\n{ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR\n{ex.Message}");
            }
            finally
            {
                FetchWithAwaitButton.IsEnabled = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            tokenSource.Cancel();
        }

        private void ClearListBox()
        {
            PersonListBox.Items.Clear();
        }
    }
}
