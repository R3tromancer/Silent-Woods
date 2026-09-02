using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LoadTime = 0.6f;

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        int cScene = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(LoadTime);
        FindObjectOfType<GamePersist>().SelfDestruct();
        if(cScene == 3) cScene = -1;
        SceneManager.LoadScene(cScene + 1);
    }


}
