using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartGame;
    public GameObject titleScreen;
    private float spawnRate = 1f;
    public bool isGameActive;
    private int score;
    void Start()
    {
       
    }

    void Update()
    {
        
    }
    IEnumerator SpawnTargets()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }

    }
    public void UpdateScore(int addScore)
    {
        
        score += addScore;
        scoreText.text = "Score: " + score;

    }
    public void GameOver()
    {
        restartGame.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int diffuculty)
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        spawnRate /= diffuculty;
        score = 0;

        StartCoroutine(SpawnTargets());
        UpdateScore(0);
    }
}
