using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharController : MonoBehaviour
{   //Переменные
    public Rigidbody2D rb2d;
    public static int playerSpeed = 5;
    public float jumpPower;
    public int directionInput;
    public bool groundCheck;
    public bool facingRight = true;
    private Animator anim;
    public static bool isGrounded = false;
    public Transform groundCheckMain;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public GameObject quitButton, restartButton, LevelButton, tapButton, ResumeButton, ControlsButton;
    public static bool dead = false;
    private int extraJumps;
    public int extraJumpsValue;
    //Health
    public static int health = 3;
    public GameObject health1, health2, health3, emptyhealth1, emptyhealth2, emptyhealth3;
    int playerLayer, enemyLayer;
    bool coroutineAllowed = true;
    Color color;
    Renderer rend;
    public float KnifeValue = 3f;
    //Attack
    public Collider2D attackTrigger;
    public float AttackTimer;
    public float AttackCd = 0.1f;
    public Transform firePoint;
    public GameObject bullet;
    public bool isRun = false;
    public Collider2D picketFence;
    public Collider2D picketFenceOne;
    public GameObject KnifeActive, NoKnifeActive, KnifeActive1, NoKnifeActive1, KnifeActive2, NoKnifeActive2;
    void Start()
    {
        restartButton.SetActive(false);
        LevelButton.SetActive(false);
        quitButton.SetActive(false);
        ResumeButton.SetActive(false);
        ControlsButton.SetActive(false);
        //Подключение анимации и физики
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
        health1.SetActive(true);
        health2.SetActive(true);
        health3.SetActive(true);
        emptyhealth1.SetActive(false);
        emptyhealth2.SetActive(false);
        emptyhealth3.SetActive(false);
        //Health
        playerLayer = this.gameObject.layer;
        enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        rend = GetComponent<Renderer>();
        color = rend.material.color;
        //Attack
        attackTrigger.enabled = false;
        //Box 
        picketFence.enabled = true;
        //KnifeButton
        KnifeActive.SetActive(true);
        KnifeActive1.SetActive(true);
        KnifeActive2.SetActive(true);
        NoKnifeActive.SetActive(false);
        NoKnifeActive1.SetActive(false);
        NoKnifeActive2.SetActive(false);
    }
    //Анимация бега
    public void SrartRunAnim()
    {
        isRun = true;
    }
    public void EndRunAnim()
    {
        isRun = false;
    }
    void Update()
    {
        if ((directionInput < 0) && (facingRight))
        {
            Flip();
        }
        if ((directionInput > 0) && (!facingRight))
        {
            Flip();
        }
        groundCheck = true;
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }
        if (AttackTimer > 0f)
        {
            AttackTimer -= Time.deltaTime;
        }
        //KnifeButton
        if(KnifeValue == 2)
        {
            KnifeActive.SetActive(true);
            KnifeActive1.SetActive(true);
            KnifeActive2.SetActive(false);
            NoKnifeActive.SetActive(false);
            NoKnifeActive1.SetActive(false);
            NoKnifeActive2.SetActive(true);
        }
        if (KnifeValue == 1)
        {
            KnifeActive.SetActive(true);
            KnifeActive1.SetActive(false);
            KnifeActive2.SetActive(false);
            NoKnifeActive.SetActive(false);
            NoKnifeActive1.SetActive(true);
            NoKnifeActive2.SetActive(true);
        }
        if (KnifeValue == 0)
        {
            KnifeActive.SetActive(false);
            KnifeActive1.SetActive(false);
            KnifeActive2.SetActive(false);
            NoKnifeActive.SetActive(true);
            NoKnifeActive1.SetActive(true);
            NoKnifeActive2.SetActive(true);
        }
        //AnimationRunUpdate
        if (ButtonsScript.PauseActive == false &&  dead == false && isRun == true && isGrounded == true)
            {
                anim.SetBool("isRunning", true);
            }
        if (ButtonsScript.PauseActive == false && dead == false && isRun == false && isGrounded == true)
        {
            anim.SetBool("isRunning", false);
        }
        //Animation Pause
        if(ButtonsScript.PauseActive == true && isGrounded == true)
        {
            anim.Play("Stop");
            anim.SetBool("isRunning", false);
        }
    }
    //Attack
    public void Attack()
    {
        if (AttackTimer <= 0f && isGrounded != false && dead == false && ButtonsScript.PauseActive == false)
        {
            attackTrigger.enabled = true;
            anim.Play("Attack");
            playerSpeed = 0;
            Invoke("AttackEndTrigger", 0.25f);
            AttackTimer = AttackCd + 0.3f;
            Invoke("Go", 0.6f);

        }
    }
    public void Go()
    {
        playerSpeed = 5;
    }
    public void AttackEndTrigger()
    {
        attackTrigger.enabled = false;
    }
    //Knife
    public void KnifeStart()
    {
        if (KnifeValue > 0f && isGrounded != false && AttackTimer <= 0f && dead == false && ButtonsScript.PauseActive == false)
        {
            KnifeValue -= 1f;
            AttackTimer = AttackCd + 0.5f;
            anim.Play("Knife");
            Invoke("KnifeTransform", 0.45f);
            playerSpeed = 0;
            Invoke("Go", 0.55f);
        }
    }
    public void KnifeTransform()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(playerSpeed * directionInput, rb2d.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheckMain.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", isGrounded);
        anim.SetFloat("vSpeed", rb2d.velocity.y);
        if (!isGrounded)
        {
            return;
        }
    }
    //Движение
    public void Move(int InputAxis)
    {
        if (dead == false && ButtonsScript.PauseActive == false)
        {
            directionInput = InputAxis;
            playerSpeed = 5;
        }
    }
    //Прыжок
    public void Jump(bool isJump)
    {
        if (!dead && extraJumps > 0 && ButtonsScript.PauseActive == false)
        {
            isJump = groundCheck;
            if (groundCheck)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
                extraJumps--;
                anim.Play("Jump 0");
            }

        }
    }
    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    //Взаимодействие с врагом
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy" && EnemyController.isDead == false)
        {
            gameObject.layer = 11;
            Invoke("ChangeLayer", 2f);
            switch (health)
            {
                case 3:
                    health1.SetActive(true);
                    health2.SetActive(true);
                    health3.SetActive(true);
                    break;
                case 2:
                    Invoke("CaseTwo", 0.35f);
                    break;
                case 1:
                    Invoke("CaseOne", 0.35f);
                    break;

            }
            if (health <= 0)
            {
                dead = true;
                if (coroutineAllowed)
                {
                    StartCoroutine("Immortal");
                }
                playerSpeed = 0;
                anim.SetBool("Dead", true);
                Invoke("DeadIsTrue", 0.35f);

            }
        }
        if (coll.gameObject.tag == "PicketFence")
        {
            health = health - 1;
            gameObject.layer = 11;
            Invoke("ChangeLayer", 2f);
            switch (health)
            {
                case 3:
                    health1.SetActive(true);
                    health2.SetActive(true);
                    health3.SetActive(true);
                    break;
                case 2:
                    Invoke("CaseTwo", 0.15f);
                    break;
                case 1:
                    Invoke("CaseOne", 0.15f);
                    break;

            }
            if (health <= 0)
            {
                dead = true;
                if (coroutineAllowed)
                {
                    StartCoroutine("Immortal");
                }
                playerSpeed = 0;
                anim.SetBool("Dead", true);
                Invoke("DeadIsTrue", 0.15f);
                picketFence.enabled = false;
                picketFenceOne.enabled = false;

            }
        }
    }
    void DeadIsTrue()
    {
        restartButton.SetActive(true);
        gameObject.SetActive(true);
        quitButton.SetActive(true);
        LevelButton.SetActive(true);
        ResumeButton.SetActive(true);
        ControlsButton.SetActive(true);
        health1.SetActive(false);
        health2.SetActive(false);
        health3.SetActive(false);
        emptyhealth1.SetActive(true);
        emptyhealth2.SetActive(true);
        emptyhealth3.SetActive(true);
    }
    void ChangeLayer()
    {
        if (dead != true)
        {
            gameObject.layer = 9;
        }
    }
    void CaseOne()
    {
        health1.SetActive(true);
        health2.SetActive(false);
        health3.SetActive(false);
        emptyhealth1.SetActive(false);
        emptyhealth2.SetActive(true);
        emptyhealth3.SetActive(true);
        if (coroutineAllowed)
        {
            StartCoroutine("Immortal");
        }
    }
    void CaseTwo()
    {
        health1.SetActive(true);
        health2.SetActive(true);
        health3.SetActive(false);
        emptyhealth1.SetActive(false);
        emptyhealth2.SetActive(false);
        emptyhealth3.SetActive(true);
        if (coroutineAllowed)
        {
            StartCoroutine("Immortal");
        }
    }
    //Health 
    IEnumerator Immortal()
    {
        coroutineAllowed = false;
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        color.a = 0.5f;
        rend.material.color = color;
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        color.a = 1f;
        rend.material.color = color;
        coroutineAllowed = true;

    }
}