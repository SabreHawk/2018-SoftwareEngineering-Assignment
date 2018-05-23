using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    public AudioSource effectAudio;
    public AudioClip [] audioList;
    private void Awake() {
        instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playeAudioJump() {
        float clipPitch = Random.Range(0.8f, 1.2f);
        effectAudio.clip = audioList[0];
        effectAudio.volume = 0.3f;
        effectAudio.pitch = clipPitch;
        effectAudio.Play();
    }
    public void playAudioThrough() {
        float clipPitch = Random.Range(0.8f, 1.2f);
        effectAudio.clip = audioList[1];
        effectAudio.pitch = clipPitch;
        effectAudio.volume = 1f;
        effectAudio.Play();
    }
}
