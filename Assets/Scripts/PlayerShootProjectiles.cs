using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootProjectiles : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;
    [SerializeField] private PlayerAim playerAim;
    private void Awake()
    {
        playerAim.OnShoot += PlayerShootProjecttiles_OnShoot;
    }

    private void PlayerShootProjecttiles_OnShoot(object sender, PlayerAim.OnShootEventArgs e)
    {
        // Shoot
        Transform bulletTransform = Instantiate(pfBullet, e.gunEndPointPosition, Quaternion.identity);
        Vector3 shootDir = (e.shootPosition - e.gunEndPointPosition).normalized;
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);
    }
}
