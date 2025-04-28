using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFX;
    [SerializeField] private AudioClip shopOpen;
    [SerializeField] private AudioClip pause;
    [SerializeField] private AudioClip unpause;
    [SerializeField] private AudioClip denied;
    [SerializeField] private AudioClip confirm;

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource settingsMusic;

    [SerializeField] private GlobalEvents events;


    private void Start()
    {
        events.ShopOpen.AddListener(OpenShop);
        events.GameEnd.AddListener(StartSettingsMusic);

        StartSettingsMusic();
    }



    public void PlayOnce(AudioClip audioClip)
    {
        soundFX.PlayOneShot(audioClip);
    }

    public void OpenShop()
    {
        settingsMusic.Stop();
        music.Play();

        soundFX.PlayOneShot(shopOpen);
    }

    public void StartSettingsMusic()
    {
        music.Stop();
        settingsMusic.Play();
    }

    public void Pause()
    {
        soundFX.PlayOneShot(pause);
    }

    public void UnPause()
    {
        soundFX.PlayOneShot(unpause);
    }

    public void Denied()
    {
        soundFX.PlayOneShot(denied);
    }

    public void Confirm()
    {
        soundFX.PlayOneShot(confirm);
    }
}
