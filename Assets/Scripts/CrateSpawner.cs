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
    public GameObject endLevelPanel;
    public GameObject failedLevelPanel;
    public GameObject nextLevelPanel;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI spawnableCrateText;
    SceneChanger sceneChanger;
    public float ghostSpeed = 0.1f;
    public int yHeight = 2;
    public int spawnableCrate;
    int yHeightMax = 8;
    bool isGameActive = true;


    Vector3 cratePos;
    Vector3 ghostPos;



    void Start()
    {
        StartCoroutine(GhostMover());
        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();

        LevelDiffuculty();
        LevelNumberUI();
    }

    void Update()
    {
        ghostPos = ghostPrefab.transform.position - new Vector3(0, 1.1f, 0);

        PlayController();
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
    void LevelDiffuculty()
    {

        string currentLevelx = levelID(SceneManager.GetActiveScene().buildIndex);
        int currentLevelIDx = int.Parse(currentLevelx.Split('_')[1]);

        spawnableCrate = 30;
        spawnableCrate -= currentLevelIDx * 2;
        spawnableCrateText.text = spawnableCrate.ToString();

    }
    void PlayController()
    {
        if (Input.GetButtonDown("Fire1") && isGameActive && spawnableCrate > 0)
        {
            Instantiate(cratePrefab, ghostPos, transform.rotation);
            spawnableCrate--;
            spawnableCrateText.text = spawnableCrate.ToString();
            Debug.Log(spawnableCrate);
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
        else if (spawnableCrate == 0)
        {
            FailedLevelPanel();
        }
    }
    void NextLevelController()
    {
        string currentLevel = levelID(SceneManager.GetActiveScene().buildIndex);// (PlayerPrefs.GetString("suankiSecilenLevel");) Yenilendi çünkü level ekranýndan geçiþte kaydettiðimiz leveli alýyorduk fakat sonraki levele bu sahneden geçince kayýtlý level eskisi kalýyor o yüzden direk aktif sahne build indexinden adýný çaðýrýp iþlem yaptýrýyoruz.
        int currentLevelID = int.Parse(currentLevel.Split('_')[1]); //Level_id biçiminde olduðundan saðtaraf yani (id) 

        int nextLevel = PlayerPrefs.GetInt("level") + 1;

        if (currentLevelID == PlayerPrefs.GetInt("levelNumber"))
        {
            Debug.Log("Oyun Sonu");

            endLevelPanel.transform.GetChild(1).gameObject.SetActive(false); //sonraki level butonunu kapatýyoruz oyun sonuna gelindiði için
        }
        else
        {
            if (nextLevel - currentLevelID == 1)
                PlayerPrefs.SetInt("level", nextLevel);
            else
                Debug.Log("Önceden Açýlmýþ bir bölüme girdiniz.");

            // endLevelPanel.gameObject.SetActive(true);
            // nextLevelPanel.gameObject.SetActive(true); // sonraki level butonu aktif(eðer son bolüme kadar gidip tekrar onceki bolumlere girerse diye aktif hale getiriyoruz.)          
        }
        EndLevelPanel(); // end level panel open
    }





    public string orjinal;

    void LevelNumberUI()
    {
        string currentLevelx = levelID(SceneManager.GetActiveScene().buildIndex);
        int currentLevelIDx = int.Parse(currentLevelx.Split('_')[1]);
        currentLevelText.text = currentLevelIDx.ToString();
    }
    void FailedLevelPanel()
    {
        failedLevelPanel.SetActive(true);
    }
    private void EndLevelPanel()
    {
        endLevelPanel.SetActive(true);//Panel Aç

    }
    public void EndLevelPanelCloser() 
    {
        endLevelPanel.SetActive(false);
    }

    string levelID(int id) // id to level id
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(id);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    }


    public void NextLevel() // next level button
    {
        sceneChanger.SceneChange(levelID(SceneManager.GetActiveScene().buildIndex + 1));
        EndLevelPanelCloser();
    }

    public void PlayAgain() // play again button
    {
        sceneChanger.SceneChange(levelID(SceneManager.GetActiveScene().buildIndex));
        EndLevelPanelCloser();
    }
}
