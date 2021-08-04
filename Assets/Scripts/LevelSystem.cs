using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    SoundManager soundManager;
    public List<Button> levels;  // levels list
    public Texture lockOpenTexture;

    private void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        if (!PlayerPrefs.HasKey("level")) 
            PlayerPrefs.SetInt("level", 1);
        lockOpen();
    }


    public void lockOpen()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("level"); i++)
        {
            levels[i].interactable = true; 
            levels[i].GetComponent<RawImage>().texture = lockOpenTexture; // button lock texture
            levels[i].GetComponent<Button>().onClick.AddListener(soundManager.AudioButton); // button click sound
        }
        Debug.Log(PlayerPrefs.GetInt("level") + "lockOpen level sayýsý");
    }

    public string levelID(int id)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(id);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    }

    public void levelOpen(int id)
    {
        SceneManager.LoadScene(levelID(id));
    }

    public void ClearPref() // clear all prefs
    {
        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("suankiSecilenLevel");
        PlayerPrefs.DeleteKey("levelNumber");
        PlayerPrefs.DeleteKey("sounds");
        PlayerPrefs.DeleteKey("musics");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }
}
