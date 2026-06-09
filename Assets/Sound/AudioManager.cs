using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public static AudioSource musicSource, sfxSource, voiceAudioSource, cardAudioSource;
    //private static AudioSource voiceAudioSource;

    private void Awake()
    {
       if (Instance == null)
        {
            Instance = this;
            AudioSource[] audioSources = GetComponents<AudioSource>();

            voiceAudioSource = audioSources[0];
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip[0];
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip[UnityEngine.Random.Range(0, s.clip.Length)]);
        }
    }
    public static void PlayCardSFX(AudioClip audioClip)
    {
        voiceAudioSource.PlayOneShot(audioClip);
    }
    public static void PlayVoice(AudioClip audioClip, float pitch = 1f)
    {
        voiceAudioSource.pitch = pitch;
        voiceAudioSource.PlayOneShot(audioClip);

        //voiceAudioSource.pitch = pitch;
        //voiceAudioSource.PlayOneShot(audioClip);
    }

    public void PlayerSFX(AudioClip audioClip)
    {
        voiceAudioSource.PlayOneShot(audioClip);
    }
    
    public void BossSFX(AudioClip audioClip)
    {
        voiceAudioSource.PlayOneShot(audioClip);
    }
    
    public void StackingSFX(AudioClip audioClip)
    {
        voiceAudioSource.PlayOneShot(audioClip);
    }

    public void DDRHitSFX(AudioClip audioClip)
    {
        voiceAudioSource.PlayOneShot(audioClip);
    }
    
    public void DDRMissSFX(AudioClip audioClip)
    {
        voiceAudioSource.PlayOneShot(audioClip);
    }

}
