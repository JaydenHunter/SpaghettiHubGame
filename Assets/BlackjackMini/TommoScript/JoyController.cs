using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;

public class JoyController : MonoBehaviour
{
    //public Canvas can;
    public Image circleImg;
    public Image outerCircleImg;
    public bool touchStart = false;
    public float minRangeDetect;
    private Vector3 mousePosA;

    //public float speed;
    //public Vector2 deltaAxis = Vector2.zero;
    public Vector2 pointA;
    public Vector2 pointB;


    //public Transform B;
    public event EventHandler UpdateTrailEvent;

    //Vector2 StartPoint;
    //public bool isHeld = false;

    void Start()
    {
        //pointA = outerCircleImg.transform.position;
    }
    void OnDrawGizmosSelected()
    {


        UnityEditor.Handles.color = Color.green;
        //joy sweet spot lines
        UnityEditor.Handles.DrawWireDisc(mousePosA, Vector3.forward, minRangeDetect);
        //draw where mouse is
        //Vector3 mousePosition = Event.current.mousePosition;
        //mousePosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePosition.y;
        //mousePosition = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(mousePosition);
        //mousePosition.y = -mousePosition.y;

        //UnityEditor.Handles.DrawLine(mousePosA, MouseCursor.);

    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            //get vector direction from mouse to current mouse pos
            mousePosA = Input.mousePosition;


            //pointA = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //outerCircleImg
            //transform.position = Input.mousePosition;

        }


        if (Input.GetMouseButton(0))
        {

            //distance min range to be activated
            //find vector direction
            Vector3 mouseDragPos = Input.mousePosition - mousePosA;
            //find current distance between
            //find float distance and compare it to min range

            //Debug.Log("x =" + mouseDragPos.x + ", y =" + mouseDragPos.y);
            //up
            if (mouseDragPos.y > minRangeDetect && Mathf.Abs(mouseDragPos.x) < minRangeDetect)
            {
                Debug.Log("up");
            }
            //down
            if (mouseDragPos.y < -minRangeDetect && Mathf.Abs(mouseDragPos.x) < minRangeDetect)
            {
                Debug.Log("down");
            }
            //left
            if (Mathf.Abs(mouseDragPos.y) < minRangeDetect && mouseDragPos.x < -minRangeDetect)
            {
                Debug.Log("left");
            }
            //right
            if (Mathf.Abs(mouseDragPos.y) < minRangeDetect && mouseDragPos.x > minRangeDetect)
            {
                Debug.Log("right");
            }

            //mouseDragPos = mousePosA - ;

            //get mouse pos when clicked and calulate the direction 
            //use that direction to 

            //if (isHeld == true)
            //{

            //	UpdateTrailEvent?.Invoke(this, EventArgs.Empty);




            //	//get mouse pos 
            //	// Get Mouse Pos

            //	Vector2 mousePos = Input.mousePosition;
            //	//mousePos.x -= Screen.width / 2;
            //	//mousePos.y -= Screen.height / 2;
            //	//Vector2 CurrentPoint = GetCanvasMousePos();
            //	// Clamp Inner Circle to Outer Circle

            //	float maxDst = outerCircleImg.rectTransform.rect.width * 0.5f;
            //	float x = Mathf.Clamp(mousePos.x, StartPoint.x - maxDst, StartPoint.x + maxDst);
            //	float y = Mathf.Clamp(mousePos.y, StartPoint.y - maxDst, StartPoint.y + maxDst);
            //	Vector2 ClampedCurrent = new Vector2(x, y);
            //	// Calculate delta
            //	Vector2 d = (ClampedCurrent - StartPoint);

            //	deltaAxis = d / maxDst;

            //	Debug.Log("D: " + d + ", norm: " + deltaAxis);
            //	// Set Inner Circle Position

            //	circleImg.transform.position = new Vector3(x, y, circleImg.transform.position.z);

            //}
        }
        if (Input.GetMouseButtonUp(0))
        {
            mousePosA = Vector3.zero;
            //mOVEoBJ();
        }

        //private Vector2 GetCanvasMousePos()
        //{
        //	Vector2 movePos;
        //	RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //		can.transform as RectTransform, Input.mousePosition, can.worldCamera, out movePos);
        //	movePos = can.transform.TransformPoint(movePos);
        //	return movePos;
        //}
        //public void mOVEoBJ()
        //{

        //	B.Translate(new Vector3(deltaAxis.x, 0f, deltaAxis.y) * speed * Time.deltaTime);

        //}
        //public void click()
        //{
        //	isHeld = true;

        //}
        //public void clickRelease()
        //{
        //	isHeld = false;
        //	deltaAxis = Vector2.zero;
        //	//release button
        //	circleImg.transform.position = outerCircleImg.transform.position;
        //	isHeld = false;

        //}


    }
}

