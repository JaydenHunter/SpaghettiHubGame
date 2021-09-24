using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        //audioSource = audioClip;
        //audioSource.Play(audioClip, volume);
        audioSource.Play();
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
