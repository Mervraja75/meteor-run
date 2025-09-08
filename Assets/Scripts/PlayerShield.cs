using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public bool isActive = false;
    public float timeLeft = 0f;
    public GameObject shieldVisual;

    public void Activate(float duration)
    {
        Debug.Log($"[PlayerShield] Activate called. Duration={duration}, BEFORE reset timeLeft={timeLeft}"); //shows more info in the console

        isActive = true;
        timeLeft = duration;  // reset to full

        if (shieldVisual)
            shieldVisual.SetActive(true);

        Debug.Log($"[PlayerShield] AFTER reset timeLeft={timeLeft}"); //shows more info in the console
    }

    void Update()
    {
        if (!isActive) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            Debug.Log("[PlayerShield] Timer ended. Turning shield OFF");

            isActive = false;
            timeLeft = 0f;

            if (shieldVisual)
                shieldVisual.SetActive(false);
        }
    }
}