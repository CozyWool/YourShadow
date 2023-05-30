using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{

    private static List<Enemy> enemyList;

    public static Enemy GetClosestEnemy(Vector3 position, float range)
    {
        if (enemyList == null)
            return null;

        Enemy closestEnemy = null;


        for (int i = 0; i < enemyList.Count; i++)
        {
            Enemy testEnemy = enemyList[i];
            if (Vector3.Distance(position,testEnemy.transform.position) > range)
            {
                continue; 
            }
            if (closestEnemy == null)
            {
                closestEnemy = testEnemy;
            }
            else
            {
                if (Vector3.Distance(position, testEnemy.transform.position) < Vector3.Distance(position,closestEnemy.transform.position))
                {
                    closestEnemy = testEnemy;
                }
            }
        }
        return closestEnemy;
    }
    public Room roomWhereIsEnemy;
    EnemyGFX gfx;
    private AIPath aIPath;
    public EllisController2D ellis;
    public int health = 100;
    public bool isAttacking, playernotnull,isMiniBoss;
    public PlayerAim playerAim;
    int EllisQ;
    float timer = 2f;

    public GameObject dieSprite, spriteKirisi,swordSprite;



    private void Start()
    {
        swordSprite.SetActive(false);
        dieSprite.SetActive(false);
        ellis = GameObject.FindGameObjectWithTag("Player").GetComponent<EllisController2D>();
        gfx = GetComponentInChildren<EnemyGFX>();
        if (enemyList == null)
            enemyList = new List<Enemy>();
        enemyList.Add(this);
        aIPath = GetComponent<AIPath>();
    }
    public void TakeDamage(int damage)
    { 
       // gfx.anim.SetTrigger("Die");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    
    }
    
    private void OnTriggerEnter2D(Collider2D reload)
    {
        if (reload.gameObject.CompareTag("Weapon"))
        {
           
            if (!playerAim.hitted && playerAim.isKnife)
            {
                int damage = 10;
                bool isCrit = UnityEngine.Random.Range(0, 100) < 30;
                if (isCrit)
                {
                    damage *= 2;
                }
              //  Debug.Log(reload.gameObject);
                TakeDamage(damage);
                DamagePopup.Create(transform.position, damage, isCrit);
            }
            else if (!playerAim.hitted && playerAim.isSword)
            {
                int damage = 15;
                bool isCrit = UnityEngine.Random.Range(0, 100) < 30;
                if (isCrit)
                {
                    damage *= 2;
                }
              //  Debug.Log(reload.gameObject);
                TakeDamage(damage);
                DamagePopup.Create(transform.position, damage, isCrit);
            }
            
        }
   
    }
    private void Update()
    {
       // Debug.Log(EllisQ);
        if (EllisQ > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            timer = 2f;
            EllisQ = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D reload)
    {


        if (reload.gameObject.CompareTag("EllisQ"))
        {
           // Debug.Log(reload.gameObject);

            int damage = 5;




            if (!playerAim.hitted && EllisQ == 0 && ellis.QStack < 5)
            {
                EllisQ = 1;
                if (EllisQ == 1)
                {
                    if (ellis.QStack < 5)
                    {
                       
                        ellis.QStack++;
                        EllisQ = 2;
                        TakeDamage(damage);
                        DamagePopup.Create(transform.position, damage, false);
                    }


                }

            }
           

        }

        if (reload.gameObject.tag == "Player" && !isAttacking)
        {
            playernotnull = true;
            StartCoroutine(Attack());
        }
    }
    private void OnTriggerExit2D(Collider2D reload)
    {
        if (reload.gameObject.tag == "Player")
        {
            playernotnull = false;
        }
    }
    void Die()
    {
        roomWhereIsEnemy.enemies.Remove(this);
        if (isMiniBoss)
        {
            playerAim.swordPickedUp = true;
            swordSprite.SetActive(true);
            Destroy(swordSprite,5f);
        }
        dieSprite.transform.position = transform.position;
        aIPath.canMove = false;
        dieSprite.SetActive(true);
        spriteKirisi.SetActive(false);
        enemyList.Remove(this);
        Destroy(gameObject);
        
       
    }
    IEnumerator Attack()
    {
        isAttacking = true;
        // aIPath.canMove = false;
        yield return new WaitForSeconds(1f);
        // aIPath.canMove = true;
        if (playernotnull)
        {
            DamagePopup.Create(ellis.transform.position,10,false);
            ellis.health -= 10;
        }
        if (playernotnull && isMiniBoss)
        {
            DamagePopup.Create(ellis.transform.position, 20, false);
            ellis.health -= 20;
        }
        yield return new WaitForSeconds(1f);

        isAttacking = false;
    }
}