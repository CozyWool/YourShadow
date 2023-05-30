using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Button button00, button01, button02,
                  button10, button11, button12,
                  button20, button21, button22;

    public bool player1 = true;

    public Canvas canvasWin;
    public Text textWin;

    int counter;

    // Start is called before the first frame update
    void Start()
    {
        canvasWin.enabled = false;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Click(Button btn)
    {
        if (btn.GetComponentInChildren<Text>().text == "")
        {
            if (player1)
            {
                btn.GetComponentInChildren<Text>().text = "X";
            //    btn.GetComponent<Graphic>().material.color = new Color(255/255f, 165/255f, 0/255f);  //Color.cyan;
            }
            else
            {
                btn.GetComponentInChildren<Text>().text = "O";
      //          btn.GetComponent<Graphic>().color = Color.yellow;
            }

            // Горизонтали
            Win(button00, button01, button02);
            Win(button10, button11, button12);
            Win(button20, button21, button22);

            // Вертикали
            Win(button00, button10, button20);
            Win(button01, button11, button21);
            Win(button02, button12, button22);

            // Диагонали
            Win(button00, button11, button22);
            Win(button02, button11, button20);

            //Уголки
            Win(button00, button01, button10);
            Win(button01, button02, button12);
            Win(button10, button20, button21);
            Win(button21, button22, button12);

            counter++;
            if (counter == 9 && textWin.text == "-")
            {
                textWin.text = "Ничья!";
                canvasWin.enabled = true;
            }

            player1 = !player1;
        }
    }

    public void Win(Button b1, Button b2, Button b3)
    {
        string s1 = b1.GetComponentInChildren<Text>().text;
        string s2 = b2.GetComponentInChildren<Text>().text;
        string s3 = b3.GetComponentInChildren<Text>().text;

        bool b = (s1 == s2) && (s2 == s3) && (s3 != "");

        if (b)
        {
            if (player1) textWin.text = "Выиграли крестики";
            else textWin.text = "Выиграли нолики";

            canvasWin.enabled = true;
            // canvasWin.enabled = b;

           // b1.GetComponent<Graphic>().color = Color.green;
          //  b2.GetComponent<Graphic>().color = Color.green;
          //  b3.GetComponent<Graphic>().color = Color.green;
        }

    }


    public void Restart()
    {
        SceneManager.LoadScene(0);
    }








    private void OnGUI()
    {
        //GUI.Button(new Rect(330, 120, 100, 20), "!");
    }
}