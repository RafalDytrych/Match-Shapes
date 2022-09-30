using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchLine
{
    /// <summary>
    /// Game logic, main class
    /// </summary>
    public class Board : MonoBehaviour
    {
        public static int Width;
        public static int Height;

        [SerializeField] private BgSlotsManager _bgSlotsManager;
        [SerializeField] private StartShapeSlotsManager _startShapeSlotsManager;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private CheckBoardSquares _checkBoardSquares;
        [SerializeField] private Scores _scores;
        [SerializeField] private SquaresSound _squaresSound;
        [SerializeField] private SquaresAnimationManager _squaresAnimationManager;

        private Square[,] _squares; //Hold game table
        private List<int> _columnsToCheck; 
        private List<int> _rowsToCheck;

        private void Start()
        {
            _squares = new Square[Width, Height];
        }
        private void Awake()
        {
            Width = 11;
            Height = 10;
            _columnsToCheck = new List<int>();
            _rowsToCheck = new List<int>();
            _gameManager.OnRoundStarted += b =>
            {
                if (b)
                {
                    ClearBoard();
                }                
            };
            _gameManager.OnRoundRestarted += ClearBoard;
        }

        private void ClearBoard()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if(_squares[i,j] != null)
                        Destroy(_squares[i,j].gameObject);
                }
            }
        }

        public bool CheckBoard(Square[] squares)
        {
            foreach (var item in squares)
            {
                if (_squares[item.IntPosition.x, item.IntPosition.y] != null)
                    return false;                
            }
            return true;
        }

        public void UpdateBoard(SquareShape shape)
        {
            _columnsToCheck.Clear();
            _rowsToCheck.Clear();
            shape.Lock(false);
            _squaresSound.PlayPutOnShape();
            foreach (var item in shape.Squares) //Update block squares on the board
            {
                int column = item.IntPosition.x;
                int row = item.IntPosition.y;
                _squares[column, row] = item;
                item.UpdatePosition();
                
                if (!_columnsToCheck.Contains(column))
                    _columnsToCheck.Add(column);
                if (!_rowsToCheck.Contains(row))
                    _rowsToCheck.Add(row);
            }

            _startShapeSlotsManager.OneLess(shape);
            CheckForCompleteLine(shape);
            if (!_checkBoardSquares.IsPossibleMove(_squares))
            {
                _gameManager.RoundEnded();
            }
        }

        private void CheckForCompleteLine(SquareShape shape)
        {
            for (int i = _columnsToCheck.Count-1; i >= 0; i--)
            {
                for (int j = 0; j < Board.Height; j++)
                {
                    if (_squares[_columnsToCheck[i],j] == null)
                    {
                        _columnsToCheck.RemoveAt(i);
                        break;
                    }
                }
            }
            
            for (int i = _rowsToCheck.Count-1; i >= 0; i--)
            {
                for (int j = 0; j < Board.Width; j++)
                {
                    if (_squares[j, _rowsToCheck[i]] == null)
                    {
                        _rowsToCheck.RemoveAt(i);
                        break;
                    }
                }
            }
             
            _scores.CalculateIncomingPoints(_columnsToCheck.Count, _rowsToCheck.Count, shape.Squares.Length);

            if (_columnsToCheck.Count == 0 && _rowsToCheck.Count == 0)
                return;           
            DestroyColumns();
            DestroyRows();
            _squaresAnimationManager.Execute(shape);   
        }

        private void DestroyColumns()
        {
            
            for (int j = 0; j < _columnsToCheck.Count; j++)
            {
                for (int i = 0; i < Height; i++)
                {
                    Square square = _squares[_columnsToCheck[j], i];
                    if (square != null) 
                    {
                        _squaresAnimationManager.AnimateMe(square);
                        _squares[_columnsToCheck[j], i] = null;
                    }
                }
            }         
        }

        private void DestroyRows()
        {
            for (int j = 0; j < _rowsToCheck.Count; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    Square square = _squares[i, _rowsToCheck[j]];
                    if (square != null)
                    {
                        _squaresAnimationManager.AnimateMe(square);
                        _squares[i, _rowsToCheck[j]] = null;
                    }
                }
            }         
        }
        public void ClearMarks()
        {
            _bgSlotsManager.ClearMarks();
        }

        public void CheckForMarks(int x, int y, SquareShape squareShape, bool areNotInBounds)
        {
            _bgSlotsManager.MarkSlots(x, y,squareShape.SquaresPositions, areNotInBounds, _squares);
        }
    }
}
