using UnityEngine;

namespace MatchLine
{
    public class SquareShape : MonoBehaviour
    {
        [field: SerializeField] public Square[] Squares { get; private set; } //children squares
        [field: SerializeField] public Vector2Int[] SquaresPositions { get; private set; } //children squares positions
        [field: SerializeField] public bool IsLocked { get; private set; } //is locked? trye = cannot move
        [field: SerializeField] public GameObject MyGameObject { get; private set; } //game object reference
        private Vector2Int _myCurrentIntPosition; //my current int position
        private Vector2Int _previousIntPosition; //previos int position        

        private Transform _myTransform;
        private BoxCollider2D _collider;
        private Vector3 _positionDifference;
        private Vector3 _previousPosition;
        private Vector3 _currentMousePosition;
        private Vector3 _startPosition;

        //References
        private Board _board;
        private ShapesManager _shapesManager;
        public void ChangeColliderStatus(bool status) => _collider.enabled = status;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            MyGameObject = gameObject;
            _myTransform = transform;
            _startPosition = _myTransform.position;
            if (_shapesManager == null)
                return;
            if (_shapesManager.SquaresSpeedOffset <= 0)
                _shapesManager.SquaresSpeedOffset = 3f;
        }

        public void Initialize(Board board, ShapesManager shapesManager) //Initialize shape on instantiate
        {
            _board = board;
            _shapesManager = shapesManager;
        }

        public void OnMouseDown()
        {
            _previousPosition = ConvertScreenToWorldPoint();
        }

        public void OnMouseUp()
        {
            DragEnded();
        }

        public void OnMouseDrag()
        {
            if (IsLocked) //if is locked block cannot move
                return;
            _currentMousePosition = ConvertScreenToWorldPoint(); 
            _positionDifference = _previousPosition - _currentMousePosition;
            Move(-_positionDifference);
            _previousPosition = _currentMousePosition;

            Vector3 position = _myTransform.position;
            int intX = Mathf.RoundToInt(position.x);
            int intY = Mathf.RoundToInt(position.y);
            _myCurrentIntPosition = new Vector2Int(intX, intY);
            if (_myCurrentIntPosition == _previousIntPosition)
                return;
            _previousIntPosition = _myCurrentIntPosition;
            _board.CheckForMarks(intX, intY, this, AreInBounds()); //Check on board after move made
        }

        private Vector3 ConvertScreenToWorldPoint()
        {
            Vector3 mousePos = Input.mousePosition;
            return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -10));
        }

        private void DragEnded()
        {
            _board.ClearMarks(); //Clear previous marks on board
            if (!AreInBounds() || !_board.CheckBoard(Squares)) 
                _myTransform.position = _startPosition;
            else
                _board.UpdateBoard(this);
        }
        private void Move(Vector3 dir) => _myTransform.position -=
            new Vector3(dir.x, dir.y, 0) * _shapesManager.SquaresSpeedOffset; //We multiply to move more than we moved coursor        

        private bool AreInBounds() //Are squares in board bounds?
        {
            for (var index = 0; index < Squares.Length; index++)
            {
                var item = Squares[index];
                Vector3 position = item.Position;
                bool inBounds = position.x < -0.3f || position.x > Board.Width - 0.7f || position.y < -0.3f ||
                                position.y > Board.Height - 0.7f;
                if (inBounds)
                    return false;
            }
            return true;
        }

        public void Lock(bool canBeMoved) //On locked status changed
        {
            ChangeColliderStatus(canBeMoved);
            IsLocked = !canBeMoved;
        }

        public void SetPosition(Vector2 position)
        {
            _startPosition = position;
            _myTransform.position = position;
            this.gameObject.On();
        }
    }
}