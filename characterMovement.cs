using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    Animator animator;
    SpriteRenderer renderer;
    Rigidbody2D rigid;

    public float movePower = 1f;
    public float jumpPower = 1f;

    Vector3 movement;
    bool isJumping = false;


    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            animator.SetBool("isMoving", false);
        }

        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetBool("isMoving", true);
            renderer.flipX = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool("isMoving", true);
            renderer.flipX = false;
        }

        if (Input.GetButtonDown("Jump")){
            isJumping = true;
            animator.SetBool("isJumping", true);
            animator.SetTrigger("doJumping");
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.layer == 12 && rigid.velocity.y < 0)
        {
            animator.SetBool("isJumping", false);
        }

        if(other.gameObject.tag == "Monster" && !other.isTrigger && rigid.velocity.y < -1.5f)
        {
            monsterMovement monster = other.gameObject.GetComponent<monsterMovement>();
            monster.Die();

            Vector2 killVelocity = new Vector2(0, 3f);
            rigid.AddForce(killVelocity, ForceMode2D.Impulse);

            ScoreManager.setScore(monster.score);
        }

        if(other.gameObject.tag == "Monster" && !other.isTrigger && rigid.velocity.y==0)
        {
            GameManager.EndGame();
        }


        if (other.gameObject.tag == "bronze_coin")
        {
            BlockStatus bronze_coin = other.gameObject.GetComponent<BlockStatus>();
            ScoreManager.setScore((int)bronze_coin.value);

            Destroy(other.gameObject, 0f);
        }

        if (other.gameObject.tag == "silver_coin")
        {
            BlockStatus_2 silver_coin = other.gameObject.GetComponent<BlockStatus_2>();
            ScoreManager.setScore((int)silver_coin.value);

            Destroy(other.gameObject, 0f);
        }

        if (other.gameObject.tag == "gold_coin")
        {
            BlockStatus_3 gold_coin = other.gameObject.GetComponent<BlockStatus_3>();
            scoreManager.setScore((int)gold_coin.value);

            Destroy(other.gameObject, 0f);
        }

        if(other.gameObject.tag == "end")
        {
            GameManager.EndGame();
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Detach : "+ other.gameObject.layer);
    }


    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;

        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;

    }

    void OnColliderEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster" && !other.isTrigger && rigid.velocity.y<-0.5f)
        {
            monsterMovement monster = other.gameObject.GetComponent<monsterMovement>();
            monster.Die();

            Vector2 killVelocity = new Vector2(0, 2f);
            rigid.AddForce(killVelocity, ForceMode2D.Impulse);

           // scoreManager.setScore(monster.score);
        }


    }

    void Jump()
    {
        if (!isJumping) return;

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;

    }
}
