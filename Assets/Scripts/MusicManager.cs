using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance { get; private set; }

    private const float DEFAULT_VAOLUME = 0.5f;

    private AudioSource audioSource;

    private float volume = 0.5f;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        volume = PlayerPrefs.GetFloat(Strings.pref_musicVolume, DEFAULT_VAOLUME);
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }

    public void ChangeVolume() {
        volume += 0.1f;
        if (volume > 1.0f) {
            volume = 0.0f;
        }
        audioSource.volume = volume;

        PlayerPrefs.SetFloat(Strings.pref_musicVolume, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}
