using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Aufgabe_1
{
    using System;
    using System.Windows;
    
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _timer;
        private readonly ObservableCollection<Clock> _clocks;

        public MainWindow()
        {
            InitializeComponent();

            _timer = CreateTimer();

            _clocks = new ObservableCollection<Clock>
            {
                new Clock(_timer, TimeSpan.Zero)
            };

            this.DataContext = _clocks;
        }

        private static DispatcherTimer CreateTimer()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Start();

            return timer;
        }

        private void AddClock_OnClick(object sender, RoutedEventArgs e)
        {
            var offset = int.Parse(TimeZone.Text);
            var newClock = new Clock(_timer, TimeSpan.FromHours(offset));
            _clocks.Add(newClock);
        }
    }
}
