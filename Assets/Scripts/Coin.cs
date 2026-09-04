using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] AudioClip coinSound;
    [SerializeField] int coinScore = 1000;
    [SerializeField] float soundVolume = 0.5f;

    bool wasCollected = false;

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindAnyObjectByType<GameSession>().ProcessCoin(coinScore);
            AudioSource.PlayClipAtPoint(coinSound,Camera.main.transform.position, soundVolume);
            Destroy(gameObject);
        }
    }

}
