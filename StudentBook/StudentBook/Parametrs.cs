using System;
using System.Collections.Generic;
using System.Text;

namespace StudentBook
{
    public class Parametrs
    {
        public bool Sounds { get; set; }
        public bool Notices { get; set; }
        public string Language { get; set; }
        public int Count { get; set; }

        public Parametrs()
        {
            Sounds = false;
            Notices = false;
            Language = "English";
            Count = 5;
        }
        public Parametrs(bool sounds, bool notices, string language, int count)
        {
            Sounds = sounds;
            Notices = notices;
            Language = language;
            Count = count;
        }
    }
}
