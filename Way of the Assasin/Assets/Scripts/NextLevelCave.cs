using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelCave : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && CharController.isGrounded == true){
            Invoke("LoadingNextScene", 0.15f);
        }
    }
    void LoadingNextScene()
    {
        SceneManager.LoadScene("Level1");
    }
}