using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SelectHero : MonoBehaviour
{
    public GameObject Ellis;
    public GameObject Bit;
    public RectTransform charPosistion;
    public RectTransform charOutside;
    private int charInt = 1;
    bool isEllis = false;
  
    private void Awake()
    {
        isEllis = true;
    }
    public void Next()
    {
        switch (charInt)
        {
            case 1:
                isEllis = false;
                Ellis.transform.position = charOutside.transform.position;
                Ellis.transform.localScale = new Vector3(0.5f,0.5f,1f);
                Bit.transform.position = charPosistion.transform.position;
                Bit.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
                charInt++;
                break;
            case 2:
                isEllis = true;
                Ellis.transform.position = charPosistion.transform.position;
                Ellis.transform.localScale = new Vector3(1f, 1f, 1f);
                Bit.transform.position = charOutside.transform.position;
                Bit.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                charInt++;
                Loop();
                break;
            default:
                Loop();
                break;
            
        }
    }
    public void Previous()
    {
        switch (charInt)
        {
            case 1:
                isEllis = true;
                Bit.transform.position = charOutside.transform.position;
                Bit.transform.localScale = new Vector3(0.5f,0.5f,1f);
                Ellis.transform.position = charPosistion.transform.position;
                Ellis.transform.localScale = new Vector3(1f, 1f, 1f);
                charInt--;
                Loop();
                break;
            case 2:
                isEllis = true;
                Bit.transform.position = charPosistion.transform.position;
                Bit.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
                Ellis.transform.position = charOutside.transform.position;
                Ellis.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                charInt--;
                Loop();
                break;
            default:
                Loop();
                break;
            
        }
    }
    private void Loop()
    {
        if (charInt >= 2)
        {
            charInt = 1;
        }
        else
        {
            charInt = 2;
        }
    }
    public void StartGame()
    {
        if (isEllis)
        {
            // Если выбран Эллис
            SceneTransition.SwitchToScene("Level");
        }
        else
        {
            // Если выбран Бит
            SceneTransition.SwitchToScene("MainMenu");
        }
        
    }
}
