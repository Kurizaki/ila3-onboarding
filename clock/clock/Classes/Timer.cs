using System;
using System.Timers;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.IO;
using System.Media;

namespace clock.Classes
{
    public class Timer
    {
        private ComboBox _comboBoxTimerHours;
        private ComboBox _comboBoxTimerMinutes;
        private ComboBox _comboBoxTimerSeconds;
        private Label _labelTimer;

        private int _hours;
        private int _minutes;
        private int _seconds;

        private bool _isTimerRunning;
        private int _totalSeconds;

        private System.Timers.Timer _timer = new System.Timers.Timer(1000);

        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public Timer(ComboBox comboBoxTimerHours, ComboBox comboBoxTimerMinutes, ComboBox comboBoxTimerSeconds, Label labelTimer)
        {
            _isTimerRunning = false;
            _comboBoxTimerHours = comboBoxTimerHours;
            _comboBoxTimerMinutes = comboBoxTimerMinutes;
            _comboBoxTimerSeconds = comboBoxTimerSeconds;
            _labelTimer = labelTimer;
            _timer.Elapsed += Tick;
        }

        //CHAT GPT START
        private static void PlayChestnutsSound()
        {
            byte[] chestnutsWav = Properties.Resources.Chestnuts;
            using (MemoryStream ms = new MemoryStream(chestnutsWav))
            {
                SoundPlayer player = new SoundPlayer(ms);
                player.Play();
            }
        }
        //CHAT GPT END

        private async void Tick(object sender, ElapsedEventArgs e)
        {
            if (_totalSeconds > 0)
            {
                _totalSeconds--;
            }

            // CHAT GPT START
            // Calculate hours, minutes, and seconds remaining
            _hours = _totalSeconds / 3600;
            _minutes = (_totalSeconds % 3600) / 60;
            _seconds = _totalSeconds % 60;
            // CHAT GPT END

            await UpdateLabelAsync($"{_hours:d2}:{_minutes:d2}:{_seconds:d2}");

            if (_totalSeconds <= 0)
            {
                Stop();
                PlayChestnutsSound();
            }
        }

        private Task UpdateLabelAsync(string content)
        {
            return _labelTimer.Dispatcher.InvokeAsync(() =>
            {
                _labelTimer.Content = content;
            }).Task;
        }

        public void PopulateComboBoxes()
        {
            for (int i = 0; i < 24; i++)
            {
                _comboBoxTimerHours.Items.Add(i.ToString());
            }
            _comboBoxTimerHours.SelectedIndex = 0;

            for (int i = 0; i < 60; i++)
            {
                _comboBoxTimerMinutes.Items.Add(i.ToString());
            }
            _comboBoxTimerMinutes.SelectedIndex = 0;

            for (int i = 0; i < 60; i++)
            {
                _comboBoxTimerSeconds.Items.Add(i.ToString());
            }
            _comboBoxTimerSeconds.SelectedIndex = 0;
        }

        public void SelectionChanged()
        {
            _hours = _comboBoxTimerHours.SelectedIndex;
            _minutes = _comboBoxTimerMinutes.SelectedIndex;
            _seconds = _comboBoxTimerSeconds.SelectedIndex;

            _labelTimer.Content = $"{_hours:d2}:{_minutes:d2}:{_seconds:d2}";
        }

        public void StartStop()
        {
            player.Stop();
            if (_isTimerRunning) Stop();
            else Start();
        }

        private int CalculateTimerSeconds()
        {
            return _seconds + (_minutes * 60) + (_hours * 60 * 60);
        }

        private void Stop()
        {
            _isTimerRunning = false;
            _timer.Stop();
        }

        private async void Start()
        {
            _isTimerRunning = true;
            _totalSeconds = CalculateTimerSeconds();
            _timer.AutoReset = true;
            _timer.Start();
            _timer.Enabled = true;

            await UpdateLabelAsync($"{_hours:d2}:{_minutes:d2}:{_seconds:d2}");
        }
    }
}
