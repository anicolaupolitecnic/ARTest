using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	// Audio players components.
	private AudioSource fxSource;
    private AudioSource musicSource;

	public AudioClip menuMusic;
	public AudioClip modeAdvMusic;
    public AudioClip modeCirMusic;
    public AudioClip modeXperMusic;

    public AudioClip fx_Click;
    public AudioClip fx_Options;

    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
	public float HighPitchRange = 1.05f;

	// Singleton instance.
	public static SoundManager Instance = null;

	//Initialize the singleton instance.
	private void Awake()
	{
		//GameObject[] objs = GameObject.FindGameObjectsWithTag("SoundManager");
		//if (objs.Length > 1)
		//{
		//	Destroy(this.gameObject);
		//}
		////Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		//DontDestroyOnLoad(this.gameObject);

  //      musicSource = this.transform.GetChild(0).GetComponent<AudioSource>();
  //      fxSource = this.transform.GetChild(1).GetComponent<AudioSource>();
	}

	// Play a single clip through the sound effects source.
	public void PlayFX(AudioClip clip) {
		//if (game.GetComponent<Game>().isFXEnabled) {
			fxSource.clip = clip;
			fxSource.Play();
		//}
	}

	// Play a single clip through the music source.
	public void PlayMusic(AudioClip clip) {
		//if (game.GetComponent<Game>().isMusicEnabled) {
			musicSource.enabled = true;
			musicSource.loop = true;
			musicSource.clip = clip;
			musicSource.Play();
		//}
	}
	
	public void PlayFX(int i) {
		//if (game.GetComponent<Game>().isFXEnabled) {
			fxSource.enabled = true;
			if (i == 0) {

			} else if (i == 1) {

			} else if (i == 2) {

			} else if (i == 3) {
				
			}
			fxSource.Play();
		//}
	}

	// Play a single clip through the music source.
	public void PlayMusic(int i) {
		//if (game.GetComponent<Game>().isMusicEnabled) {
			musicSource.enabled = true;
			musicSource.loop = true;
			if (i == 0) { 
				musicSource.clip = menuMusic;
			} else if (i == 1) {
				//musicSource.clip = gameMusic;
			}
			musicSource.Play();
		//}
	}

	public void playMenuMusic() {
		//if (game.GetComponent<Game>().isMusicEnabled)
			PlayMusic(menuMusic);
	}

	public void playGameMusic() {
		//if (game.GetComponent<Game>().isMusicEnabled)
			//PlayMusic(gameMusic);
	}

	public void StopMusic() {
		musicSource.Stop();
	}

	public void setMusicVolume(float f) {
		musicSource.volume = f;
	}

	public void setFXVolume(float f) {
		fxSource.volume = f;
	}
}