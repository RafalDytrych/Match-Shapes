using UnityEngine;

namespace MatchLine
{
    public class BgSlot : MonoBehaviour
    {
        private BgSlotsManager _myManager;
        [SerializeField] private SpriteRenderer _sprite;
        public void Initialize(BgSlotsManager manager)
        {
            _sprite = GetComponent<SpriteRenderer>();
            _myManager = manager;
            if (_myManager == null)
                _myManager = GetComponentInParent<BgSlotsManager>();
        }

        public void MarkMe() => _sprite.color = _myManager.MarkColor;
        public void UnMarkMe() => _sprite.color = _myManager.NormalColor;
        public void IncorrectPosition() => _sprite.color = _myManager.IncorrectPositionColor;
    }
}
