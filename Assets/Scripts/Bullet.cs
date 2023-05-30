using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    private Animator bulletanim;
    public float moveSpeed = 200f;
    bool isDie = false;

    private void Start()
    {
        bulletanim = GetComponentInChildren<Animator>();
    }

    public void Setup(Vector3 shootDir)
    {
        
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
      
        Destroy(gameObject, 5f);
        
    }
    private void Update()
    {
        if (!isDie)
            transform.position += shootDir * moveSpeed * Time.deltaTime;
       
    }
    private void OnTriggerEnter2D(Collider2D reload)
    {
        if (reload.gameObject.CompareTag("Wall") || reload.gameObject.CompareTag("Door"))
        {
            Die();
        }
        if (reload.gameObject.CompareTag("Enemy"))
        {
            int damage = 20;
            bool isCrit = Random.Range(0, 100) < 30;
            if (isCrit)
            {
                damage *= 2;
            }

            Enemy enemy = reload.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Die();
                DamagePopup.Create(enemy.transform.position, damage, isCrit);
                enemy.TakeDamage(damage);
            }
        }
    }
    public void Die()
    {
        isDie = true;
        moveSpeed = 0f;
        bulletanim.SetTrigger("Die");
        Destroy(gameObject);
    }
}
