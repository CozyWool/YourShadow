using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Marker : MonoBehaviour
{

    public Transform MiniGame, Ellis;
    public CinemachineVirtualCamera cms;
    public bool inMiniGame = false;

    private void Update()
    {
        if (inMiniGame && Input.GetKeyDown(KeyCode.Escape))
        {
            cms.Follow = Ellis;
        }
    }

    private void OnTriggerStay2D(Collider2D reload)
    {

        if (reload.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            cms.Follow = MiniGame;
            inMiniGame = true;
        }

    }
}

