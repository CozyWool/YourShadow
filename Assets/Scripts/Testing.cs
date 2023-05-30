using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Linq;

public class Testing : MonoBehaviour
{
    int a = 137,b = 0;
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 301; i++)
        {

            int sum = 0;
            while (a != 0)
            {
                sum += a % 10;
                a /= 10;
            }
            a = sum + 20;
            b += a;
            if (i == 300 * 380)
            {
                Debug.Log(b);
            }
            
        }
       
    }
}
