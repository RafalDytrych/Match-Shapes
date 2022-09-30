using UnityEngine;

namespace MatchLine
{
    public class Square : MonoBehaviour
    {
        private Transform _myTransform;

        private void Awake()
        {
            _myTransform = transform;
        }

        public Vector2Int IntPosition
        {
            get
            {
                var position = _myTransform.position;
                return new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
            }
        }

        public Vector2 Position
        {
            get => _myTransform.position;
        }
        
        public void UpdatePosition() =>
            _myTransform.position = new Vector3(IntPosition.x, IntPosition.y);
        
    }
}
