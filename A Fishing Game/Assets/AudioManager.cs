using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip cinematic;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource aus = GetComponent<AudioSource>();
        aus.clip = cinematic;
        aus.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
