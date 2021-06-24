using UnityEngine;
using Core.Utils;

namespace Core.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Instantiate(Resources.Load<AudioManager>("AudioManager"));
                    DontDestroyOnLoad(instance);
                }
                return instance;
            }
        }

        private static AudioManager instance;

        private const string MasterVolumeSettingsId = "MasterVolume";
        private const string MusicVolumeSettingsId = "MusicVolume";
        private const string EffectsVolumeSettingsId = "EffectsVolume";

        [SerializeField] AudioSettings settings;

        private AudioSource[] audioEffectSources;
        private AudioSource musicSource;

        public void AttachTo(Transform parent)
        {
            transform.SetParent(parent);
            transform.ResetLocalPosition();
        }

        public void PlayMusic(AudioClip music)
        {
            musicSource.Stop();
            musicSource.clip = music;
            musicSource.Play();
        }

        public void PlayAudioEffect(AudioClip[] clips, Vector3 position)
        {
            PlayAudioEffect(clips[Random.Range(0, clips.Length)], position);
        }

        public void PlayAudioEffect(AudioClip clip, Vector3 position)
        {
            var source = GetAvailableAudioSource();
            source.clip = clip;
            source.transform.position = position;
            source.Play();
        }

        private AudioSource GetAvailableAudioSource()
        {
            AudioSource oldestSource = null;
            foreach(var source in audioEffectSources)
            {
                if (source.isPlaying)
                {
                    if (oldestSource == null || oldestSource.time < source.time)
                        oldestSource = source;
                }
                else
                    return source;
            }
            oldestSource.Stop();
            return oldestSource;
        }

        private void Awake()
        {
            musicSource = GetComponent<AudioSource>();
            var sourcesParent = new GameObject("AudioSourcesParent");
            DontDestroyOnLoad(sourcesParent);
            audioEffectSources = new AudioSource[settings.MaxSimultaneusAudioEffects];
            for (int i = 0; i < settings.MaxSimultaneusAudioEffects; ++i)
                audioEffectSources[i] = Instantiate(settings.AudioEffectSourcePrefab, sourcesParent.transform);
        }
    }
}
