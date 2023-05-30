// using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EllisController : MonoBehaviour
{
    public float speed, jumpforce;
    private float moveInput;
    public Rigidbody2D rb;

    public bool facingRight = true;

    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float checkRadius;

    public float qTimer = 3;
    public bool canQ;
    public GameObject Qobj, Robj, RobjOnEllis;
    public CapsuleCollider2D QHitBox;

    public Animator Q, anim, RQ;

    public int health = 100;
    public bool canJump;

    public float damage = 25;

    public int QStack = 0, QStackCurrent = 0, QStackCurrentHP = 0;
    public Image R2;
    public float fill;

    public bool isR;
    Vector2 savepos;

    public GameObject fullPotion, emptyPotion;




    int playerObject, collideObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObject = LayerMask.NameToLayer("Player");
        collideObject = LayerMask.NameToLayer("Collide");
        Physics2D.queriesStartInColliders = false;
        anim = GetComponent<Animator>();
        Qobj.SetActive(false);
        QHitBox.enabled = false;
        fill = 0f;
        Robj.SetActive(false);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);



        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

    }
    void Update()
    {
        if (rb.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(playerObject, collideObject, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(playerObject, collideObject, false);
        }
        if (isGrounded && Input.GetKey(KeyCode.A) || isGrounded && Input.GetKey(KeyCode.D))
        {
            anim.SetBool("EllisRun", true);
        }
        else
        {
            anim.SetBool("EllisRun", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity = Vector2.up * jumpforce;

            anim.SetTrigger("EllisJump");
        }
        if (isGrounded)
        {
            canJump = true;
        }

        else
        {
            canJump = false;
        }
        if (!canQ)
        {

            qTimer -= Time.deltaTime;
            if (qTimer <= 0)
            {
                qTimer = 8;
                canQ = true;

            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && canQ)
        {
            StartCoroutine("EliisQDo");
        }
        if (Input.GetKeyDown(KeyCode.R) && QStack == 5)
        {
            StartCoroutine("EliisRDo");
        }
        if (Input.GetKey(KeyCode.S))
        {
            StartCoroutine("JumpOff");
        }
        if (QStack == 0)
        {
            fill = 0f;
            R2.fillAmount = fill;
        }
        if (QStack == 1)
        {
            fill = 0.2f;
            R2.fillAmount = fill;
        }
        if (QStack == 2)
        {
            fill = 0.4f;
            R2.fillAmount = fill;
        }
        if (QStack == 3)
        {
            fill = 0.6f;
            R2.fillAmount = fill;
        }
        if (QStack == 4)
        {
            fill = 0.8f;
            R2.fillAmount = fill;
        }
        if (QStack == 5)
        {
            fill = 1f;
            R2.fillAmount = fill;
        }
        if (isR)
        {
            Robj.transform.position = savepos;
        }
       

    }


    void Flip()
    {
        //  if (!photonView.IsMine) return;
        facingRight = !facingRight;
        transform.Rotate(0f, -180f, 0f);
    }
    private void OnCollisionEnter2D(Collision2D reload)
    {
        /* if (reload.gameObject.tag == "death")
         {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
         }*/

        if (reload.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (reload.gameObject.CompareTag("Potion"))
        {
            health += 30;
            emptyPotion.SetActive(true);
            fullPotion.SetActive(false);
        }
    }

    

    IEnumerator JumpOff()
    {

        Physics2D.IgnoreLayerCollision(playerObject, collideObject, true);
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreLayerCollision(playerObject, collideObject, false);

    }

    IEnumerator EliisQDo()
    {
        canQ = false;
        Qobj.SetActive(true);
        Q.Play("QQQQQQQQQQQQQEllis");
        anim.SetTrigger("EllisQAttack");
        yield return new WaitForSeconds(0.5f);
        RQ.SetTrigger("RQ");
        if (facingRight)
            transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(5f, 0f, 0f), 5f);
        else if (!facingRight)
            transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(-5f, 0f, 0f), 5f);
        yield return new WaitForSeconds(0.3f);
        QHitBox.enabled = true;
        yield return new WaitForSeconds(0.2f);
        QHitBox.enabled = false;
        Qobj.SetActive(false);
    }
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
        Robj.transform.localPosition = new Vector3(-48.98f, -28.95f, 0f);
        isR = false;
    }

}
