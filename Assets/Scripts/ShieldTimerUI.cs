using UnityEngine;
using TMPro;

public class ShieldTimerUI : MonoBehaviour
{
    public PlayerShield shield;      // Player
    public TMP_Text timerText;       // Time (text)

    void Reset() { timerText = GetComponent<TMP_Text>(); }

    void Update()
    {
        if (shield != null && shield.isActive && shield.timeLeft > 0f)
        {
            int secs = Mathf.CeilToInt(shield.timeLeft);
            timerText.text = $"Shield: {secs}s";
            timerText.enabled = true;
        }
        else
        {
            timerText.enabled = false;   // hide when no shield
        }
    }
}