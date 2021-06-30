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

        public float MasterVolume => masterVolume;
        public float MusicVolume => musicVolume;
        public float EffectsVolume => effectsVolume;

        [SerializeField] AudioSettings settings;

        private AudioSource[] audioEffectSources;
        private AudioSource musicSource;
        private float masterVolume;
        private float musicVolume;
        private float effectsVolume;

        public void SetMasterVolume(float volume)
        {
            masterVolume = volume;
            ApplyVolumeChanges(MasterVolumeSettingsId, volume);
        }

        public void SetMusicVolume(float volume)
        {
            musicVolume = volume;
            ApplyVolumeChanges(MusicVolumeSettingsId, volume);
        }

        public void SetEffectsVolume(float volume)
        {
            effectsVolume = volume;
            ApplyVolumeChanges(EffectsVolumeSettingsId, volume);
        }

        private void ApplyVolumeChanges(string id, float volume)
        {
            PlayerPrefs.SetFloat(id, volume);
            PlayerPrefs.Save();
            UpdateVolumes();
        }

        private void UpdateVolumes()
        {
            musicSource.volume = masterVolume * musicVolume;
            foreach (var effectSource in audioEffectSources)
                effectSource.volume = masterVolume * effectsVolume;
        }

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
            if (clips.Length > 0)
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

            masterVolume = PlayerPrefs.GetFloat(MasterVolumeSettingsId, 1);
            musicVolume = PlayerPrefs.GetFloat(MusicVolumeSettingsId, 1);
            effectsVolume = PlayerPrefs.GetFloat(EffectsVolumeSettingsId, 1);
        }
    }
}
