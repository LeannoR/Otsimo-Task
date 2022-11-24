using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    private float moveSpeed = 5f;
    private float horizontalMove;

    public bool isFacingRight = true;

    public void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void Update()
    {
        CharacterFacingRightWayController();
        Movement();
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

    public void FlipCharacter()
    {
        isFacingRight = !isFacingRight;

        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
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
}
