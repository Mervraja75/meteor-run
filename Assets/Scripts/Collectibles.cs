using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip coinSfx;      // assign the coins' sound
    private AudioSource audioSrc;  // reference to the Player's AudioSource that is in the inspector

    void Start()
    {
        
        PlayerController player = Object.FindFirstObjectByType<PlayerController>();
        if (player != null)
            audioSrc = player.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1) Play the coin sound
            if (audioSrc != null && coinSfx != null)
                audioSrc.PlayOneShot(coinSfx);

            // 2) Add 1 point to the score
            ScoreManager.instance.AddScore(1);

            // 3) Remove the coin
            Destroy(gameObject);
        }
    }
}