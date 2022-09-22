using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject[] enemies;
    int enemiesLeft = 0;
    bool gameHasEnded = false;

    public void EndGame (bool hasWon)
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;

            if(hasWon)
            {
                Debug.Log("You win!");
                WinGame();
            } else
            {
                Debug.Log("Game Over");
                //End the game
                GameOver();
            }
        }
    }

    void GameOver ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void WinGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemies.Length;
    }

    private void Update()
    {

    }

    public void KillEnemy()
    {
        enemiesLeft--;

        if(enemiesLeft == 0)
        {
            EndGame(true);
        }
    }
}
