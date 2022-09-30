using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchLine
{
    public class SquaresAnimationManager : MonoBehaviour
    {
        [SerializeField] private SquaresSound _squaresSound;
        [SerializeField] private List<Square> _squares;
        [SerializeField] private float _squareAnimOffset = 0.05f;
        private bool _isExecuting;

        public void AnimateMe(Square square)
        {
            _squares.Add(square);
        }

        public void Execute(SquareShape shape)
        {
            if (_isExecuting)
                return;
            StartCoroutine(DoAnim(shape));
        }

        private IEnumerator DoAnim(SquareShape shape) //We destroy squares from putted shape to the board bounds
        {
            if (_isExecuting)
                yield break;
            _isExecuting = true;

            _squaresSound.PlayDestroyLoop(true);
            Square square = null;
            Vector2 position;
            for (int i = 0; i < shape.Squares.Length; i++)
            {
                if (shape.Squares[i] != null)
                {
                    square = shape.Squares[i];
                    position = square.Position;
                    break;
                }
            }

            if (square == null)
                position = new Vector2(Board.Width / 2, Board.Height / 2);
                //yield break;

            _squares.Sort( //sort distance between shape putted to board bounds
                delegate(Square s1, Square s2)
                {
                    return Vector2.Distance(s2.Position, square.Position).CompareTo(Vector2.Distance(s1.Position, square.Position));
                });
            do
            {
                for (int i = _squares.Count - 1; i >= 0; i--)
                {
                    Destroy(_squares[i].gameObject);
                    _squares.RemoveAt(i);
                    yield return new WaitForSeconds(_squareAnimOffset);
                }
            } while (_squares.Count > 0);
            _squaresSound.PlayDestroyLoop(false);
            _isExecuting = false;
        }
    }
}
