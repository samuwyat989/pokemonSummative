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

        public Boundary(int _xTileIndex, int _yTileIndex, int _tileWidth, int _tileHeight, string _message)//int _x, int _y, int _width, int _height)
        {
            xTileIndex = _xTileIndex; 
            yTileIndex = _yTileIndex;
            tileWidth = _tileWidth;
            tileHeight = _tileHeight;
            message = _message;
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
                case "Right"://left player collision
                    if (_player.x == GameScreen.lineXVals[xTileIndex] + GameScreen.tileSize*tileWidth &&
                        _player.y >= GameScreen.lineYVals[yTileIndex] && _player.y < GameScreen.lineYVals[yTileIndex + tileHeight]
                        || bound && _player.x == GameScreen.lineXVals[0]) //bound
                    {
                        edge = true;
                    }
                    break;
                case "Left"://right player collision
                    if(_player.x + _player.size == GameScreen.lineXVals[xTileIndex] &&
                       _player.y >= GameScreen.lineYVals[yTileIndex] && _player.y < GameScreen.lineYVals[yTileIndex + tileHeight]
                       || bound && _player.x + _player.size == GameScreen.lineXVals[GameScreen.gameRegions[GameScreen.gameRegionName].Width])
                    {
                        edge = true;
                    }
                    break;
                case "Up"://bottom player collision
                    if (_player.y + _player.size == GameScreen.lineYVals[yTileIndex] &&
                        _player.x >= GameScreen.lineXVals[xTileIndex] && _player.x < GameScreen.lineXVals[xTileIndex + tileWidth]
                        || bound && _player.y + _player.size == GameScreen.lineYVals[GameScreen.gameRegions[GameScreen.gameRegionName].Height])
                    {
                        edge = true;
                    }
                    break;
                case "Down"://top player collision
                    if (_player.y == GameScreen.lineYVals[yTileIndex] + GameScreen.tileSize * tileHeight &&
                        _player.x >= GameScreen.lineXVals[xTileIndex] && _player.x < GameScreen.lineXVals[xTileIndex + tileWidth]
                        || bound && _player.y == GameScreen.lineYVals[0])//bound
                    {
                        edge = true;
                    }
                    break;
            }
            return edge;
        }
    }
}
