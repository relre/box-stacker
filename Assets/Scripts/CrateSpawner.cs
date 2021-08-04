using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrateSpawner : MonoBehaviour
{
    public GameObject homeUI;
    public GameObject inGameUI;
    public GameObject inGameObjects;
    public GameObject levelListUI;
    public GameObject settingsUI;

    public TextMeshProUGUI levelMenuText;
    public GameObject cratePrefab;
    public GameObject ghostPrefab;
    public GameObject endLevelPanel;
    public GameObject failedLevelPanel;
    public GameObject nextLevelPanel;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI spawnableCrateText;
    SceneChanger sceneChanger;
    public float ghostSpeed;
    public int yHeight = 2;
    public int spawnableCrate;
    int yHeightMax = 8;
    bool isGameActive = false;
    public float timer = 0f;


    Vector3 cratePos;
    Vector3 ghostPos;



    void Start()
    {
        isGameActive = true;
        StartCoroutine(GhostMover());
        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();

        LevelDiffuculty();
        LevelNumberUI();
    }

    void Update()
    {
        timer += Time.deltaTime;
        ghostPos = ghostPrefab.transform.position - new Vector3(0, 1.1f, 0);

        if (PlayerPrefs.GetString("sounds") == "offsound")
        {
            cratePrefab.GetComponent<AudioSource>().mute = true;
        }
        if (PlayerPrefs.GetString("sounds") == "onsound")
        {
            cratePrefab.GetComponent<AudioSource>().mute = false;
        }
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

        if (currentLevelIDx >= 0 && currentLevelIDx <= 5)
        {
            ghostSpeed = 0.15f;
            spawnableCrate = 34;
            spawnableCrate -= currentLevelIDx * 4;
        }
        else if (currentLevelIDx >= 5 && currentLevelIDx <= 10)
        {
            ghostSpeed = 0.15f;
            spawnableCrate = 22;
            spawnableCrate -= currentLevelIDx * 2;
        }
        else if (currentLevelIDx >= 10 && currentLevelIDx <= 15)
        {
            ghostSpeed = 0.13f;
            spawnableCrate = 22;
            spawnableCrate -= currentLevelIDx * 2;
        }
        else if (currentLevelIDx >= 15 && currentLevelIDx <= 20)
        {
            ghostSpeed = 0.10f;
            spawnableCrate = 22;
            spawnableCrate -= currentLevelIDx * 2;
        }
        else if (currentLevelIDx >= 20 && currentLevelIDx <= 25)
        {
            ghostSpeed = 0.10f;
            spawnableCrate = 20;
            spawnableCrate -= currentLevelIDx * 3;
        }
        spawnableCrateText.text = spawnableCrate.ToString();

    }
    void PlayController()
    {

        if (Input.GetButtonDown("Fire1") && isGameActive && spawnableCrate > 0 && timer > 1f)
        {
            Instantiate(cratePrefab, ghostPos, transform.rotation);
            spawnableCrate--;
            spawnableCrateText.text = spawnableCrate.ToString();


        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGameActive = !isGameActive;
            if (isGameActive)
            {
                StartCoroutine(GhostMover());
            }
        }
        else if (yHeight >= yHeightMax) // win this level
        {
            isGameActive = false;
            NextLevelController();
            
        }
        else if (spawnableCrate == 0 && !(yHeight >= yHeightMax) && isGameActive && inGameUI.activeInHierarchy == true) // lose this level
        {
            FailedLevelPanel();
        }
    }
    void NextLevelController()
    {
        string currentLevel = levelID(SceneManager.GetActiveScene().buildIndex);
        int currentLevelID = int.Parse(currentLevel.Split('_')[1]); 

        int nextLevel = PlayerPrefs.GetInt("level") + 1;

        if (currentLevelID == PlayerPrefs.GetInt("levelNumber"))
        {
            Debug.Log("Oyun Sonu");
            endLevelPanel.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            if (nextLevel - currentLevelID == 1)
            {
                PlayerPrefs.SetInt("level", nextLevel);
                Debug.Log(nextLevel - currentLevelID + " " + PlayerPrefs.GetInt("level")); 
            }
        }
        EndLevelPanel(); // end level panel open
    }

    void LevelNumberUI() // level number
    {
        string currentLevelx = levelID(SceneManager.GetActiveScene().buildIndex);
        int currentLevelIDx = int.Parse(currentLevelx.Split('_')[1]);
        currentLevelText.text = currentLevelIDx.ToString();
    }
    void FailedLevelPanel()
    {
        failedLevelPanel.SetActive(true);
        if (PlayerPrefs.GetString("sounds") == "offsound")
        {
            failedLevelPanel.GetComponent<AudioSource>().mute = true;
        }
        if (PlayerPrefs.GetString("sounds") == "onsound")
        {
            failedLevelPanel.GetComponent<AudioSource>().mute = false;
        }
    }
    private void EndLevelPanel()
    {
        endLevelPanel.SetActive(true);
        if (PlayerPrefs.GetString("sounds") == "offsound")
        {
            endLevelPanel.GetComponent<AudioSource>().mute = true;
        }
        if (PlayerPrefs.GetString("sounds") == "onsound")
        {
            endLevelPanel.GetComponent<AudioSource>().mute = false;
        }
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
    }

    public void PlayAgain() // play again button
    {
        sceneChanger.SceneChange(levelID(SceneManager.GetActiveScene().buildIndex));
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("login");
    }
    public void LevelButton()
    {
        homeUI.SetActive(false);
        levelListUI.SetActive(true);
        levelMenuText.text = "LEVELS";
    }
    public void PlayButton()
    {
        homeUI.SetActive(false);
        levelListUI.SetActive(true);
        levelMenuText.text = "SELECT LEVEL";
    }
    public void SettingButton()
    {
        homeUI.SetActive(false);
        settingsUI.SetActive(true);
        
    }  
    public void SettingButtonInGame()
    {
        isGameActive = false;
        settingsUI.SetActive(true);
    }
    public void SettingButtonInGamePlayerPref()
    {
        sceneChanger.SceneChange(levelID(SceneManager.GetActiveScene().buildIndex));
        settingsUI.SetActive(false);
    }

}
