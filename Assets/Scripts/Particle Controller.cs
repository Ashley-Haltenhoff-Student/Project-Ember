using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] GlobalEvents events;

    [SerializeField] ParticleSystem particleOne;
    [SerializeField] ParticleSystem particleTwo;

    private void Start()
    {
        events.GameEnd.AddListener(StartParticleSystems);
    }

    public void StartParticleSystems()
    {
        particleOne.Play();
        particleTwo.Play();
    }
}
