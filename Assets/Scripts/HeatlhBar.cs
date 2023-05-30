using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeatlhBar : MonoBehaviour
{
    public Image bar;
    public float fill;
    private EllisController2D ec;
    public bool isMike, isEliis = false;
    public Text HP;
    
    void Start()
    {
  
        ec = GetComponent<EllisController2D>();
        fill = 1f;
    }

    
    void Update()
    {
        fill = ec.health / 100f;
        bar.fillAmount = fill;
        HP.text = ec.health.ToString();
      
    }
}
 