using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

public class AudioGhost : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    public AudioClip sound;

    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(sound);

        Destroy(gameObject, sound.length);

    }


}
