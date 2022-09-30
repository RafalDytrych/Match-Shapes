using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AdressablesManager : MonoBehaviour
{
    public AssetReferenceAudioClip[] _audioClips;
    [SerializeField] private AudioManager _audioManager;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        Addressables.InitializeAsync().Completed += AdressablesManager_Completed;
    }


    private void AdressablesManager_Completed(AsyncOperationHandle<IResourceLocator> obj)
    {
        int random = UnityEngine.Random.Range(0, _audioClips.Length);
        _audioClips[random].LoadAssetAsync<AudioClip>().Completed += clip => _audioManager.UpdateList(clip.Result);
        StartCoroutine(GetMusic(random));
    }

    private IEnumerator GetMusic(int index)
    {
        //We limit audio loading to one clip at once
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < _audioClips.Length; i++)
        {
            if(i != index)
            {
                bool isAudioLoaded = false;
                _audioClips[i].LoadAssetAsync<AudioClip>().Completed += go =>
                {
                    _audioManager.UpdateList(go.Result);
                    isAudioLoaded = true;
                };
                while (!isAudioLoaded) //wait till previous audio end loading
                    yield return null;               
            }
        }
    }
}

[Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
{
    public AssetReferenceAudioClip(string guid): base (guid){}
}