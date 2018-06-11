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
        public int x, y, size, xTileIndex, yTileIndex;
        Image[] playerImages;
        int imageIndex;
        public string message, faceDirection;

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

        public bool IntersectNPC(Character _player, string _direction)
        {
            bool edge = false;

            switch (_direction)
            {
                case "Right"://left player collision
                    if (_player.x == GameScreen.lineXVals[xTileIndex] + GameScreen.tileSize &&
                        _player.y == GameScreen.lineYVals[yTileIndex])
                    {
                        edge = true;
                    }
                    break;
                case "Left"://right player collision
                    if (_player.x + _player.size == GameScreen.lineXVals[xTileIndex] &&
                        _player.y == GameScreen.lineYVals[yTileIndex])
                    {
                        edge = true;
                    }
                    break;
                case "Up"://bottom player collision
                    if (_player.y + _player.size == GameScreen.lineYVals[yTileIndex] &&
                        _player.x == GameScreen.lineXVals[xTileIndex])
                    {
                        edge = true;
                    }
                    break;
                case "Down"://top player collision
                    if (_player.y == GameScreen.lineYVals[yTileIndex] + GameScreen.tileSize &&
                        _player.x == GameScreen.lineXVals[xTileIndex])
                    {
                        edge = true;
                    }
                    break;
            }
            return edge;
        }

        public void MoveNPC(string _direction, int _moveSpeed)
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

        public bool CheckExit(List<Boundary> _boundaries)
        {
            bool exit = false;

            foreach(Boundary b in _boundaries)
            {
                if (x >= GameScreen.lineXVals[b.xTileIndex] && x < GameScreen.lineXVals[b.xTileIndex+ b.tileWidth] &&
                    y == GameScreen.lineYVals[b.yTileIndex] &&
                    b.message == "Exit")
                {
                    exit = true;
                    break;
                }
            }

            return exit;
        }
    }
}
