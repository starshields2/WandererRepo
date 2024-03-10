using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public float moveDistance = 25; // Distance to move
    public float moveSpeed = 5f; // Speed of movement

    private Vector3 startPos; // Starting position

    void Start()
    {
        startPos = transform.position; // Store the starting position
      //  StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            float pingPong = Mathf.PingPong(Time.time * moveSpeed, moveDistance); // PingPong value between 0 and moveDistance
            Vector3 newPosition = startPos + Vector3.forward * pingPong; // Calculate new position
            transform.position = newPosition; // Move the object
            yield return null; // Wait for the next frame
        }
    }
}
