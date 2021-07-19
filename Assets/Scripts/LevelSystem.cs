using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public List<Button> levels;  //leveller listesi

    private void Start()
    {
        if (!PlayerPrefs.HasKey("level")) //default deðer atama level adlý bir key yoksa 1 atamasý yapýyor
            PlayerPrefs.SetInt("level", 1);
        //  if (!PlayerPrefs.HasKey("seviyeSayisi")) //default deðer atama level adlý bir key yoksa 1 atamasý yapýyor

        lockOpen(); //açýk olan levelleri açýyor
    }

    public void lockOpen()//açýlan bölümlerin týklanabilirliðini aktif hale getiriyor.
    {
        for (int i = 0; i < PlayerPrefs.GetInt("level"); i++)
        {
            levels[i].interactable = true;
        }
    }

    public string levelID(int id)//id den level'in ismini döndürüyor
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(id);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    }

    public void levelOpen(int id)//buton üzerinden gelen id ye göre level açýlýyor
    {
        //PlayerPrefs.SetString("suankiSecilenLevel", levelAdi(id));dawdaw
        SceneManager.LoadScene(levelID(id));
    }

    public void ClearPref() //Levelleri Sýfýrlama
    {
        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("suankiSecilenLevel");
        PlayerPrefs.DeleteKey("levelNumber");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  //aktif sahneyi yeniden yüklüyor
    }
}
