using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MatchLine
{
    public class BgSlotsManager : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [field: SerializeField] public BgSlot SlotPrefab { get; private set; }
        [SerializeField] private Board _board;
        [field: SerializeField] public Color NormalColor { get; private set; }
        [field: SerializeField] public Color MarkColor { get; private set; }
        [field: SerializeField] public Color IncorrectPositionColor { get; private set; }
        [SerializeField] private GameManager _gameManager;
        private BgSlot[,] _bgSlots;
        private List<BgSlot> _markedSlots;
        private bool _isCreated;

        private void Awake()
        {
            _markedSlots = new List<BgSlot>(); _gameManager.OnRoundStarted += b =>
            {
                if (b)
                    CreateNewBoard();
                else
                    ClearBoard();
            };
        }
        private void CreateNewBoard()
        {
            if (_isCreated)
                return;
            _bgSlots = new BgSlot[Board.Width, Board.Height];
            for (int i = 0; i < Board.Width; i++)
            {
                for (int j = 0; j < Board.Height; j++)
                {
                    _bgSlots[i, j] = Instantiate(SlotPrefab, new Vector3(i, j, 0),
                        Quaternion.identity, _parent);
                    _bgSlots[i, j].Initialize(this);
                }
            }
            _isCreated = true;
        }

        private void ClearBoard()
        {
            ClearMarks();
        }
        public void MarkSlots(int x, int y, Vector2Int[] newSlotsToMark, bool areInBounds, Square[,] _squares)
        {
            ClearMarks();

            bool inBounds = true;
            bool slotIsNotEmpty = true;

            int posX;
            int posY;
            for (int i = 0; i < newSlotsToMark.Length; i++)
            {
                posX = x + newSlotsToMark[i].x;
                posY = y + newSlotsToMark[i].y;
                if (posX < 0 || posX >= Board.Width || posY < 0 || posY >= Board.Height)
                {
                    inBounds = false;
                }
                else if (_squares[posX, posY] != null)
                {
                    slotIsNotEmpty = false;
                }
            }

            if(inBounds && !slotIsNotEmpty)
            {
                for (int i = 0; i < newSlotsToMark.Length; i++)
                {
                    posX = x + newSlotsToMark[i].x;
                    posY = y + newSlotsToMark[i].y;
                    BgSlot slot = _bgSlots[posX, posY] = _bgSlots[posX, posY];
                    slot.IncorrectPosition();
                    _markedSlots.Add(slot);
                }
                return;
            }



            if (inBounds)
            {
                for (int i = 0; i < newSlotsToMark.Length; i++)
                {
                    posX = x + newSlotsToMark[i].x;
                    posY = y + newSlotsToMark[i].y;
                    BgSlot slot = _bgSlots[posX, posY] = _bgSlots[posX, posY];
                    slot.MarkMe();
                    _markedSlots.Add(slot);
                }
                return;
            }
            else
            {
                for (int i = 0; i < newSlotsToMark.Length; i++)
                {
                    posX = x + newSlotsToMark[i].x;
                    posY = y + newSlotsToMark[i].y;
                    if (posX >= 0 && posX < Board.Width && posY >= 0 && posY < Board.Height)
                    {
                        BgSlot slot = _bgSlots[posX, posY];
                        slot.IncorrectPosition();
                        _markedSlots.Add(slot);
                    }
                }
                return;
            }
        }

        public void ClearMarks()
        {
            int length = _markedSlots.Count;
            for (int i = 0; i < length; i++)
            {
                _markedSlots[i].UnMarkMe();
            }

            _markedSlots.Clear();
        }
    }
}