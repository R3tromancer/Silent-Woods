using UnityEngine;

public class GamePersist : MonoBehaviour
{
    void Awake()
    {
        if(FindObjectsByType<GamePersist>(FindObjectsSortMode.None).Length > 1)
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
