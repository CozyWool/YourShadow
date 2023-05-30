using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
                bullet.Die();
        }
    }
}
