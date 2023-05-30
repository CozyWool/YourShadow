using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath AiPath;
    public Animator anim;
    public EllisController2D ellisCntrl;
    private Enemy enemy;
    public string runBool, attackTrigger, attackTrigger1,damageTrigger;
    private void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponentInParent<Enemy>();
    }

  
    void Update()
    {
        if (enemy.health <= 0)
        {
            anim.SetTrigger("Die");
        }
        if (AiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            
        }
        else if (AiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            
        }
        if (AiPath.reachedDestination)
        {
            anim.SetBool(runBool,false);
        }
        else
        {
            anim.SetBool(runBool, true);
        }
        if (enemy.isAttacking)
        {
            int animindex = Random.Range(0, 1);

            if (animindex == 0)
                
                anim.SetTrigger(attackTrigger);
            if (animindex == 1)
                anim.SetTrigger(attackTrigger1);
        }
        
    }
}
