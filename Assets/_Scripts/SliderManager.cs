using UnityEngine;
using UnityEngine.Audio;

public class SliderManager : MonoBehaviour
{
    private AudioManager audioManager;
    public float volume;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        volume = audioManager.GetVolume();
    }

    public void ChangeVolume(float _volume)
    {
        volume = _volume;
        audioManager.SetVolume(volume);
    }

}
