using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using SongClass;
using WMPLib;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Mp3PlayerClass
{
    public class Mp3Player
    {
        private WindowsMediaPlayer player;
        private Song currentSong;

        public Song CurrentSong 
        {
            get { return currentSong; } 
        }
        public int Volume
        {
            get { return player.settings.volume; }
            set
            {
                if (value >= 0 && value <= 100 && player.settings.get_isAvailable("Volume"))
                    player.settings.volume = value;
            }

        }
        public double CurrentPosition
        {
            get { return player.controls.currentPosition; }
            set { player.controls.currentPosition = value; }
        }
        public string CurrentPositionString
        {
            get { return player.controls.currentPositionString; }
        }
        public double CurrentSongDuration 
        { 
            get { return player.currentMedia.duration; } 
        }
        public string CurrentSongDurationString 
        { 
            get { return player.currentMedia.durationString; }
        }
        public WMPPlayState PlayerState
        {
            get { return player.playState; }
        }

        public Mp3Player()
        {
            player = new WindowsMediaPlayer();
            Volume = 70;
        }

        public void OpenSong()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Mp3 files|*.mp3|All Files(*.*)|*.*";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                player.URL = openFile.FileName;
                currentSong = new Song(player.URL, player);
                if(player.controls.get_isAvailable("play"))
                {
                    player.controls.play();
                }
            }
        }

        public void Play()
        {
            if (player.controls.get_isAvailable("play"))
            {
                player.controls.play();
            }
        }

        public void Stop()
        {
            if (player.controls.get_isAvailable("stop"))
            {
                player.controls.stop();
            }
        }

        public void Pause()
        {
            if (player.controls.get_isAvailable("pause"))
            {
                player.controls.pause();
            }
        }
        public BitmapImage LoadImageForCurrentSong(MemoryStream memoryStream)
        {
            CurrentSong.SongPicture.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            memoryStream.Position = 0;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = memoryStream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }

    }
}
