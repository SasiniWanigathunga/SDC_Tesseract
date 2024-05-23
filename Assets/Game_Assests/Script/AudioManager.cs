using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour

{
    [Header("----------Audio Sources----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource bulletSource;
    [SerializeField] AudioSource lawnMoverSource;
    [SerializeField] AudioSource plantSource;
    [SerializeField] AudioSource zombieDieSource;


    [Header("----------Audio Clips-------------")]
    public AudioClip background;
    public AudioClip bullet;
    public AudioClip lawnMover;
    public AudioClip plant;
    public AudioClip zombieDie;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlayBullet(AudioClip clip)
    {
        bulletSource.clip = clip;
        bulletSource.Play();
    }

    public void PlayLawnMover(AudioClip clip)
    {
        lawnMoverSource.clip = clip;
        lawnMoverSource.Play();
    }

    public void PlayPlant(AudioClip clip)
    {
        plantSource.clip = clip;
        plantSource.Play();
    }

    public void PlayZombieDie(AudioClip clip)
    {
        zombieDieSource.clip = clip;
        zombieDieSource.Play();
    }

}
