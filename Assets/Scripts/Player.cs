using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float speedBonus;
    public float jumpForce;

    private float speedStart;

    private bool isGrounded;

    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public int score;
    public Text scoreText;

    public float timerSpeed;
    public float timerSpeedMax;

    public float timerScale;
    public float timerScaleMax;

    public void SpeedBonus()
    {
        timerSpeed = timerSpeedMax;
    }
    public void ScaleBonus()
    {
        timerScale = timerScaleMax;
    }

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        speedStart = speed;

        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    private void Update()
    {        
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        //сохраняем позицию игрока
        Vector3 positon = transform.position;

        //прибавляем к сохраненным координатам ввод с клавиатуры
        positon.x += Input.GetAxis("Horizontal")/10 * speed;

        //присваиваем игроку новую позицию
        transform.position = positon;

        if (Input.GetAxis("Horizontal") != 0)
        {

            if (Input.GetAxis("Horizontal") > 0)
            {
                //Debug.Log(false);
                spriteRenderer.flipX = false;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                //Debug.Log(true);
                spriteRenderer.flipX = true;
            }

            animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 0);
        }

        BonusCheck();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void BonusCheck()
    {
        if (timerSpeed > 0)
        {
            speed = speedBonus;

            timerSpeed--;
        }
        else
        {
            speed = speedStart;
        }

        if (timerScale > 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);

            timerScale--;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void AddCoin(int count)
    {
        score += count;
        scoreText.text = score.ToString();
    }

    private void Jump()
    {
            isGrounded = false;
            rigidBody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
