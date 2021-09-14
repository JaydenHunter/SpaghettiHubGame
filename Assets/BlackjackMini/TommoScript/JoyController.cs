using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class JoyController : MonoBehaviour
{
    //public Canvas can;
    public enum JoyDirection { UP, DOWN, LEFT, RIGHT, NONE };
    private JoyDirection joyDir = JoyDirection.NONE;
    public Image circleImg;
    public Image outerCircleImg;
    public bool touchStart = false;
    public float minRangeDetect;
    private Vector3 mousePosA;
    public bool hasSelection;
    public bool clickRelease;
    public event JoyDistance DragUp;
    public event JoyDistance DragDown;
    public event EventHandler DragLeft;
    public event EventHandler DragRight;

    public event JoyDistance DragUpRelease;
    public event JoyDistance DragDownRelease;
    public event EventHandler DragLeftRelease;
    public event EventHandler DragRightRelease;
    
    public float joyDistance;
    public delegate void JoyDistance(float f);
    private float mouseDragDistance;
    
    //public event EventHandler<DragUpEventArgs> DragUp;
    //public class DragUpEventArgs : EventArgs {
    //    int whatever;
    //}

    public Vector2 pointA;
    public Vector2 pointB;
    public int delayTimer;
    public float hitTimer;


    void Start()
    {
        //pointA = outerCircleImg.transform.position;
    }
    //void OnDrawGizmosSelected()
    //{


    //    UnityEditor.Handles.color = Color.green;
    //    //joy sweet spot lines
    //    UnityEditor.Handles.DrawWireDisc(mousePosA, Vector3.forward, minRangeDetect);

    //}
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //get vector direction from mouse to current mouse pos
            mousePosA = Input.mousePosition;
            hasSelection = false;
        }

        if (Input.GetMouseButton(0))
        {

            //distance min range to be activated
            //find vector direction
            Vector3 mouseDragPos = Input.mousePosition - mousePosA;
            joyDistance = Vector3.Distance(Input.mousePosition , mousePosA);
            //find current distance between
            //find float distance and compare it to min range
            clickRelease = false;
            if (!hasSelection)
            {

                if (mouseDragPos.y > minRangeDetect && Mathf.Abs(mouseDragPos.x) < minRangeDetect)
                {
                    joyDir = JoyDirection.UP;
                    joyController();
                    hasSelection = true;
                    FunctionTimer.Create(() =>ResetTimer(), 0.1f);
                }else
                //down
                if (mouseDragPos.y < -minRangeDetect && Mathf.Abs(mouseDragPos.x) < minRangeDetect)
                {
                    joyDir = JoyDirection.DOWN;
                    joyController();
                    hasSelection = true;
                    FunctionTimer.Create(() => ResetTimer(), 0.1f);
                }
                else
                //left
                if (Mathf.Abs(mouseDragPos.y) < minRangeDetect && mouseDragPos.x < -minRangeDetect)
                {

                    joyDir = JoyDirection.LEFT;
                    joyController();
                    hasSelection = true;
                    FunctionTimer.Create(() => ResetTimer(), 0.1f);
                }
                //right
                else if (Mathf.Abs(mouseDragPos.y) < minRangeDetect && mouseDragPos.x > minRangeDetect)
                {

                    joyDir = JoyDirection.RIGHT;
                    joyController();
                    hasSelection = true;
                    FunctionTimer.Create(() => ResetTimer(), 0.1f);
                }
                
                 else if (Mathf.Abs(mouseDragPos.y) < minRangeDetect && Mathf.Abs(mouseDragPos.x) < minRangeDetect)
                {
                    joyDir = JoyDirection.NONE;
                    hasSelection = false;
                }


            }

        }


        if (Input.GetMouseButtonUp(0))
        {
           
            clickRelease = true;
            joyController();
            //hasSelection = false;

            //mOVEoBJ();
        }


    }
    public void OnJoyUp(float breakForce)
    {
            
    }
    void joyController()
    {

        switch (joyDir)
        {
            case JoyDirection.UP:
                if (clickRelease==false)
                {
                    

                    Debug.Log("up");
                    DragUp?.Invoke(joyDistance);
                   // hasSelection = true;
                    //FunctionTimer.Create(() => ResetTimer(), hitTimer);
                }
                else
                {
                    Debug.Log("up release");
                     DragUpRelease?.Invoke(joyDistance);
                    clickRelease = false;

                }

                break;

            case JoyDirection.DOWN:
                if (!clickRelease)
                {
                   DragDown?.Invoke(joyDistance);
                    //hasSelection = true;
                    Debug.Log("down");
                    //FunctionTimer.Create(() => ResetTimer(), hitTimer);
                }
                else
                {
                    DragDownRelease?.Invoke(joyDistance);
                    clickRelease = false;

                }

                break;

            case JoyDirection.RIGHT:
                if (!clickRelease)
                {
                    DragRight?.Invoke(this, EventArgs.Empty);
                    //hasSelection = true;
                    // Debug.Log("right");
                    
                    Debug.Log("right");
                }
                else
                {
                    Debug.Log("up release");
                    DragRightRelease?.Invoke(this, EventArgs.Empty);
                    clickRelease = false;
                    
                }

                break;
            case JoyDirection.LEFT:
                if (!clickRelease)
                {
                     DragLeft?.Invoke(this, EventArgs.Empty);
                    //hasSelection = true;
                    Debug.Log("left");
                    //FunctionTimer.Create(() => ResetTimer(), hitTimer);
                }
                else
                {
                    DragLeftRelease?.Invoke(this, EventArgs.Empty);
                    clickRelease = false;

                }

                break;
            case JoyDirection.NONE:


                break;


        }
    }
    void ResetTimer()
    {
        hasSelection = false;
    }
}

