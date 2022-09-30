using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float _destroyTime = 1.5f;
    private void Start()
    {
        Destroy(gameObject, _destroyTime);
    }
}
