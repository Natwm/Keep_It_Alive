using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource source;
    public static SoundManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance !=null)
            Debug.LogWarning("Multiple SoundManager");
        
        instance = this;
        source = GetComponent<AudioSource>();
    }
    
    public void Play(AudioClip clip){
        source.clip = clip;
        source.pitch = Random.Range(-1f,1f);
        source.Play();
    }
}
