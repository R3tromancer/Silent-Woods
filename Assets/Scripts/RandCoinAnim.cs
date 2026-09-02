using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandCoinAnim : MonoBehaviour
{
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play("CoinAn", 0, Random.Range(0f,1f));
    }
}
