
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MatchLine
{
    public class ShapesManager : MonoBehaviour
    {
        
        [field: SerializeField] public SquareShape[] ShapesPrefabs { get; private set; }
        [field: SerializeField] public List<SquareShape> ShapesInUse { get; private set; }
        [field: SerializeField] public List<SquareShape> PoolShapes { get; private set; }
        [field: SerializeField] public float SquaresSpeedOffset { get; set; }
        [SerializeField] private Transform _parent;
        [SerializeField] private Board _board;
        [SerializeField] private StartShapeSlotsManager _startShapeSlotsManager;
        [SerializeField] private GameManager _gameManager;
        private void Awake()
        {
            ShapesInUse = new List<SquareShape>();
            PoolShapes = new List<SquareShape>();
            _gameManager.OnRoundStarted += b =>
            {
                if (b)
                {
                    CreateNewBoard();
                }               
            };
            GeneratePool(100); //pool limited to 100 shapes
            _gameManager.OnRoundRestarted += CreateNewBoard;
            _gameManager.OnPlayerDataInitialized += DataInitialized;
        }

        private void DataInitialized(PlayerData playerData)
        {
            SquaresSpeedOffset = playerData.SquaresSpeedOffset;
            if (SquaresSpeedOffset < 1)
                SquaresSpeedOffset = 3;


        }

        private void CreateNewBoard()
        {
            int count = PoolShapes.Count;
            if (count is < 100 and >= 1)
                GeneratePool(100-count);
        }

        public SquareShape GetShape()
        {
            int index = UnityEngine.Random.Range(0, PoolShapes.Count);
            SquareShape shape = PoolShapes[index];
            PoolShapes.RemoveAt(index);
            if (PoolShapes.Count < 19)
                GeneratePool(15);
            return shape;
        }

        private void GeneratePool(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                SquareShape shape = Instantiate(ShapesPrefabs[UnityEngine.Random.Range(0, ShapesPrefabs.Length)],
                    _parent);
                shape.Initialize(_board, this);
                PoolShapes.Add(shape);
                shape.MyGameObject.Off();
            }
        }
    }
}
