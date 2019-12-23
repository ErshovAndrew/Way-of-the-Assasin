using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public int coinValue = 1;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ScoreManger.instance.ChangeScore(coinValue);
            Destroy(gameObject);
        }
    }
}
