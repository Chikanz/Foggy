using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSound : MonoBehaviour {

    public int SoundDelayMax;

    public AudioClip[] clips;

    public GameObject Lost;
	
	void Start ()
    {
        Invoke("PlaySound", Random.Range(5, SoundDelayMax));
    }

    void PlaySound()
    {
        if(!Lost.activeInHierarchy)
            GetComponent<AudioSource>().PlayOneShot(clips[Random.Range(0, clips.Length)]);
        Invoke("PlaySound", Random.Range(5, SoundDelayMax));
    }

}
