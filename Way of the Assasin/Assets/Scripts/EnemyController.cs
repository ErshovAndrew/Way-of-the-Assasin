using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int speed = 2;
    public float distance;
    private bool movingRight = true;
    public Transform GroundDetection;
    private Animator anim;
    public GameObject healthBar, healthBar1, healthBar2, healthBar3;
    public static float healthEnemy = 3f;
    public static bool isDead = false;
    public Collider2D Enemy;
    private void Start()
    {
        anim = GetComponent<Animator>();
        healthBar.SetActive(true);
        healthBar1.SetActive(false);
        healthBar2.SetActive(false);
        healthBar3.SetActive(false);
    }
    void Update()
    {
        if (isDead == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            RaycastHit2D groundInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, distance);
            if (groundInfo.collider == false)
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }
        switch (healthEnemy)
        {
            case 2:

                healthBar.SetActive(false);
                healthBar1.SetActive(true);
                healthBar2.SetActive(false);
                healthBar3.SetActive(false);
                break;
            case 1:
                healthBar.SetActive(false);
                healthBar1.SetActive(false);
                healthBar2.SetActive(true);
                healthBar3.SetActive(false);
                break;
            case 0:
                healthBar.SetActive(false);
                healthBar1.SetActive(false);
                healthBar2.SetActive(false);
                healthBar3.SetActive(false);
                anim.SetBool("isDead", true);
                isDead = true;
                Enemy.enabled = false;
                break;

        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && !isDead)
        {
            speed = 0;
            anim.SetBool("isAttack", true);
            CharController.health = CharController.health - 1;
            
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Player" && !isDead)
        {
            speed = 2;
            anim.SetBool("isAttack", false);

        }
    }
    void BackGoEnd()
    {
        healthBar.SetActive(false);
        healthBar1.SetActive(false);
        healthBar2.SetActive(false);
        healthBar3.SetActive(false);
    }
    void SetActivefalsehealthBar()
    {
        healthBar.SetActive(false);
    }
}
