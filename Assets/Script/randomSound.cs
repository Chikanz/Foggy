using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSound : MonoBehaviour {

    public Vector2 RandomRange;

    public AudioClip[] clips;

    public GameObject Lost;
	
	void Start ()
    {
        QueueSound();
    }

    void PlaySound()
    {
        //Get random child, play random clip
        if (!Lost.activeInHierarchy) //don't play sound when lost
        {
            transform.GetChild(Random.Range(0,transform.childCount)).
                GetComponent<AudioSource>().PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
       
        QueueSound();
    }

    void QueueSound()
    {
        Invoke("PlaySound", Random.Range((int)RandomRange.x, (int)RandomRange.y));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
