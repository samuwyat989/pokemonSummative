using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonSummative
{
    public class Boundary
    {
        public int x, y, width, height, xTileIndex, yTileIndex, tileWidth, tileHeight;
        public string message;

        public Boundary(int _xTileIndex, int _yTileIndex, int _tileWidth, int _tileHeight)//int _x, int _y, int _width, int _height)
        {
            xTileIndex = _xTileIndex; 
            yTileIndex = _yTileIndex;
            tileWidth = _tileWidth;
            tileHeight = _tileHeight;

            //x = _x;
            //y = _y;
            //width = _width;
            //height = _height;
        }

        public void Move(string _direction, int _moveSpeed)
        {
            if (_direction == "Left")
            {
                x -= _moveSpeed;
            }
            else if (_direction == "Right")
            {
                x += _moveSpeed;
            }
            else if (_direction == "Up")
            {
                y -= _moveSpeed;
            }
            else if (_direction == "Down")
            {
                y += _moveSpeed;
            }
        }

        public bool Intersect(Character _player, string _direction, bool bound)
        {
            bool edge = false;

            switch (_direction)
            {
                case "Right":
                    if (_player.x == GameScreen.lineXVals[xTileIndex] + GameScreen.tileSize*tileWidth)
                        //bound
                    {
                        edge = true;
                    }
                    break;
                case "Left":
                    if(_player.x + _player.size == GameScreen.lineXVals[xTileIndex] && _player.y == GameScreen.lineYVals[yTileIndex])
                        //|| bound && _player.x + _player.size == GameScreen.lineXVals[10])
                    {
                        edge = true;
                    }
                    break;
                case "Up":
                    if (_player.y + _player.size == GameScreen.lineYVals[yTileIndex] && _player.x == GameScreen.lineXVals[xTileIndex])
                        //bound
                    {
                        edge = true;
                    }
                    break;
                case "Down":
                    if (_player.y == GameScreen.lineYVals[yTileIndex] + GameScreen.tileSize * tileHeight)
                        //bound
                    {
                        edge = true;
                    }
                    break;
            }
            return edge;
        }
    }
}
