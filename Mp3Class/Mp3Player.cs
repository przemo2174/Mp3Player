using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;
using System.Windows.Forms;


namespace Mp3Class
{
    public class Mp3Player
    {
        WindowsMediaPlayer player;
        
        public string SongPath
        {
            get { return player.URL; }
        }
        public string SongName
        {
            get { return player.currentMedia.name; }
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
        public bool Mute 
        {
            get { return player.settings.mute; }
            set 
            {
                if(player.settings.get_isAvailable("Mute"))
                    player.settings.mute = value;
            }
        }
        public double SongCurrentPositionDouble
        {
            get { return player.controls.currentPosition; }
        }
        public string SongCurrentPositionString
        {
            get 
            {
                return player.controls.currentPositionString;
            }
        }
        public double SongDuration
        {
            get { return player.currentMedia.duration; }
        }

        public Mp3Player()
        {
            player = new WindowsMediaPlayer();
            Volume = 70;
        }
        public string OpenSong()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Mp3 files|*.mp3|All Files(*.*)|*.*";
            openFile.FilterIndex = 1;
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                player.URL = openFile.FileName;
                return player.URL;
            }
            return "Error";
        }

        public void PlaySong()
        {
            if(player.controls.get_isAvailable("play"))
                player.controls.play();
        }

        public void PauseSong()
        {
            if(player.controls.get_isAvailable("pause"))
                player.controls.pause();
        }

        public void StopSong()
        {
            if(player.controls.get_isAvailable("stop"))
                player.controls.stop();
        }

        public void FastForward()
        {
            player.controls.fastForward();
        }

        public void FastReverse()
        {
            player.controls.fastReverse();
        }


        
    }
}
