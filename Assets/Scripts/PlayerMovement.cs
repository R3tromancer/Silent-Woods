using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float gravity = 1f;
    [SerializeField] float gravityDown = 2f;
    [SerializeField] float dashSpeed = 4f;
    [SerializeField] float dashTime = 0.25f;

    [Header("Dying Attributes")]
    [SerializeField] float deathStopTime = 0.068f;
    [SerializeField] float deathThrowAngle = 5f;
    [SerializeField] Vector2 throwValue;


    Vector2 moveInput;
    new Rigidbody2D rigidbody2D;
    CapsuleCollider2D capsuleCollider2D;
    BoxCollider2D boxCollider2D;
    Animator animator;
    PlayerInput playerInput;
    SpriteRenderer spriteRenderer;
    Color color;
    PolygonCollider2D polygonCollider2D;

    bool isDashing = false;
    bool isDead = false;
    bool isDownfall = false;
    bool canDash = false;
    bool jumpAssist = false;
    bool isJumping = false;

    void Start()
    {

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D.gravityScale = gravity;
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        if(isDead) return;
        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
            StartCoroutine(Die());

        if (isDashing) return;
        Run();
        FLipSprite();
        FallGravityChange();
        if(!canDash && (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) || boxCollider2D.IsTouchingLayers(LayerMask.GetMask("bounce")) ))
        canDash = true;
        if(jumpAssist && capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            jumpAssist = false;
            isJumping = false;
            //rigidbody2D.gravityScale = gravity; 
            rigidbody2D.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
        }
    }

    private IEnumerator Die()
    {

        isDead = true;
        playerInput.enabled = false;
        animator.enabled = false;
        rigidbody2D.freezeRotation = false;
        color.r = 255;
        color.a = 255;
        spriteRenderer.color = color;
        rigidbody2D.AddTorque(deathThrowAngle);
        rigidbody2D.AddForce(throwValue , ForceMode2D.Impulse);
        yield return new WaitForSeconds(deathStopTime);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
    void Run()
    {
        Vector2 velocity = new Vector2 (moveInput.x * moveSpeed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = velocity;

        bool isMoving = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;       
        animator.SetBool("isWalking", isMoving);
    }
    private void FLipSprite()
    {
        bool isMoving = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        if(isMoving)
        transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f);
    }
    void OnJump(InputValue value)
    {
        if(value.isPressed)
        if(polygonCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isJumping = true;
        rigidbody2D.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
        }
        else
        if(boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        if(capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        rigidbody2D.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
        else
        jumpAssist = true;
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnDash(InputValue value)
    {
        if(value.isPressed && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        animator.SetBool("hasDashed", isDashing);
        rigidbody2D.gravityScale = 0f;
        rigidbody2D.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        yield return new WaitForSeconds(dashTime);
        if(isDownfall)
        rigidbody2D.gravityScale = gravityDown;
        else
        rigidbody2D.gravityScale = gravity;
        isDashing = false;
        animator.SetBool("hasDashed", isDashing);
    }
    void FallGravityChange()
    {
        if(rigidbody2D.velocity.y <= 0 && !isDownfall && !boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isDownfall = true;
            rigidbody2D.gravityScale = gravityDown;
            Debug.Log("DNNNN");
        }
        else if(rigidbody2D.velocity.y > 0 && isDownfall && boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isDownfall = false;
            rigidbody2D.gravityScale = gravity;
            Debug.Log("UPPPP");
        }
    }
    public void mossBounce(float bouncePower)
    {
        rigidbody2D.AddForce(new Vector2(0f, bouncePower), ForceMode2D.Impulse);
    }
}
