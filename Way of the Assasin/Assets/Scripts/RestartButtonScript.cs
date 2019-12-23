using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void restartScene()
    {
        SceneManager.LoadScene("LevelZero");
        EnemyController.healthEnemy = 3f;
        EnemyController.isDead = false;
        CharController.health = 3;
        CharController.dead = false;
        ButtonsScript.PauseActive = false;
    }
}
