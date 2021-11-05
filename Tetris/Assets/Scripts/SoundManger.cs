using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManger : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject ActiveMusic;
    public GameObject DesactiveMusic;

    private void Awake()
    {
        this.ListenToTheme();
    }

    public void ListenToTheme()
    {
        audioSource.enabled = true;
        DesactiveMusic.SetActive(true);
        ActiveMusic.SetActive(false);
    }
    public void MuteMusic()
    {
        audioSource.enabled = false;
        DesactiveMusic.SetActive(false);
        ActiveMusic.SetActive(true);
    }
}
