using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public float dmg = 1f;
    public float AttackTimer;
    public float AttackCd = 0.2f;
   public void OnTriggerEnter2D(Collider2D col)
    {
    if(col.isTrigger == false && col.CompareTag("Enemy") && AttackTimer <= 0f)
        {
            EnemyController.healthEnemy = EnemyController.healthEnemy - dmg;
            AttackTimer = AttackCd;
        }
    }
    private void Update()
    {
       if(AttackTimer > 0f)
        {
            AttackTimer -= Time.deltaTime;
        }
    }
}
