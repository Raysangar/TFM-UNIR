using UnityEngine;

namespace Core.Audio
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "Core/Audio/Audio Settings")]
    public class AudioSettings : ScriptableObject
    {
        public int MaxSimultaneusAudioEffects;
        public AudioSource AudioEffectSourcePrefab;
    }
}
