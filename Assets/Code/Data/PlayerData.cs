using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    [field: SerializeField] public bool IsAudioOn { get; set; }
    [field: SerializeField] public float AudioVolume { get; set; }
    [field: SerializeField] public float SfxVolume { get; set; }
    [field: SerializeField] public float GameTime { get; set; }
    [field: SerializeField] public Score[] Records { get; set; }
    [field: SerializeField] public float SquaresSpeedOffset { get; set; }
    [field: SerializeField] public int LanguageIndex { get; set; }
}
