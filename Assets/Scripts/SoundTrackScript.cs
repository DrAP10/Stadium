using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrackScript : MonoBehaviour {

	public AudioClip[] gamesSoundTrack;
	int currentTrack;

	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		currentTrack = 0;
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = gamesSoundTrack [currentTrack];
		audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySoundTrack(int index)
	{
		audioSource.Stop();
		audioSource.clip = gamesSoundTrack[index];
		audioSource.Play();
	}
	public void Play()
	{
		if (audioSource.isPlaying)
			return;
		audioSource.clip = gamesSoundTrack[currentTrack];
		audioSource.Play();
	}

	public void Stop()
	{
		audioSource.Stop();
	}
	public void Pause()
	{
		audioSource.Pause();
	}
	public void UnPause()
	{
		audioSource.UnPause();
	}

	public void SetClip(int index)
	{
		currentTrack = index;
		if (index == 7)
			audioSource.loop = false;
		else
			audioSource.loop = true;
		Stop ();
	}
}
