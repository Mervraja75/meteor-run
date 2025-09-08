using UnityEngine; // Unity types

public class Meteor : MonoBehaviour
{
    public float rotationSpeed = 200f;          // How fast the meteor spins
    public GameObject explosionPrefab;          // The meteor explosion prefab
    public LayerMask groundLayer;               // Meteor recognizes the ground

    public AudioClip playerHitSfx;              // sound when meteor hits the player

    void Update() 
    {
        
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // meteor spin while falling
    }

    void OnCollisionEnter2D(Collision2D collision)             // Called when meteor hits player or ground
    {
         // 1) If meteor hit the Player 
        if (collision.collider.CompareTag("Player"))
        {
            // Play hit sound
            AudioSource src = collision.collider.GetComponent<AudioSource>();
            if (playerHitSfx && src)
            {
                src.PlayOneShot(playerHitSfx);
            }
            else if (playerHitSfx && Camera.main) // fallback
            {
                AudioSource.PlayClipAtPoint(playerHitSfx, Camera.main.transform.position, 0.9f);
            }

            // Explosion VFX
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // Damage player
            PlayerDamage dmg = collision.collider.GetComponent<PlayerDamage>();
            if (dmg != null)
            {
                dmg.TakeHitFrom(transform);
            }

            // Camera shake when hitting player
            CameraShake shake = Camera.main.GetComponent<CameraShake>();
            if (shake != null)
            {
                StartCoroutine(shake.Shake(0.2f, 0.3f)); // duration + strength
            }

            Destroy(gameObject); // Remove meteor after hitting player
            return;              // Exit
        }

        // 2) If meteor hit the Ground
        bool hitGround = (groundLayer.value & (1 << collision.collider.gameObject.layer)) != 0;
        if (hitGround)
        {
            // Explosion VFX
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject); // Remove meteor after hitting ground
            return;
        }
    }
}