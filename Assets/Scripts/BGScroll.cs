using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public float tileSizeZ = 10.0f;

    private Vector3 startPosition;

    public int direction = 1;

    public void ToggleDirection()   // set direction to -1 to scroll left
    {
    direction *= -1;
    }

    void Start () {
        startPosition = transform.position;

        // Calculate tileSizeZ based on the size of the background image
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        tileSizeZ = spriteRenderer.sprite.bounds.size.x / transform.localScale.x;
    }

    void Update () {            // updated update to have direction variable for scroll
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed * direction, tileSizeZ);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}

/*
public float scrollSpeed = 0.5f;
    public float tileSizeZ = 10.0f;

    private Vector3 startPosition;

    void Start () {
        startPosition = transform.position;
    }

    void Update () {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.right * newPosition;
    }





    void Update () {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.right * newPosition;
    }
*/