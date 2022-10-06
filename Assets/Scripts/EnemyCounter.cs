using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public TextMeshProUGUI enemyText;
    int enemiesLeft;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCounter(int enemies)
    {
        enemiesLeft = enemies;
        string text = "Enemies Left: " + enemiesLeft.ToString();
        enemyText.text = text;
    }
}
