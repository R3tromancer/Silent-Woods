using UnityEngine;

public class JumpMoss : MonoBehaviour
{
    [SerializeField] float bouncePower = 10f;
    PolygonCollider2D polygonCollider2D;
    Animator animator;

    void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
       if(other.otherCollider == polygonCollider2D)
       {
        FindAnyObjectByType<PlayerMovement>().mossBounce(bouncePower);
        animator.SetTrigger("isSteppedOn");
       }
    }

}
