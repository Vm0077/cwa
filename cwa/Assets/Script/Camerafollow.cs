using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;  // Drag the Player GameObject here in the Inspector
    public Vector3 offset;    // Offset to maintain distance from the player

    void LateUpdate()
    {
        // Update the camera's position to follow the player
        transform.position = player.position + offset;
        // Optional: Uncomment if you want the camera to also rotate with the player
        // transform.LookAt(player);
    }
}
