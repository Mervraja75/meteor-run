using UnityEngine; // Unity engine types

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;             // Player's auto-run speed
    public float jumpForce = 10f;            // Base jump strength
    public float doubleTapBoost = 1.5f;      // Multiplier when you double-tap Space quickly
    public float doubleTapThreshold = 0.4f;  // Max time (sec) between control taps to count as a double
    public Transform groundCheck;            // Empty child placed slightly below feet to make sure player is on the ground
    public float groundCheckRadius = 0.25f;  // Overlap circle radius for ground detection
    public LayerMask groundLayer;            // Which layers count as ground below the player

    /*public float tiltAmount = 0f;           Visual tilt amount when pressing up/down */ //(will be using this in Meteor Run 2)

    // ---- Audio ----
    public AudioClip jumpSfx;                // Assign in Inspector
    private AudioSource audioSrc;            // Cached source on Player (optional)

    // ---- Player State ----
    public bool isGrounded;                  // True when standing on ground

    // ---- Inspectors ----
    private Rigidbody2D rb;                  // Rigidbody2D reference
    private float lastJumpTime = -1f;        // For double-tap timing
    private float verticalInput = 0f;        // W/S or Up/Down arrows

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>(); // OK if null; we have a fallback
    }

    void Update()
    {
        // Ground check (small circle under feet)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Read vertical input (-1, 0, 1)
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jump (only if grounded)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            float timeSinceLastJump = Time.time - lastJumpTime;
            float jumpPower = jumpForce;
            if (timeSinceLastJump <= doubleTapThreshold)
                jumpPower *= doubleTapBoost;

            // Apply vertical velocity
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);

            // Play jump SFX
            if (jumpSfx)
            {
                if (audioSrc) audioSrc.PlayOneShot(jumpSfx);
                else AudioSource.PlayClipAtPoint(jumpSfx, transform.position, 0.8f);
            }

            // Remember time for double-tap detection
            lastJumpTime = Time.time;
        }

        //(Tilting will be used on Meteor Run 2)

        /* Visual tilt for feedback
        float targetZRotation = -verticalInput * tiltAmount;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetZRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);*/
    }

    void FixedUpdate()
    {
        // Keep current vertical velocity (gravity/jumps)
        float vy = rb.linearVelocity.y;

        // Gentle vertical move while holding up/down (Will be updated more in Meteor Run 2)
        if (verticalInput != 0f)
            vy += verticalInput * 0.5f;

        
        rb.linearVelocity = new Vector2(moveSpeed, Mathf.Clamp(vy, -10f, 10f));
    }

    // Visualize groundCheck in Scene view
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}