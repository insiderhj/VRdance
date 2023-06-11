using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFrame : MonoBehaviour
{
	AudioSource myAudio;
	bool musicStart = false;

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
    	if (!musicStart) {
    		myAudio.Play();
    		musicStart = true;
    	}
    }
}
