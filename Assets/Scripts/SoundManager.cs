using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

	[SerializeField]List<AudioClip> musicList;
	[SerializeField]AudioSource source;
	[SerializeField]Slider slider;

	[SerializeField]GameObject muteUI;
	[SerializeField]GameObject unmuteUI;


	void Start () {
		if (PlayerPrefs.GetInt("mute", 0) == 1 && !_mute)
			mute();
		else if (PlayerPrefs.GetInt("mute", 0) == 0 && _mute)
			mute();
		source.clip = musicList[Random.Range(0, musicList.Count)];
		source.Play();
	}

	private void Update()
	{
		muteUI.SetActive(!_mute);			
		unmuteUI.SetActive(_mute);			
		if (!source.isPlaying){
			source.clip = musicList[Random.Range(0, musicList.Count)];
			source.Play();
		}
		if (!_mute)
			source.volume = slider.value;
	}

	bool _mute = false;
	public void mute(){
		_mute = !_mute;
		PlayerPrefs.SetInt("mute", _mute ? 1 : 0);
		PlayerPrefs.Save();
		source.volume = _mute ? 0f : 1f;
	}

}
