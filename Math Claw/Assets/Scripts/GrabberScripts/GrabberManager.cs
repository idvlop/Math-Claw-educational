using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberManager : MonoBehaviour
{
    public GameObject GrabberRoot;
    //public GameObject LeftHands;
    //public GameObject RightHands;
    //public Transform BottomEdge;

    //public Collider2D ballCol1;
    //public Collider2D ballCol2;

    public delegate void CheckGrabber();

    public static event CheckGrabber FoundBall;
    //public static event CheckGrabber NotFoundBall;
    //public static event CheckGrabber GrabIsOvered;

    //private readonly float openedAngle = 30;
    //private readonly float closedAngle = 15;

   
    //public bool IsGrabberOpened { get; private set; }



    void Start()
    {
        //rigidbodyGrabber = GrabberRoot.GetComponent<Rigidbody2D>();

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BallTrigger") && MovementGrabber.isGrabbing)
        {
            //CloseHands();
            //UpDownGrabberMovement.direction = 1f;
            //collision.tag = "GrabbedBall";
            collision.gameObject.transform.parent = gameObject.transform;
            //collision.attachedRigidbody.bodyType = RigidbodyType2D.Kinematic;
            collision.attachedRigidbody.simulated = false;
            //if(!GameManager.HaveTaskOnSolve)
            FoundBall();
        }
        //else if (collision.gameObject.CompareTag("WallTrigger") && StartGame.IsGrubbing)
        //{
        //    //UpDownGrabberMovement.direction = 1f;
        //}
    }

    //void OnGrabPressed()
    //{
    //    OpenHands();
    //    //UpDownGrabberMovement.direction = -1f;
    //}

    //void OpenHands()
    //{
    //    if (!IsGrabberOpened)
    //        for (var i = closedAngle; i <= openedAngle; i++)
    //        {
    //            LeftHands.transform.Rotate(0, 0, -1);
    //            RightHands.transform.Rotate(0, 0, 1);
    //        }
    //    IsGrabberOpened = true;
    //}

    //void CloseHands()
    //{
    //    if (IsGrabberOpened)
    //        for (var i = closedAngle; i <= openedAngle; i++)
    //        {
    //            LeftHands.transform.Rotate(0, 0, 1);
    //            RightHands.transform.Rotate(0, 0, -1);
    //        }
    //    IsGrabberOpened = false;
    //}
}
