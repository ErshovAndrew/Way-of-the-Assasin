using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 7f;
    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            
            Destroy(gameObject);
            EnemyController.healthEnemy = EnemyController.healthEnemy - 1f;
        }
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

}
