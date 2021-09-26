using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace ChordFileGenerator
{
    public class Class1
    {
        public string SongName { get; set; }

        public string Artist { get; set; }

        public FileType FileType { get; set; }

        private List<string> lines = new List<string>();

        public void ChordFileGenerator()
        {
            FileStream fs = new FileStream(@"C:\Training\EuroTraining\Files\lines.txt",FileMode.OpenOrCreate,FileAccess.Write);
            fs.Close();
        }
        public void AddLine(string lyrics)
        {
            lines.Add(lyrics);
        }
        public void AddLine(string chords,string lyrics)
        {
            lines.Add(chords);
            lines.Add(lyrics);
        }
        public void SaveFile(string filename)
        {
            string file_contents = GenerateFileContents();
            File.WriteAllText(@"C:\Training\EuroTraining\Files\lines.txt", file_contents);

        }

        private string GenerateFileContents()
        {
            StringBuilder contents = new StringBuilder();

            contents.AppendLine("Song Name: " + SongName);
            contents.AppendLine("Artist: " + Artist);

            switch (FileType)
            {
                case FileType.Chords:
                    contents.AppendLine("Type: Chords");
                    break;
                case FileType.Tab:
                    contents.AppendLine("Type: Tab");
                    break;
                default:
                    contents.AppendLine("Type: Lyrics");
                    break;
            }
            foreach (string line in lines)
                contents.AppendLine(line);

            return contents.ToString();
        }
    }
    public enum FileType
    {
        Chords,
        Tab,
        Lyrics
    }
}
