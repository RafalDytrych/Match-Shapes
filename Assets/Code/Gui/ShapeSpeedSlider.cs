using UnityEngine;
using UnityEngine.UI;

namespace MatchLine
{
    public class ShapeSpeedSlider : MonoBehaviour
    {
        [SerializeField] private ShapesManager _shapesManager;
        [SerializeField] private Slider _speedSlider;
        [SerializeField] private GameManager _gameManager;

        private void Awake()
        {
            _gameManager.OnPlayerDataInitialized += DataInitialized;
            _speedSlider.onValueChanged.AddListener((x) => 
            { 
                _shapesManager.SquaresSpeedOffset = x; 
            });
        }

        private void DataInitialized(PlayerData playerData)
        {
            _speedSlider.value = playerData.SquaresSpeedOffset;
        }
    }
}
