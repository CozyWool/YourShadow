using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using TMPro;

public class PlayerAim : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }
    public float attackRange = 10f;
    public bool isRevolver, isKnife, canShoot = true, relolverPickedUp = false, isSword, swordPickedUp = false;
    public Transform aimTransform,weaponTransform, meleeTransform, aimGunEndPointPosition,swordTransform;
    public EllisController2D ellis;
    public Animator aimAnim,meleeAnim,swordAnim;
   
    public int damage = 10;
    public float angle;
    public AudioSource shootAudioSource;
    public AudioClip shootSound;
    Vector3 mousePosition;
    Vector3 aimDir;
    public bool hitted;
    public CircleCollider2D meleeCollider, swordCollider;
    public float shootTimer = 1.5f;


    public TextMeshPro ammoText;
    public bool isReloading = false;
    public int maxAmmo = 6, currentAmmo, allAmmo = 12;
    
    public float reloadTime = 2f;

    public GameObject revolverUI, knifeUI,AmmoUI, swordUI;
    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }
    private void Start()
    {
        ChangeWeapon();
        currentAmmo = maxAmmo;
    }
    private void Update()
    {
        ShowAmmoAtUI();
        ChangeWeapon();
        mousePosition = UtilsClass.GetMouseWorldPosition();
        if (!isReloading)
        {
            HandleAiming();
        }
       
        HandleMelee();
        HandleShootTimer();
        if (currentAmmo <= 0 && allAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }
        HandleShooting();
        
    }
    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        aimAnim.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        int outOfAmmo = maxAmmo - currentAmmo;
        if (allAmmo >= outOfAmmo)
        {
            allAmmo -= outOfAmmo;
            currentAmmo = maxAmmo;
            Debug.Log("Reloaded!");
        }
        else
        {
            currentAmmo += allAmmo;
            allAmmo = 0;
            Debug.Log("Reloaded,but AllAmmo = " + allAmmo);
        }
       
       
    }
    private void ShowAmmoAtUI()
    {
        ammoText.text = currentAmmo + " / " + allAmmo;
    }
    private void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && relolverPickedUp)
        {
            isRevolver = true;
            isKnife = false;
            isSword = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isRevolver = false;
            isSword = false;
            isKnife = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && swordPickedUp)
        {
            isRevolver = false;
            isSword = true;
            isKnife = false;
        }
        if (isRevolver && relolverPickedUp)
        {
            ammoText.enabled = true;
            swordUI.SetActive(false);
            knifeUI.SetActive(false);
            revolverUI.SetActive(true);
            AmmoUI.SetActive(true);
            swordTransform.gameObject.SetActive(false);
            weaponTransform.gameObject.SetActive(true);
            meleeTransform.gameObject.SetActive(false);
        }
        if (isKnife)
        {
            ammoText.enabled = false;
            swordUI.SetActive(false);
            knifeUI.SetActive(true);
            revolverUI.SetActive(false);
            AmmoUI.SetActive(false);
            swordTransform.gameObject.SetActive(false);
            weaponTransform.gameObject.SetActive(false);
            meleeTransform.gameObject.SetActive(true);
        }
        if (isSword && swordPickedUp)
        {
            ammoText.enabled = false;
            swordUI.SetActive(true);
            knifeUI.SetActive(false);
            revolverUI.SetActive(false);
            AmmoUI.SetActive(false);
            swordTransform.gameObject.SetActive(true);
            weaponTransform.gameObject.SetActive(false);
            meleeTransform.gameObject.SetActive(false);
        }
    }

    private void HandleAiming()
    {
       

        aimDir = (mousePosition - transform.position).normalized;
       
        angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
            aimLocalScale.y = -1f;
        
        else
        {
       
            aimLocalScale.y = +1f;
        }
        aimTransform.localScale = aimLocalScale;

        aimTransform.eulerAngles = new Vector3(0, 0, angle);
       // Debug.Log(angle);
    }
    private void HandleShootTimer()
    {
       
        if (!canShoot)
        {
            shootTimer -= Time.deltaTime;

            if (shootTimer < 0)
            {
                canShoot = true;
                shootTimer = 1.5f;
            }
        }
    }
    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && allAmmo > 0)
        {
            StartCoroutine(Reload());
        }
        if (Input.GetMouseButtonDown(0) && isRevolver && canShoot && relolverPickedUp)
        {
         
            currentAmmo--;
            canShoot = false;
            shootAudioSource.PlayOneShot(shootSound);
            aimAnim.SetTrigger("Shoot");

            OnShoot?.Invoke(this, new OnShootEventArgs {

                gunEndPointPosition = aimGunEndPointPosition.position,
                shootPosition = mousePosition,
            });
                
        }
    }
    private void HandleMelee()
    {
        if (Input.GetMouseButtonDown(0) && isKnife)
        {
            meleeAnim.SetTrigger("Attack");
            StartCoroutine(Attack(false));
        }
        if (Input.GetMouseButtonDown(0) && isSword && swordPickedUp)
        {
            swordAnim.SetTrigger("Attack");
            StartCoroutine(Attack(false));
        }
        if (Input.GetKeyDown(KeyCode.Q) && ellis.canQ)
        {
            if (ellis.QStack < 5)
            {
                ellis.canQ = false;
                StartCoroutine(ellis.EllisQDo());
                StartCoroutine(Attack(true));
            }
        }
    }
    IEnumerator Attack(bool IsQ)
    {

        if (!hitted)
        {
            if (!IsQ)
            {
                if (isKnife)
                {
                    meleeCollider.enabled = true;
                }
                else if (isSword)
                {
                    swordCollider.enabled = true;
                }
               
                yield return new WaitForSeconds(1f);
                if (isKnife)
                {
                    meleeCollider.enabled = false;
                }
                else if(isSword)
                {
                    swordCollider.enabled = false;
                }
              
                hitted = true;
            }
            else if (IsQ)
            {
                hitted = false;


            }
            

        }
        else if(hitted)
        {
            hitted = false;
        }
     
    }
}
