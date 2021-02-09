using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArmController : MonoBehaviour
{
    public Transform Controller;
    public Quaternion leftTilt;
    public Quaternion rightTilt;
    public Quaternion initialTilt;

    float leftEdgeMouseInput;
    float rightEdgeMouseInput;

    //private void OnEnable()
    //{
    //    MovementGrabber.MovementXUpdate += MovementXUpdate;
    //}

    //private void OnDisable()
    //{
    //    MovementGrabber.MovementXUpdate -= MovementXUpdate;
    //}

    private void Start()
    {
        //Debug.Log(Controller.position);
        //Debug.Log(Controller.localPosition);
        rightEdgeMouseInput = Controller.transform.localPosition.x + 15;
        leftEdgeMouseInput = Controller.transform.localPosition.x - 15;
        //rightEdgeMouseInput = Controller.transform.localPosition.x + 15;
        //leftEdgeMouseInput = Controller.transform.localPosition.x - 15;
        //Debug.Log(Camera.current.transform.right.x);
        //Debug.Log(leftEdgeMouseInput);
        //Debug.Log(rightEdgeMouseInput);
        //initialTilt = Controller.rotation;
        // tiltAngleReversed = -tiltAngle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnMouseDrag()
    {
        if (Input.mousePosition.x < leftEdgeMouseInput)
        {
            Controller.transform.rotation = leftTilt;
        }
        else if (Input.mousePosition.x > rightEdgeMouseInput)
        {
            Controller.transform.rotation = rightTilt;
        }
        else
        {
            Controller.transform.rotation = initialTilt;
        }
    }

    //public void EndDrag()
    //{
    //    Debug.Log(3333);
    //    Controller.transform.rotation = initialTilt;
    //}

    //void MovementXUpdate()
    //{
    //    var z = Input.GetAxisRaw("Horizontal") == -1 ? tiltAngle : tiltAngleReversed;
    //    Controller.rotation.Set(0, 0, z, 0);
    //}
}
