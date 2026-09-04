using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{

    [SerializeField] float cloudSpeed = 1f; 

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector2(transform.localPosition.x + (Time.deltaTime * cloudSpeed), transform.localPosition.y);
    }
}
