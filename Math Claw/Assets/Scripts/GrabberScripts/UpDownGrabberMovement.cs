using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownGrabberMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 2f;
    private Vector2 movement;
    private float directionY = 0f; // 0 = на месте, -1 = вниз, 1 = вверх
    bool isGrabbing = false;
    public float bottomBorder = -1.6f;
    public float topBorder = 3.5f;

    private void OnEnable()
    {
        StartGame.OnGrabPressed += OnGrabPressed;
    }

    private void OnDisable()
    {
        StartGame.OnGrabPressed -= OnGrabPressed;
    }

    private void Start()
    {
        OnGrabPressed();    

    }

    void Update()
    {
        //Debug.Log("1 = " + isGrabbing);
        //Debug.Log("2 = " + Input.GetKeyDown(KeyCode.DownArrow));
        //if (Input.GetKeyDown(KeyCode.DownArrow) && !isGrabbing)
        //{
        //    Debug.Log("truetrue134");
        //    OnGrabPressed();
        //}
        movement.y = directionY;
        if(directionY == -1f)
        {
            if (gameObject.transform.position.x < bottomBorder)
                directionY = 1f;
        }
        else if(directionY == 1f)
        {
            if (gameObject.transform.position.x > topBorder)
            {
                directionY = 0f;
                isGrabbing = false;
            }
        }

    }

    private void FixedUpdate()
    {
        if (isGrabbing)
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    void OnGrabPressed()
    {
        isGrabbing = true;
        directionY = -1f;
    }

}