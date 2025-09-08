using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)   // Work to shake the camera
    {
        Vector3 originalPos = transform.localPosition;          // this is to Save camera’s starting point

        float elapsed = 0.0f;                                   // this is to track how long the camera has been shaking

        while (elapsed < duration)                              // Let it run until time runs out
        {
            float x = Random.Range(-1f, 1f) * magnitude;        //  horizontal offset
            float y = Random.Range(-1f, 1f) * magnitude;        //  vertical offset

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z); // Apply offset

            elapsed += Time.deltaTime;                          // Add time each frame

            yield return null;                                  // Wait until next frame
        }

        transform.localPosition = originalPos;                  // Reset position when done
    }
}