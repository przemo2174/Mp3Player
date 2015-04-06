using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using WMPLib;


namespace SongClass
{
    public class Song
    {
        private Mp3Lib.Mp3File file;
        public string SongDuration {get; set;}
        public string SongDurationToString { get; set; }
        public string SongPath { get; set; }
        public string SongName { get; set; }
        public Image SongPicture { get; set; }
        public string SongLyrics { get; set; }
        public string SongArtist { get; set; }
        public string SongAlbum { get; set; }
        

        public Song(string filePath, WindowsMediaPlayer mediaPlayer)
        {
            SongPath = filePath;
            file = new Mp3Lib.Mp3File(SongPath);
            SongName = file.TagHandler.Title;
            SongPicture = file.TagHandler.Picture;
            SongLyrics = file.TagHandler.Lyrics;
            SongArtist = file.TagHandler.Artist;
            SongAlbum = file.TagHandler.Album;
        }

       
    }
}
