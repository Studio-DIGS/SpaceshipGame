using UnityEngine;
using UnityEngine.Audio;

public class SliderManager : MonoBehaviour
{
    private AudioManager audioManager;
    private Player player;
    public float volume;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        volume = audioManager.GetVolume();
        foreach(AudioSource _audio in player.allPlayerSounds)
        {
            _audio.volume = volume;
        }
    }

    public void ChangeVolume(float _volume)
    {
        volume = _volume;
        audioManager.SetVolume(volume);
        foreach(AudioSource _audio in player.allPlayerSounds)
        {
            _audio.volume = _volume;
        }
    }

}
