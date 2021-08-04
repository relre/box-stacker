using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public GameObject musicObject;
    public GameObject soundObject;
    public Texture soundOn;
    public Texture soundOff;
    AudioSource[] sounds;


    void Start()
    {
        sounds = GetComponents<AudioSource>();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "level_" + (scene.buildIndex).ToString()) // check in menu or game
        {
            MuteMenuMusic();
        }
        else
        {
            UnMuteMenuMusic();
        }
    }

    void Update()
    {
        AudioController();
    }
    public void MusicButton()
    {
        if (musicObject.GetComponent<RawImage>().texture == soundOn)
        {
            musicObject.GetComponent<RawImage>().texture = soundOff;
            PlayerPrefs.SetString("musics", "offmusic");
        }
       else if (musicObject.GetComponent<RawImage>().texture == soundOff)
        {
            musicObject.GetComponent<RawImage>().texture = soundOn;
            PlayerPrefs.SetString("musics", "onmusic");
        }
    }
    public void SoundButton()
    {
        if (soundObject.GetComponent<RawImage>().texture == soundOn)
        {
            soundObject.GetComponent<RawImage>().texture = soundOff;
            PlayerPrefs.SetString("sounds", "offsound");
        }
        else if (soundObject.GetComponent<RawImage>().texture == soundOff)
        {
            soundObject.GetComponent<RawImage>().texture = soundOn;
            PlayerPrefs.SetString("sounds", "onsound");
        }
    }
    public void AudioController()
    {
        if (PlayerPrefs.GetString("musics") == "onmusic")
        {
            sounds[0].mute = false;
            musicObject.GetComponent<RawImage>().texture = soundOn;
        }
        if (PlayerPrefs.GetString("musics") == "offmusic")
        {
            sounds[0].mute = true;
            musicObject.GetComponent<RawImage>().texture = soundOff;
        }
        if (PlayerPrefs.GetString("sounds") == "onsound")
        {
            sounds[1].mute = false;
            soundObject.GetComponent<RawImage>().texture = soundOn;
        }
        if (PlayerPrefs.GetString("sounds") == "offsound")
        {
            sounds[1].mute = true;
            soundObject.GetComponent<RawImage>().texture = soundOff;
        }


    }
    public void AudioButton()
    {
        sounds[1].Play();
    }
    public void MuteMenuMusic()
    {
        sounds[0].mute = true;
    }
    public void UnMuteMenuMusic()
    {
        sounds[0].mute = false;
    }
}
