using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 2f;

    new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2 (transform.localScale.x * enemySpeed, rigidbody2D.velocity.y);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        transform.localScale = new Vector2(-transform.localScale.x, 1);
    }


}
