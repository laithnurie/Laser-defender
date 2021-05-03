using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] GameObject bullet;

    float xMin;
    float xMax;

    float yMin;
    float yMax;

    float playerWidth;
    float playerHeight;

    Coroutine firingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        playerWidth = spriteRenderer.bounds.size.x;
        playerHeight = spriteRenderer.bounds.size.y;
        SetupMoveBoundries();
    }

    private void SetupMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + (playerWidth / 2);
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - (playerWidth / 2);

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + (playerHeight / 2);
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - (playerHeight / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PrepareWeapons();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);


        transform.position = new Vector2(newXPos, newYPos);
    }

    private void PrepareWeapons()
    {
        if (Input.GetKeyDown("space"))
        {
            firingCoroutine =  StartCoroutine(FireContinously());
        }

        if (Input.GetKeyUp("space"))
        {
            StopCoroutine(firingCoroutine);
        }
    }


    IEnumerator FireContinously()
    {
        while(true)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
