using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataGenerator
{
    class Genre
    {
        public bool Indie { get; set; }
        public bool Metal { get; set; }
        public bool Folk { get; set; }
        public bool Rock { get; set; }
        public bool Pop { get; set; }
        public bool Jazz { get; set; }

        // get genres
        private int count = 2;
        int randNum;
        Random rnd = new Random();

        public bool genreBoolGen()
        {
            randNum = (rnd.Next() % count++);
            if (randNum == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Genre()
        {
            this.Indie = genreBoolGen();
            this.Metal = genreBoolGen();
            this.Folk = genreBoolGen(); ;
            this.Pop = genreBoolGen();
            this.Rock = genreBoolGen();
            this.Jazz = genreBoolGen();
        }
    }
}