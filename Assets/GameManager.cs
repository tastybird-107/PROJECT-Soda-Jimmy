using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public void EndGame ()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            //End the game
            GameOver();
        }
    }

    void GameOver ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}