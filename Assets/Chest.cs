using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator anim,revolverAnim;
    public PlayerAim playerAim;
    public GameObject revolverOnGround;
    private void Start()
    {
        revolverAnim = revolverOnGround.GetComponent<Animator>();
        anim = GetComponent<Animator>();
        playerAim = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAim>();
    }
    private void OnTriggerStay2D(Collider2D reload)
    {




        if (reload.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            revolverAnim.Play("RevolverChest");
            Destroy(revolverOnGround,5f);
            anim.SetTrigger("Open");
            playerAim.relolverPickedUp = true;
        }
    }
}
