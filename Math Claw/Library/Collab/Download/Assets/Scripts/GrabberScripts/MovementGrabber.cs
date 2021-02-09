using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGrabber : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform manager;
    private Transform currentBall;
    public float moveSpeed = 2f;
    Vector2 movement;
    public static bool isGrabbing = false;

    public float rightBorder = 2.55f;
    public float leftBorder = -2.75f;

    private static float directionY = 0f; // 0 = на месте, -1 = вниз, 1 = вверх
    public float bottomBorder = -1.6f;
    public float topBorder = 3.5f;

    public delegate void GrabberCheck();
    public static event GrabberCheck PickedUpBall;
    private static bool isBallInHands = false;

    public Transform Controller;
    //public static event GrabberCheck MovementXUpdate;

    private void OnEnable()
    {
        StartGame.OnGrabPressed += OnGrabPressed;
        GrabberManager.FoundBall += OnFoundBall;
    }

    private void OnDisable()
    {
        StartGame.OnGrabPressed -= OnGrabPressed;
        GrabberManager.FoundBall -= OnFoundBall;
    }

    void OnGrabPressed()
    {
        if (!isBallInHands)
        {
            directionY = -1f;
            isGrabbing = true;
        }
        else
        {
            currentBall = manager.GetChild(0);
            var ballRB = currentBall.GetComponent<Rigidbody2D>();
            ballRB.simulated = true;
            currentBall.localScale = new Vector3(0.82f, 0.82f, 0);
            //ballRB.AddForce(Vector2.down);
            manager.DetachChildren();
            isBallInHands = false;
        }
    }

    void OnFoundBall()
    {
        directionY = 1f;
        isBallInHands = true;
    }

    void Update() {

        movement.x = 0;
        movement.y = directionY;

        if (gameObject.transform.position.x < rightBorder && !isGrabbing && (Input.GetAxisRaw("Horizontal") > 0 || Controller.rotation.z < 0))
            movement.x = 1f;
        if (gameObject.transform.position.x > leftBorder && !isGrabbing && (Input.GetAxisRaw("Horizontal") < 0 || Controller.rotation.z > 0))
            movement.x = -1f;

        if (directionY == -1f)
        {
            if (gameObject.transform.position.y < bottomBorder)
                directionY = 1f;
        }
        else if (directionY == 1f)
        {
            if (gameObject.transform.position.y > topBorder)
            {
                directionY = 0f;
                isGrabbing = false;
                if (isBallInHands)
                {
                    //isBallInHands = false; //Убрать после написания кода для бросания
                    PickedUpBall();
                }
            }
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
}
