using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchLine
{
    public class CheckBoardSquares : MonoBehaviour
    {
        [SerializeField] private StartShapeSlotsManager _startShapeSlotsManager;

        public bool IsPossibleMove(Square[,] squares) //is there place on board for shapes to move?
        {
            if (_startShapeSlotsManager.ShapesToMove.Count == 0)
                return true;
            for (int shapes = 0; shapes < _startShapeSlotsManager.ShapesToMove.Count; shapes++)  //loop throught shapes we can still move
            {
                if (_startShapeSlotsManager.ShapesToMove[shapes] == null) //if shape is null go to next iteration
                    continue;
                for (int x = 0; x <= Board.Width; x++)
                {
                    for (int y = 0; y <= Board.Height; y++) //loop through board size
                    {
                        if (IsTherePlaceForSquare(squares, x, y,
                                _startShapeSlotsManager.ShapesToMove[shapes].SquaresPositions))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsTherePlaceForSquare(Square[,] squares, int x, int y, Vector2Int[] vector2Ints)
        {
            for (int j = 0; j < vector2Ints.Length; j++)
            {
                int posX = x + vector2Ints[j].x;
                int posY = y + vector2Ints[j].y;

                if (posX < 0 || posX >= Board.Width || posY < 0 || posY >= Board.Height) //if checked position is outside bounds return false
                    return false;
                if (squares[posX, posY] != null) //if there is square at position return false
                    return false;
            }
            return true; //if we got to this point it means there is place for shape
        }
    }
}