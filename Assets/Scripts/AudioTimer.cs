using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioTimer : MonoBehaviour
{
    public bool recursive;
    public float minutes;
    bool OnPlaying;
    [Range(0f, 59.9999999999f)] public float seconds;
    void Start()
    {
        Invoke("PlaySound", (minutes*60)+seconds);
    }
    private void Update()
    {
        if (recursive && !GetComponent<AudioSource>().isPlaying && !OnPlaying) 
        {
            OnPlaying = true;
            Invoke("PlaySound", (minutes * 60) + seconds); 
        }
    }
    public void PlaySound()
    {
        OnPlaying = false;
        GetComponent<AudioSource>().Play();
        
    }
}
