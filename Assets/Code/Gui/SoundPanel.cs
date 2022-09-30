using System.Collections;
using UnityEngine;

public class SoundPanel : Panel
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private float _audioDelayTime;

    [ContextMenu("Turn ONN")]
    public override void TurnOn()
    {
        base.TurnOn();
        StartCoroutine(PlayAudio());
    }

    private IEnumerator PlayAudio()
    {
        yield return new WaitForSeconds(_audioDelayTime);
        _audioManager.PlayOnceSFX(_clip);
    }
}
