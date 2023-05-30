using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
public class PlayerAttack : MonoBehaviour
{
    private void Update()
    {
    }
    /*
    public float timeBtwAttack, startTimeBtwAttack;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;
    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }


            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    } */
}
