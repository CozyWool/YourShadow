using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool IsClear = false;
   
    public List<Enemy> enemies;
    
    private void Update()
    {
        if (enemies.Count > 0)
        {
            IsClear = false;
        }
        else if (enemies.Count == 0)
        {
            IsClear = true;
        }
       

    }
    private void OnTriggerStay2D(Collider2D reload)
    {
        /*
        if (reload.gameObject.tag == "Enemy")
        {
            IsClear = false;     
        }
        else if(reload.gameObject.GetComponent<Enemy>() == null)
        {           
            IsClear = true;
        }
        */
    }
}