using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
        private bool userIsDraggingVolume = false;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer prgTimer = new DispatcherTimer();
            prgTimer.Interval = TimeSpan.FromSeconds(1);
            prgTimer.Tick += timer_Tick;
            prgTimer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if((mediaElement.Source != null) && (mediaElement.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                slider.Minimum = 0;
                slider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                slider.Value = mediaElement.Position.TotalSeconds;
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            var dialogBox = new OpenFileDialog();
            dialogBox.Title = "Select Media";
            dialogBox.Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*";
            if(dialogBox.ShowDialog() == true)
            {
                mediaElement.Source = new Uri(dialogBox.FileName);
            }


        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mediaElement.Position = TimeSpan.FromSeconds(slider.Value);
        }
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            label.Content = TimeSpan.FromSeconds(slider.Value).ToString(@"hh\:mm\:ss");
        }

        private void volume_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingVolume = true;
        }

        private void volume_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingVolume = false;
            mediaElement.Volume = (double)volume.Value;
        }
    }
}
