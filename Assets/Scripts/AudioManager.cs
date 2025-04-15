using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundFX;

    [SerializeField] private AudioClip shopOpen;

    public void ShopOpen() { soundFX.PlayOneShot(shopOpen); }
}
