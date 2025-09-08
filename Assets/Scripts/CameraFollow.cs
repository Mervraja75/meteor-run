using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                        // the player the camera is following
    public Vector3 offset;                          // adjusts the camera's position relative to the player
    public float smoothTime = 0.3f;                 //controls how fast camera catches up to the player (lower = faster, higher = slower)

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        if(target != null)
        {
          Vector3 targetPosition = target.position + offset;
          transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);  //smoothly moves the camera to the target position

        }
    }
}
