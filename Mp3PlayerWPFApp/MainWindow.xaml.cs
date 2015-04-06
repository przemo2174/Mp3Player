using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using Mp3PlayerWPF;
using SongClass;


namespace Mp3PlayerWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Mp3Player mp3Player = new Mp3Player();
        private DispatcherTimer timer;
        private bool dragStarted = false;
        private bool isPlaying = false;
        private bool songLoaded = false;
        MemoryStream stream;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            sldVolume.Value = mp3Player.Volume;
            stream = new MemoryStream();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            if(mp3Player.CurrentSong != null) // Jesli mamy załadowana juz jakąs piosenke i chcemy otworzyc następną, to najpierw oczyszczamy stream poprzedniej i otwieramy nowy
            {
                stream.Dispose();
                stream.Close();
                stream = new MemoryStream();
            }

            mp3Player.OpenSong();
            isPlaying = true;
            txbl.Text = mp3Player.CurrentSong.SongName;
            txbLyrics.Text = mp3Player.CurrentSong.SongLyrics;
            timer.Start();
                
            img.Stretch = Stretch.Fill;
            img.Source = mp3Player.LoadImageForCurrentSong(stream);
            songLoaded = true;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (songLoaded)
            {
                mp3Player.Play();
                timer.Start();
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mp3Player.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mp3Player.Stop();
            timer.Stop();
            lblTime.Content = "00:00";
            sldTime.Value = 0; 
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if(mp3Player.CurrentPositionString == mp3Player.CurrentSongDurationString)
            {
                sldTime.Value = 0;
                lblTime.Content = "00:00";
            }
            lblDuration.Content = mp3Player.CurrentSongDurationString;
            lblTime.Content = mp3Player.CurrentPositionString;
            if (mp3Player.CurrentPosition != 0 && dragStarted != true)
                sldTime.Value = sldTime.Maximum / mp3Player.CurrentSongDuration * mp3Player.CurrentPosition;
        }

        private void sldTime_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isPlaying)
            {
                mp3Player.CurrentPosition = mp3Player.CurrentSongDuration / sldTime.Maximum * sldTime.Value;
                lblTime.Content = mp3Player.CurrentPositionString;
            }
        }
        
        private void sldVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mp3Player.Volume = (int)sldVolume.Value;
        }

        private void sldTime_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            if (isPlaying)
            {
                dragStarted = true;
                lblTime.Content = mp3Player.CurrentPositionString;
            }
        }

        private void sldTime_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (isPlaying)
            {
                mp3Player.CurrentPosition = mp3Player.CurrentSongDuration / sldTime.Maximum * sldTime.Value;
                lblTime.Content = mp3Player.CurrentPositionString;
                dragStarted = false;
            }
        }
    }
}
