using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = FindObjectOfType<AudioSource>().GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Muted") == 1)
        {
            _audioSource.Stop();
        }
    }

    public void MusicManager()
    {
        if (PlayerPrefs.GetInt("Muted", 0) ==1)
        {
            _audioSource.Play();
            PlayerPrefs.SetInt("Muted", 0);
            PlayerPrefs.Save();
        }
        else
        {
            _audioSource.Pause();
            PlayerPrefs.SetInt("Muted", 1);
            PlayerPrefs.Save();
        }
    }

    public void EndMusic()
    {
        _audioSource.Stop();
    }
}
