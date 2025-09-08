using UnityEngine;

public class LoopingGround : MonoBehaviour
{
    public Transform player;
    public float tileLength = 20f;

    private void Update()
    {
        if (player.position.x - transform.position.x > tileLength)
        {
            // Move this ground tile by 3 tile lengths and goes in a loop
            transform.position += new Vector3(tileLength * 3f, 0f, 0f);
        }
    }
}