using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquaresSound : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _destroyClip;
    [SerializeField] private AudioClip _destroyLoopClip;
    [SerializeField] private AudioClip _putOnBoardSoundClip;
    [SerializeField] private AudioClip _refillShapes;
    private bool _isDestroyLoopPlaying;

    [ContextMenu("Play")]
    public void PlayDestroySound() => _audioManager.PlayOnceVFX(_destroyClip);
    public void PlayDestroyLoop(bool status) 
    {
        if (_isDestroyLoopPlaying && status)
            return;
        _isDestroyLoopPlaying = status;
        _audioManager.PlayVFXSound(_destroyLoopClip, status);
    }

    public void PlayPutOnShape() => _audioManager.PlayOnceVFX(_putOnBoardSoundClip);
    public void RefillNewShapes() => _audioManager.PlayOnceVFX(_refillShapes);
}
