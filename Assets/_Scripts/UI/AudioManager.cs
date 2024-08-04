using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private float volumePercent = 1.0f;

    public static AudioManager instance; //Making sure there is only 1 instance

    AudioSource[] allAudioManagerSounds;

    void Awake ()
    {
        //allAudioManagerSounds = GetComponents<AudioSource>();
        if (instance == null)
            instance = this;
        else
            {
                Destroy(gameObject);
                return;
            }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Theme");
    }
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + "is typed wrong. Check again spelling");
                return;
            }
        s.source.Play();
    }

    public void SetVolume(float _volumePercent)
    {
        volumePercent = _volumePercent;
        UpdateVolumes();
    }
    public float GetVolume()
    {
        return volumePercent;
    }

    private void UpdateVolumes()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = volumePercent;
        }
    }
}
// FindObjectOfType<AudioManager>().Play("_______");