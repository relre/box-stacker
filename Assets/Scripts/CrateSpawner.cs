using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrateSpawner : MonoBehaviour
{
    public GameObject cratePrefab;
    public GameObject ghostPrefab;
    public GameObject endGamePanel;
    public Button currentLevelButton;
    SceneChanger sceneChanger;
    public float ghostSpeed = 0.1f;
    public int yHeight = 2;
    int yHeightMax = 8;
    bool isGameActive = true;
   

    Vector3 cratePos;
    Vector3 ghostPos;



    void Start()
    {
        StartCoroutine(GhostMover());
        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
        currentLevelButton.GetComponentInChildren<TextMeshProUGUI>().text = ;
    }

    void Update()
    {
        ghostPos = ghostPrefab.transform.position - new Vector3(0, 1.1f, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGameActive)
        {
            Instantiate(cratePrefab, ghostPos, transform.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGameActive = !isGameActive;
            if (isGameActive)
            {
                StartCoroutine(GhostMover());
            }
        }
        else if (yHeight >= yHeightMax)
        {
            isGameActive = false;
            ghostPrefab.gameObject.SetActive(false);
            NextLevelController();
        }
    }

    WaitForSeconds moveTimer(int i)
    {
        cratePos = new Vector3(i, yHeight, 0);
        ghostPrefab.transform.position = cratePos;
        return new WaitForSeconds(ghostSpeed);
    }
    IEnumerator GhostMover()
    {
        while (isGameActive)
        {
            for (int i = -3; i < 3; i++)
            {
                yield return moveTimer(i);
            }

            for (int i = 3; i > -3; i--)
            {
                yield return moveTimer(i);
            }
        }
    }
    void NextLevelController()
    {
        string currentLevel = levelAdi(SceneManager.GetActiveScene().buildIndex);// (PlayerPrefs.GetString("suankiSecilenLevel");) Yenilendi çünkü level ekranýndan geçiþte kaydettiðimiz leveli alýyorduk fakat sonraki levele bu sahneden geçince kayýtlý level eskisi kalýyor o yüzden direk aktif sahne build indexinden adýný çaðýrýp iþlem yaptýrýyoruz.
        int currentLevelID = int.Parse(currentLevel.Split('_')[1]); //Level_id biçiminde olduðundan saðtaraf yani (id) 
        
        int nextLevel = PlayerPrefs.GetInt("level") + 1;

        if (currentLevelID == PlayerPrefs.GetInt("seviyeSayisi"))
        {
            Debug.Log("Oyun Sonu");

            endGamePanel.transform.GetChild(1).gameObject.SetActive(false); //sonraki level butonunu kapatýyoruz oyun sonuna gelindiði için
        }
        else
        {
            if (nextLevel - currentLevelID == 1)
                PlayerPrefs.SetInt("level", nextLevel);
            else
                Debug.Log("Önceden Açýlmýþ bir bölüme girdiniz.");
            GameObject nextLevelTag = GameObject.Find("NextLevelButton");
            endGamePanel.gameObject.SetActive(true);
            nextLevelTag.gameObject.SetActive(true); // sonraki level butonu aktif(eðer son bolüme kadar gidip tekrar onceki bolumlere girerse diye aktif hale getiriyoruz.)          
        }
        EndGamePanel(); //bolum sonu panel iþlemlerini baþlat
    }





    public string orjinal;
    




private void EndGamePanel()
{
    endGamePanel.SetActive(true);//Panel Aç
 
}

public string levelAdi(int id)//id den level'in ismini döndürüyor
{
    string scenePath = SceneUtility.GetScenePathByBuildIndex(id);
    string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
    return sceneName;
}


public void SonrakiLevel()//Sonraki Level Butonu
{
    sceneChanger.SceneChange(levelAdi(SceneManager.GetActiveScene().buildIndex + 1));
}

public void TekrarOyna()//Tekrar Oyna Butonu
{
    sceneChanger.SceneChange(levelAdi(SceneManager.GetActiveScene().buildIndex));
}
}
