using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePersist : MonoBehaviour
{
    void Awake()
    {
        if(FindObjectsOfType<GamePersist>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {

            DontDestroyOnLoad(gameObject);
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

}
