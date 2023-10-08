using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    public GameObject player;
    private int maxEnemies = 8;
    private float spawnRate = 2.0f;
    private float nextSpawn = 0;
    private float spawnRange = 100;
    private int numberOfEnemies;
    public TextMeshProUGUI scoreText;
    public GameObject healthBar;
    public GameObject pauseScreen;
    private int score;
    private bool paused = false;

    void Start()
    {
        player = GameObject.Find("Player");
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateScore(0);
        ManageEnemySpawn();
    }

    // Update is called once per frame
    void Update()
    {

        ManageEnemySpawn();

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }



    void ManageEnemySpawn()
     {
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (numberOfEnemies < maxEnemies && player != null && Time.time > nextSpawn)
        {
            Vector3 spawnDistance = ((Vector3.forward * -spawnRange) +
                                    (Vector3.up * (Random.Range(-spawnRange, spawnRange)) * 1.1f) +
                                    (Vector3.right * (Random.Range(-spawnRange, spawnRange)) * 1.1f));

            spawnDistance = player.transform.TransformPoint(spawnDistance);

            Instantiate(enemy, spawnDistance, transform.rotation);
            nextSpawn = spawnRate + Time.time;
         }
     }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {
        StartCoroutine(RestartGameHelper());
    }
    
    IEnumerator RestartGameHelper()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("StartScreen");
    }

    void PauseGame()
    {
        if (paused == false)
        {
            paused = true;
            Time.timeScale = 0;
            scoreText.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(false);
            pauseScreen.gameObject.SetActive(true);
        }
        else
        {
            paused = false;
            Time.timeScale = 1;
            scoreText.gameObject.SetActive(true);
            healthBar.gameObject.SetActive(true);
            pauseScreen.gameObject.SetActive(false);
        }
    }
}
