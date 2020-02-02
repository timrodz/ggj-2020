using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioFile {
    public AudioClip Clip;
    public string id;
}

public class AudioManager : Singleton<AudioManager> {
    // (Optional) Prevent non-singleton constructor use.
    protected AudioManager () { }

    [SerializeField]
    private AudioSource FX;

    public List<AudioFile> audioFiles = new List<AudioFile> ();

    public void PlayFX (string id) {
        AudioFile af = audioFiles.Find ((file) => file.id == id);
        if (af.Clip != null) {
            FX.PlayOneShot (af.Clip);
			Debug.LogFormat("Playing sound: {0}", af.Clip.name);
		}
    }
}