using System;
using System.Collections.Generic;
using System.Text;

namespace Fraser.Mahjong
{
    public class HKOSScoringPattern
    {
        public HKOSScoringPattern(string name, int value, string description)
        {
            Name = name;
            Value = value;
            Description = description;
        }

        public string Name { get; set; }

        public int Value { get; set; }

        public string Description { get; set; }

        //public string Name { get; set; }
    }
}
