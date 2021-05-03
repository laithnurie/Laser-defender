using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;

    private float bulletHeight;
    private float yMax;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        bulletHeight = spriteRenderer.bounds.size.y;

        Camera gameCamera = Camera.main;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + bulletHeight;
    }

    void Update()
    {
        Travel();
    }

    private void Travel()
    {
        float deltaY = Time.deltaTime * moveSpeed;
        float newYPos = transform.position.y + deltaY;
        transform.position = new Vector2(transform.position.x, newYPos);

        if (newYPos > yMax)
        {
            Destroy(gameObject);
        }
    }
}
