using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera cms;
    public Transform room;
    public bool toroom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if (toroom)
        { 
            cms.Follow = room.transform;
            toroom = false;
        } */
            
    }
    
}
