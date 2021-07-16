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
        if (!PlayerPrefs.HasKey("level")) //default de�er atama level adl� bir key yoksa 1 atamas� yap�yor
            PlayerPrefs.SetInt("level", 1);
        //  if (!PlayerPrefs.HasKey("seviyeSayisi")) //default de�er atama level adl� bir key yoksa 1 atamas� yap�yor

        kilitleriAc(); //a��k olan levelleri a��yor
    }

    public void kilitleriAc()//a��lan b�l�mlerin t�klanabilirli�ini aktif hale getiriyor.
    {
        for (int i = 0; i < PlayerPrefs.GetInt("level"); i++)
        {
            leveller[i].interactable = true;
        }
    }

    public string levelAdi(int id)//id den level'in ismini d�nd�r�yor
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(id);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    }

    public void levelAc(int id)//buton �zerinden gelen id ye g�re level a��l�yor
    {
        //PlayerPrefs.SetString("suankiSecilenLevel", levelAdi(id));dawdaw
        SceneManager.LoadScene(levelAdi(id));
    }

    public void ClearPref() //Levelleri S�f�rlama
    {
        PlayerPrefs.DeleteKey("yildizlar");
        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("suankiSecilenLevel");
        PlayerPrefs.DeleteKey("seviyeSayisi");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  //aktif sahneyi yeniden y�kl�yor
    }
}
