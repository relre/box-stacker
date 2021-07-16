using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public List<Button> leveller;  //leveller listesi

    private void Start()
    {
        if (!PlayerPrefs.HasKey("level")) //default deðer atama level adlý bir key yoksa 1 atamasý yapýyor
            PlayerPrefs.SetInt("level", 1);
        //  if (!PlayerPrefs.HasKey("seviyeSayisi")) //default deðer atama level adlý bir key yoksa 1 atamasý yapýyor

        kilitleriAc(); //açýk olan levelleri açýyor
    }

    public void kilitleriAc()//açýlan bölümlerin týklanabilirliðini aktif hale getiriyor.
    {
        for (int i = 0; i < PlayerPrefs.GetInt("level"); i++)
        {
            leveller[i].interactable = true;
        }
    }

    public string levelAdi(int id)//id den level'in ismini döndürüyor
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(id);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    }

    public void levelAc(int id)//buton üzerinden gelen id ye göre level açýlýyor
    {
        //PlayerPrefs.SetString("suankiSecilenLevel", levelAdi(id));dawdaw
        SceneManager.LoadScene(levelAdi(id));
    }

    public void ClearPref() //Levelleri Sýfýrlama
    {
        PlayerPrefs.DeleteKey("yildizlar");
        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("suankiSecilenLevel");
        PlayerPrefs.DeleteKey("seviyeSayisi");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  //aktif sahneyi yeniden yüklüyor
    }
}
