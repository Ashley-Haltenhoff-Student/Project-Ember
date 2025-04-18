using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFX;
    [SerializeField] private AudioClip shopOpen;

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource settingsMusic;

    [SerializeField] private GlobalEvents events;


    private void Start()
    {
        events.GameStart.AddListener(StartMusic);
        events.GameEnd.AddListener(StartSettingsMusic);

        StartSettingsMusic();
    }

    public void ShopOpen() { soundFX.PlayOneShot(shopOpen); }


    public void PlayOnce(AudioClip audioClip)
    {
        soundFX.PlayOneShot(audioClip);
    }

    public void StartMusic()
    {
        settingsMusic.Stop();
        music.Play();
    }

    public void StartSettingsMusic()
    {
        music.Stop();
        settingsMusic.Play();
    }
}
