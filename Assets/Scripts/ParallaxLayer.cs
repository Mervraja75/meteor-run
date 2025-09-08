using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor = 0.5f; // How much the layer moves along to the camera (0 = no movement, 1 = full movement)
    private Transform cam;              // Reference to the main camera
    private Vector3 previousCamPosition; // how far the camera has moved since the last frame

   
    void Start()
    {
        cam = Camera.main.transform;
        previousCamPosition = cam.position;
    }

   
    void Update()
    {
        
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cam.position - previousCamPosition;
        
        //moves the background at a slower rate than the camera
        transform.position += new Vector3(deltaMovement.x * parallaxFactor, deltaMovement.y * parallaxFactor, 0); 
        previousCamPosition = cam.position;
    }
}
