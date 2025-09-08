using UnityEngine;

public class LoopingBackground : MonoBehaviour //width of the background tile
{
    public float tileWidth = 20f;
    private Transform cam;                    //Tracks the camera’s X position


    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (cam.position.x - transform.position.x > tileWidth)          //Checks if the tile is behind the camera
        {
            transform.position += new Vector3(tileWidth * 3f, 0f, 0f); //Moves the tile forward to create the loop
        }
    }
}