using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSource : MonoBehaviour
{
    public AudioClip[] clips;
    void Start()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clips[Random.Range(0, clips.Length)];
        source.volume = 0.1f;
        source.Play();
    }

}
