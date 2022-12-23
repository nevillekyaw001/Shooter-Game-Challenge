using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] GameObject[] enemyInScene;
    [SerializeField] GameObject hideWaveText;
    [SerializeField] GameObject gameFinishedPanel;

    [SerializeField] Transform spawnPoints1;
    [SerializeField] Transform spawnPoints2;
    [SerializeField] Transform spawnPoints3;

    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text waveCounterText;

    int waveNumber = 0;
    float timeToCount = 30f;
    bool shouldCountTime = false;
    int randomRange;
    float playerHealth;

    [HideInInspector]public int sceneIndex;

    private void Awake()
    {
        instance = this;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == 2)
        {
            playerHealth = PlayerPrefs.GetFloat("savedHealth");
            PlayerHealth.instance.playerHealth = playerHealth;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SpawnEnemies), 4f);
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = PlayerHealth.instance.playerHealth;
        randomRange = Random.Range(-1, enemyPrefab.Length);
        waveText.SetText("Wave: " + waveNumber.ToString());
        waveCounterText.SetText("Next Wave in: " + timeToCount.ToString("#"));

        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene(0);
        }

        if (playerHealth <= 0)
        {
            GameOver();

        }

        if (waveNumber == 3 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            if(sceneIndex == 1)
            {
                SceneManager.LoadScene(sceneIndex + 1);
            }
            else
            {
                PlayerPrefs.SetFloat("savedHealth", 300);
                gameFinishedPanel.SetActive(true);
            }
        }

        if (shouldCountTime)
        {
            timeToCount -= Time.deltaTime;

            if(timeToCount <= 0)
            {
                timeToCount = 30;
                SpawnEnemies();
            }
        }

        if (waveNumber == 3)
        {
            hideWaveText.SetActive(false);
            shouldCountTime = false;
        }
            
    }

    void SpawnEnemies()
    {
        if (sceneIndex == 1 && waveNumber >= 0)
        {
            Instantiate(enemyPrefab[0], spawnPoints1.position, Quaternion.identity);
            Instantiate(enemyPrefab[0], spawnPoints2.position, Quaternion.identity);
            Instantiate(enemyPrefab[0], spawnPoints3.position, Quaternion.identity);
        }
        else if (waveNumber == 0 && sceneIndex == 2)
        {
            Instantiate(enemyPrefab[0], spawnPoints1.position, Quaternion.identity);
            Instantiate(enemyPrefab[0], spawnPoints2.position, Quaternion.identity);
            Instantiate(enemyPrefab[1], spawnPoints3.position, Quaternion.identity);
        }
        else if (waveNumber == 1 && sceneIndex == 2)
        {
            Instantiate(enemyPrefab[0], spawnPoints1.position, Quaternion.identity);
            Instantiate(enemyPrefab[1], spawnPoints2.position, Quaternion.identity);
            Instantiate(enemyPrefab[1], spawnPoints3.position, Quaternion.identity);
        }
        else if (waveNumber == 2 && sceneIndex == 2)
        {
            Instantiate(enemyPrefab[1], spawnPoints1.position, Quaternion.identity);
            Instantiate(enemyPrefab[1], spawnPoints2.position, Quaternion.identity);
            Instantiate(enemyPrefab[1], spawnPoints3.position, Quaternion.identity);
        }

        waveNumber++;

        if(waveNumber != 3)
        shouldCountTime = true;
    }

    void GameOver()
    {
        SceneManager.LoadScene(sceneIndex);
    }

   

}
