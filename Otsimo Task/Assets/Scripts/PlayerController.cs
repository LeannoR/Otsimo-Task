using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rigidBody;
    private GameObject handR;
    private GameObject handL;
    private SnapSlot snapSlotR;
    private SnapSlot snapSlotL;

    private float jumpForce = 8f;
    private float moveSpeed = 5f;
    private float horizontalMove;

    private bool isGrounded = false;
    public bool isFacingRight = true;

    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        handR = GameObject.Find("Hand_R");
        handL = GameObject.Find("Hand_L");
        snapSlotR = handR.GetComponent<SnapSlot>();
        snapSlotL = handL.GetComponent<SnapSlot>();
    }
    public void Update()
    {
        CharacterFacingRightWayController();
        Movement();
        Jump();
        Attack();
    }
    public void Movement()
    {
        if(Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRunning", true);
            transform.position = transform.position + new Vector3(1,0) * moveSpeed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isRunning", true);
            transform.position = transform.position + new Vector3(-1, 0) * moveSpeed * Time.deltaTime;
        }
        else if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("isRunning", false);
        }
    }
    public void CharacterFacingRightWayController()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (horizontalMove > 0 && !isFacingRight)
        {
            FlipCharacter();
        }
        else if (horizontalMove < 0 && isFacingRight)
        {
            FlipCharacter();
        }
    }

    public void FlipCharacter()
    {
        isFacingRight = !isFacingRight;

        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.W) && isGrounded == true)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Space) && snapSlotR.isEquipped == true)
        {
            animator.SetTrigger("attackRight");
        }
        else if(Input.GetKeyDown(KeyCode.Space) && snapSlotL.isEquipped == true)
        {
            animator.SetTrigger("attackLeft");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

}
