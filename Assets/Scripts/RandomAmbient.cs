using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class RandomAmbient : MonoBehaviour
{
    public AudioClip[] listClips;
    public float minutes_min;
    [Range(0f, 59.9999999999f)] public float seconds_min;
    public float minutes_max;
    [Range(0f, 59.9999999999f)] public float seconds_max; 
    bool OnPlaying;
    void Start()
    {

        Invoke("PlaySound", (Random.Range(minutes_min,minutes_max) * 60) + Random.Range(seconds_min, seconds_max));
    }
    private void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying && !OnPlaying)
        {
            OnPlaying = true;
            Invoke("PlaySound", (Random.Range(minutes_min, minutes_max) * 60) + Random.Range(seconds_min, seconds_max));
        }
    }
    public void PlaySound()
    {
        OnPlaying = false;
        GetComponent<AudioSource>().clip = listClips[Random.Range(0, listClips.Length)];
        GetComponent<AudioSource>().Play(); 
      
    }
}
