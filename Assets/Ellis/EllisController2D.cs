using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EllisController2D : MonoBehaviour
{
    public float speed,moveinputx,moveinputy;

    private Rigidbody2D rb;
    private Animator anim;
    
    public bool facingRight = true;
    public TextMeshPro dashKD, RStackText;
    public float dashTimer = 2;

    public Image R2,R2UI, QKD;
    public float fill;

    public bool canDash;
    public bool isR;
    Vector2 savepos;
    public GameObject Qobj, Robj, RobjOnEllis, emptyPotion, fullPotion;
    


    public int QStack = 0;
    public bool canQ = false;
    float timer = 2f;
    

    public int health = 100;


    private void Start()
    {
        canDash = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Robj.SetActive(false);
        Qobj.SetActive(false);
    }

    private void Update()
    {
        // Получение данных о направлении
        moveinputx = Input.GetAxis("Horizontal");
        moveinputy = Input.GetAxis("Vertical");
        // Таймер рывка
        if (!canDash)
        {
            dashTimer -= Time.deltaTime;
          
            if (dashTimer < 0)
            {
               
                dashTimer = 2;
                canDash = true;
            }
        }
        // Ограничение максимального здоровья
        if (health >= 100)
        {
            health = 100;
        }
        // Активация рывка
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        // Анимация бега
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("EllisRun", true);
        }
        // Анимация ожидания
        else
        {
            anim.SetBool("EllisRun", false);
        }
        

        // Таймер Q
        if (!canQ)
        {
            timer -= Time.deltaTime;
            float fillQ = timer / 2;
            QKD.fillAmount = fillQ;
            if (timer < 0)
            {
                QKD.fillAmount = 0;
                canQ = true;
                timer = 2f;

            }
        }
        // Активация R скилла
        if (Input.GetKeyDown(KeyCode.R) && QStack == 5)
        {
            StartCoroutine("EliisRDo");
        }
        // Визуализация стаков
        if (QStack == 0)
        {
            RStackText.text = "0";
            fill = 0f;
            R2.fillAmount = fill;
            R2UI.fillAmount = fill;
        }
        if (QStack == 1)
        {
            RStackText.text = "1";
            fill = 0.2f;
            R2.fillAmount = fill;
            R2UI.fillAmount = fill;
        }
        if (QStack == 2)
        {
            
            RStackText.text = "2";
            fill = 0.4f;
            R2.fillAmount = fill;
            R2UI.fillAmount = fill;
        }
        if (QStack == 3)
        {
            RStackText.text = "3";
            fill = 0.6f;
            R2.fillAmount = fill;
            R2UI.fillAmount = fill;
        }
        if (QStack == 4)
        {
            RStackText.text = "4";
            fill = 0.8f;
            R2.fillAmount = fill;
            R2UI.fillAmount = fill;
        }
        if (QStack == 5)
        {
            RStackText.text = "5";
            fill = 1f;
            R2.fillAmount = fill;
            R2UI.fillAmount = fill;
        }
        if (health <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }
        // Сохранение позиции R
        if (isR)
        {
            Robj.transform.position = savepos;
        }
    }

    private void FixedUpdate()
    {
       // Движение и разворот
       rb.velocity = new Vector2(moveinputx * speed, moveinputy * speed);
       
       if (facingRight == false && moveinputx > 0)
       {
            Flip();
       }
       else if (facingRight == true && moveinputx < 0)
       {
            Flip();
       }
       
    }
    private void OnCollisionEnter2D(Collision2D reload)
    {
        if (reload.gameObject.CompareTag("Potion"))
        {
            health += 30;
            emptyPotion.SetActive(true);
            Destroy(fullPotion);
        }
       
    }
    private void OnCollisionStay2D(Collision2D reload)
    {
        if (reload.gameObject.CompareTag("Finish") && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("End");
        }
    }
    // Метод рывка
    IEnumerator Dash()
    {
        RobjOnEllis.SetActive(false);
        canDash = false;
        speed = 40;
        anim.SetTrigger("EllisDash"); 
        yield return new WaitForSeconds(1f);
        RobjOnEllis.SetActive(true);
        speed = 20;
    }
    // Метод разворота
    public void Flip()
    {
        //  if (!photonView.IsMine) return;
        facingRight = !facingRight;
        transform.Rotate(0f,-180,0f);
    }
    // Метод Q
    public IEnumerator EllisQDo()
    {
        
        anim.SetTrigger("EllisQEllis");
        Qobj.SetActive(true);
        if (QStack >= 5)
            QStack = 5;
        yield return new WaitForSeconds(2f);
        Qobj.SetActive(false);
    }
    // Метод R
    IEnumerator EliisRDo()
    {
        health += 30;
        savepos = Robj.transform.position;
        isR = true;
        QStack = 0;
        Robj.SetActive(true);
        RobjOnEllis.SetActive(false);
        yield return new WaitForSeconds(4f);
        Robj.SetActive(false);
        RobjOnEllis.SetActive(true);
        Robj.transform.localPosition = new Vector3(-0, -0, 0f);
        isR = false;
    }
   
}
