using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("Hit / i-frames")]
    public float invincibilityTime = 1f;                 // time player avoids further hits
    public SpriteRenderer spriteRenderer;                // to assign player visual here in Inspector

    [Header("Knockback")]
    public float knockbackForce = 5f;                    // how strong the push or force of getting hit from the meteor is
    public Vector2 knockbackDirection = new Vector2(-1, 1);

    private bool isInvincible = false;
    private float invincibilityTimer = 0f;

    private Rigidbody2D rb;
    private PlayerHealth health;                         // reference to PlayerHealth

    void Start()
    {
        health = GetComponent<PlayerHealth>();           // find PlayerHealth on the same object
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer == null)                      // try auto-assign if forgot to input in SpriteRenderer
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (!isInvincible) return;

        invincibilityTimer -= Time.deltaTime;

        // Flash effect (on/off) – guard for null
        if (spriteRenderer != null)
            spriteRenderer.enabled = (Mathf.FloorToInt(invincibilityTimer * 10f) % 2 == 0);

        if (invincibilityTimer <= 0f)
        {
            isInvincible = false;
            if (spriteRenderer != null) spriteRenderer.enabled = true;
        }
    }

    public void TakeHit()
    {
        // If shield is active, don't worry about the hit
        if (ShieldIsActive()) return;

        ApplyHitEffects();
    }

    public void TakeHitFrom(Transform source)
    {
        // If shield is active, consume the hit and pop the meteor
        if (ShieldIsActive())
        {
            if (source != null) Destroy(source.gameObject); // pop the meteor
            return;
        }

        ApplyHitEffects();
    }

    // ---------- INTERNAL HELPERS ----------

    // Checks if a PlayerShield component exists and is currently active
    private bool ShieldIsActive()
    {
        PlayerShield shield = GetComponent<PlayerShield>();
        return (shield != null && shield.isActive);
    }

    // Basic Knowledge when the hit actually lands
    private void ApplyHitEffects()
    {
        if (!isInvincible)
        {
            isInvincible = true;
            invincibilityTimer = invincibilityTime;

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;                               // reset current motion
                rb.AddForce(knockbackDirection.normalized * knockbackForce,     // apply knockback
                           ForceMode2D.Impulse);
            }

            Debug.Log("Player hit!");
        }

        if (health != null)
        {
            health.TakeDamage(1);                                              // reduce health
        }
    }
}