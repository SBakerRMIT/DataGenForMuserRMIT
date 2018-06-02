using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataGenerator
{
    class Instrument
    {
        public bool LeadGuitar { get; set; }
        public bool RhythmGuitar { get; set; }
        public bool Vocals { get; set; }
        public bool Bass { get; set; }
        public bool Keyboard { get; set; }
        public bool Drums { get; set; }

        // get instruments
        private int count = 2;
        int randNum;
        Random rnd = new Random();

        public bool instrumentBoolGen()
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

        public Instrument()
        {
            this.LeadGuitar = instrumentBoolGen();
            this.RhythmGuitar = instrumentBoolGen();
            this.Vocals = instrumentBoolGen();
            this.Bass = instrumentBoolGen(); ;
            this.Drums = instrumentBoolGen();
            this.Keyboard = instrumentBoolGen();
        }
    }
}