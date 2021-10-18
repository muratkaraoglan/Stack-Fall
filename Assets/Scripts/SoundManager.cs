using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    private AudioSource audioSource;
    public static SoundManager Instance { get { return instance; } }
    public GameObject soundOffImage;
    private void Awake()
    {
        if ( instance != null && instance != this )
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
        bool m = isMute();
        audioSource.mute = m;
        soundOffImage.SetActive(m);
    }

    public void PlaySound(AudioClip audio, float volume)
    {
        audioSource.PlayOneShot(audio, volume);
    }

    public void Mute()
    {
        int mute = PlayerPrefs.GetInt("Mute");
        bool m = isMute();
        audioSource.mute = !m;
        soundOffImage.SetActive(!m);
        PlayerPrefs.SetInt("Mute", !m ? 1 : 0);
    }

    bool isMute()
    {
        int mute = PlayerPrefs.GetInt("Mute", 0);
        return mute == 1 ? true : false;
    }

}
