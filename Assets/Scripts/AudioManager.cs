using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFX;
    [SerializeField] private AudioClip shopOpen;

    [SerializeField] private AudioSource music;

    public void ShopOpen() { soundFX.PlayOneShot(shopOpen); }


    public void PlayOnce(AudioClip audioClip)
    {
        soundFX.PlayOneShot(audioClip);
    }

    public void StartMusic()
    {
        music.Play();
    }
}
