using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DoorTP : MonoBehaviour
{
    public CameraController cc;
    public Transform pointforplr,plrpositon,pointforcam;
    public GameObject roomHolder;
    public int orthosize;
    public bool isOpen = false;
    Room room;
    public Animator doorAnimL,doorAnimR;

    public bool needMorgatLight = false;
    public Light2D playerLight,morgatLight;
    bool isMorgat;
    public AudioSource scarySound;

    private void Start()
    {
        room = roomHolder.GetComponent<Room>();
    }
    private void Update()
    {
        isOpen = room.IsClear;
        if (isOpen)
        {
            doorAnimL.SetTrigger("Open");
            doorAnimR.SetTrigger("Open");
        }
        else
        {
            doorAnimL.SetTrigger("Close");
            doorAnimR.SetTrigger("Close");
        }
        if (isMorgat)
        {
            
            StartCoroutine(morgat());
        }

    }
    IEnumerator morgat()
    {
        morgatLight.enabled = true;
        yield return new WaitForSeconds(.25f);
        morgatLight.enabled = false;
        
    }
    private void OnCollisionEnter2D(Collision2D reload)
    {
        if (reload.gameObject.tag == "Player")
        {

            if (isOpen)
            {
                cc.cms.m_Lens.OrthographicSize = orthosize;
                cc.cms.Follow = pointforcam;
                plrpositon.position = pointforplr.position;
                if (needMorgatLight)
                {

                    isMorgat = true;
                    if (isMorgat)
                    {
                        scarySound.Play();
                    }
                    playerLight.enabled = false;
                    morgatLight.enabled = true;

                }
                else
                {
                    playerLight.enabled = true;
                }


            }
        }
    }
}
 
