using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOTROTATE : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }
}
