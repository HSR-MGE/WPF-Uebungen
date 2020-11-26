namespace Aufgabe_1
{
    using System;
    using System.ComponentModel;
    using System.Windows.Threading;

    public class Clock : INotifyPropertyChanged
    {
        private readonly DispatcherTimer _timer;
        private readonly TimeSpan _offset;

        // Timer wird injected, damit wir einen "globalen" Taktgeber haben
        public Clock(DispatcherTimer timer, TimeSpan offset)
        {
            _timer = timer;
            _timer.Tick += DoOnTimerTicked;

            _offset = offset;
        }

        public string TimeString { get; private set; } = "??.??.???? ??:??:??";

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Private Helper Methods

        private void DoOnTimerTicked(object sender, EventArgs e)
        {
            var time = DateTime.UtcNow.Add(_offset);
            TimeString = time.ToString("dd.MM.yyyy HH:mm:ss");
            OnPropertyChanged(nameof(TimeString));
        }

        #endregion
    }
}
