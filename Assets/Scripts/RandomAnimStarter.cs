using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var anim = GetComponent<Animator>();
        anim.Play(0, 0, Random.Range(0f, 1f));
    }


}
