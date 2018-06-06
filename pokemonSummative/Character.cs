using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace pokemonSummative
{
    public class Character
    {
        public int x, y, size;
        Image[] playerImages;
        int imageIndex;
        string message;

        public Character(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;
        }

        public void Move(string _direction, int _moveSpeed)
        {
            if (_direction == "Left")
            {
                GameScreen.screenX -= _moveSpeed;
            }
            else if (_direction == "Right")
            {
                GameScreen.screenX += _moveSpeed;
            }
            else if (_direction == "Up")
            {
                GameScreen.screenY -= _moveSpeed;
            }
            else if (_direction == "Down")
            {
                GameScreen.screenY += _moveSpeed;
            }
        }
    }
}
