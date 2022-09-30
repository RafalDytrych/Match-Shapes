using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchLine
{
    public class StartShapeSlotsManager : MonoBehaviour
    {
        [SerializeField] private ShapesManager _shapesManager;
        [SerializeField] private BgSlotsManager _bgSlotsManager;
        [SerializeField] private GameObject _bgPrefab; 
        [SerializeField] private GameManager _gameManager;
        [field: SerializeField] public Vector2[] StartSlotsPosition { get; private set; } //In default there are 3 setted up start slots in inspector, holds middle position of shape
        [field: SerializeField] public int MadeFills { get; private set; } //How many fills we made
        [field: SerializeField] public List<SquareShape> ShapesToMove { get; private set; } //Current shapes we can move
        private List<SquareShape> _shapesToDestroy; //Shapes to destroy
        private Transform _myTransform;
        private bool _isCreated; //if board is created
        private void Awake()
        {
            ShapesToMove = new List<SquareShape>();
            _shapesToDestroy = new List<SquareShape>();
            _gameManager.OnRoundPaused += b => MakeSquareShapesMovable(b);
            _gameManager.OnRoundStarted += b =>
            {
                if (b)
                {
                    ClearBoard();
                    CreateNewBoard();
                }
                
            };

            _gameManager.OnRoundRestarted += () =>
            {
                ClearBoard();
                Fill();
            };
            _myTransform = transform;
        }

        private void ClearBoard()
        {
            for (int i = 0; i < ShapesToMove.Count; i++)
            {
                if (ShapesToMove[i] != null)
                    Destroy(ShapesToMove[i].gameObject);
            }
        }

        private void CreateNewBoard()
        {
            Fill();
            if (_isCreated)
                return;
            CreateStartSlots();
            _isCreated = true;
        }

        private void MakeSquareShapesMovable(bool b)
        {
            for (int i = 0; i < ShapesToMove.Count; i++)
            {
                if (ShapesToMove[i] != null)
                    ShapesToMove[i].Lock(!b);
            }
        }

        private void CreateStartSlots()
        {
            for (int z = 0; z < StartSlotsPosition.Length; z++)
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        Instantiate(_bgPrefab, new Vector2(StartSlotsPosition[z].x + i, StartSlotsPosition[z].y + j),
                            Quaternion.identity, _myTransform);
                    }
                }
            }
        }
        public void Fill()
        {
            for (int i = 0; i < 3; i++)
            {
                SquareShape shape = _shapesManager.GetShape();
                shape.Lock(true);
                shape.SetPosition(StartSlotsPosition[i]);
                ShapesToMove.Add(shape);
            }

            MadeFills++;
        }

        public void OneLess(SquareShape shape)
        {
            ShapesToMove.Remove(shape);
            for (int i = ShapesToMove.Count-1; i >=0; i--)
            {
                if (ShapesToMove[i] == null) 
                    ShapesToMove.RemoveAt(i);
            }
            if(ShapesToMove.Count == 0)
                Fill();

            _shapesToDestroy.Add(shape);
            for (int i = _shapesToDestroy.Count - 1; i > 0; i--)
            {
                bool isEmpty = true;
                for (int j = 0; j < _shapesToDestroy[i].Squares.Length; j++)
                {
                    if(_shapesToDestroy[i].Squares[j] != null)
                    {
                        isEmpty = false;
                        break;
                    }
                }
                if (isEmpty)
                {
                    GameObject go = _shapesToDestroy[i].gameObject;
                    _shapesToDestroy.RemoveAt(i);
                    Destroy(go);
                    
                }
            }
        }
    }
}