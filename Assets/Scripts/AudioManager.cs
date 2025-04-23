using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] AudioMixer _mixer;
    [SerializeField] AudioClip _music;
    AudioMixerGroup _musicGroup;
    AudioMixerGroup _sfxGroup;
    public enum SoundType
    {
        Music,
        SFX
    }

    const string MUSIC_GROUP_NAME = "Music";
    const string SFX_GROUP_NAME = "SFX";

    const string MASTER_VOLUME_NAME = "MasterVolume";
    const string MUSIC_VOLUME_NAME = "MusicVolume";
    const string SFX_VOLUME_NAME = "SFXVolume";


    void Awake() 
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
        Init();
    }
    void Init()
    {
        _musicGroup = _mixer.FindMatchingGroups(MUSIC_GROUP_NAME)[0];
        _sfxGroup = _mixer.FindMatchingGroups(SFX_GROUP_NAME)[0];
        PlayAudio(_music, SoundType.Music, 1.0f, true);
    }
    public void ChangeMasterVolume(float volume)
    {
        _mixer.SetFloat(MASTER_VOLUME_NAME, Mathf.Log10(volume) * 20);
    }
    public void PlayAudio(AudioClip audioClip, SoundType soundType, float volume, bool loop) 
    {
        GameObject audioSourceObject = new(audioClip.name+ " Source");
        AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.loop = loop;

        switch (soundType)
        {
            case SoundType.Music:
                audioSource.outputAudioMixerGroup = _musicGroup;
                break;
            case SoundType.SFX:
                audioSource.outputAudioMixerGroup = _sfxGroup;
                break;
            default:
                break;
        }

        audioSource.Play();

        if (!loop)
        {
            Destroy(audioSource.gameObject, audioClip.length);
        }

    }
}
