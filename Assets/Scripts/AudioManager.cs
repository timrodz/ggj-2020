﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioFile {
	public AudioClip Clip;
	public string id;
}

public class AudioManager : Singleton<AudioManager>
{
    // (Optional) Prevent non-singleton constructor use.
    protected AudioManager() { }

	[SerializeField]
    private AudioSource FX;

	public List<AudioFile> audioFiles = new List<AudioFile>();

	public void PlayFX(string id) {
		var af = audioFiles.Find((file) => file.id == id);
		FX.PlayOneShot(af.Clip);
	}
}
