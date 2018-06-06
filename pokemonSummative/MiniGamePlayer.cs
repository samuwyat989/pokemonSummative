using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonSummative
{
    public class MiniGamePlayer
    {
        public int score, min, sec;
        public string name;

        public MiniGamePlayer(int _score, int _min, int _sec, string _name)
        {
            score = _score;
            min = _min;
            sec = _sec;
            name = _name;
        }
    }
}
