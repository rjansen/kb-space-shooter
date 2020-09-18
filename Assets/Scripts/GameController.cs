using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject[] hazards;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Vector3 spawnPosition = new Vector3(6f, 0f, 16f);
    public int hazardCount = 10;
    public float startWait = 1f;
    public float spawnWait = 0.5f;
    public float waveWait = 4f;

    int score = 0;
    bool gameOver = false;

    void Start () {
        restartText.enabled = false;
        gameOverText.enabled = false;
        UpdateScore();
        StartCoroutine(SpawnHazard());
    }

    void Update () {
        if (gameOver && Input.GetKeyDown(KeyCode.R)) {
            Restart();
        }
    }

    IEnumerator SpawnHazard() {
        yield return new WaitForSeconds(startWait);

        while (true) {
            for (int k = 0; k < hazardCount; k++) {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 position = new Vector3 (Random.Range(-spawnPosition.x, spawnPosition.x), spawnPosition.y, spawnPosition.z);
                Quaternion rotation = Quaternion.identity;
                Instantiate(hazard, position, rotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver) break;
        }
    }

    void UpdateScore() {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int score) {
        this.score += score;
        UpdateScore();
    }

    public void GameOver() {
        gameOver = true;
        gameOverText.enabled = true;
        restartText.enabled = true;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
