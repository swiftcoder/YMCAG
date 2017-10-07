using UnityEngine;
using System.Collections;

public class SoundEffects : MonoBehaviour {

    public AudioClip[] creatureSounds;
    public AudioClip godPleased, godAngered, winCondition, loseCondition;
    
    private AudioSource source;
    
	// Use this for initialization
	void Start () {
	   source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void startMusic() {
        source.Play();
    }
    
    public void playPleasedSound() {
        source.PlayOneShot(godPleased, 1.0f);    
    }

    public void playAngeredSound() {
        source.PlayOneShot(godAngered, 1.0f);    
    }
    
    public void playWinSound() {
        source.Stop();
        source.PlayOneShot(winCondition, 1.0f);
    }
    
    public void playLoseSound() {
        source.Stop();
        source.PlayOneShot(loseCondition, 1.0f);
    }
    
    public void playCreatureSound() {
        int which = Random.Range(0, creatureSounds.Length);
        source.PlayOneShot(creatureSounds[which], 0.5f);
    }
}
