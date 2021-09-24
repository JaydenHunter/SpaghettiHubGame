///Tomas Munro's Script
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
    [Header("Health Settings")]
    public int LeftRange;
    public int RightRange;
    public int UpRange;
    public int DownRange;
    public float minRangeDetect;
    private Vector3 mousePosA;
    public bool hasSelection;
    public bool clickRelease;
    public bool disengageButton;
    public event JoyDistance DragUp;
    public event JoyDistance DragDown;
    public event JoyDown DragLeft;
    public event EventHandler DragRight;

    public event JoyDistance DragUpRelease;
    public event JoyDistance DragDownRelease;
    public event EventHandler DragLeftRelease;
    public event EventHandler DragRightRelease;
    public event JoyDown HoldMouse;


    public float joyDistance;
    public delegate void JoyDistance(float f);
    public delegate void JoyDown(bool down);
    private float mouseDragDistance;
    public bool mousetouchFunctional;
    public Vector2 pointA;
    public Vector2 pointB;
    //public int delayTimer;
    public float hitTimer;



    void Start()
    {
    }
  
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //get vector direction from mouse to current mouse pos
            mousePosA = Input.mousePosition;
            hasSelection = false;
            if (mousetouchFunctional)
            {

                HoldMouse?.Invoke(true);
            }
        }

        if (Input.GetMouseButton(0))
        {

            //find vector direction
            Vector3 mouseDragPos = Input.mousePosition - mousePosA;
            //find current distance between
            joyDistance = Vector3.Distance(Input.mousePosition , mousePosA);
            //find float distance and compare it to min range
            clickRelease = false;


            if (!hasSelection)
            {

                if (mouseDragPos.y > UpRange && Mathf.Abs(mouseDragPos.x) < minRangeDetect)
                {
                    joyDir = JoyDirection.UP;
                    joyController();
                    hasSelection = true;
                    FunctionTimer.Create(() =>ResetTimer(), hitTimer);
                }else
                //down
                if (mouseDragPos.y < -DownRange && Mathf.Abs(mouseDragPos.x) < minRangeDetect)
                {
                    joyDir = JoyDirection.DOWN;
                    joyController();
                    hasSelection = true;
                    FunctionTimer.Create(() => ResetTimer(), hitTimer);
                }
                else
                //left
                if (Mathf.Abs(mouseDragPos.y) < LeftRange && mouseDragPos.x < -LeftRange)
                {

                    joyDir = JoyDirection.LEFT;
                    joyController();
                    hasSelection = true;
                    FunctionTimer.Create(() => ResetTimer(), hitTimer);
                }
                //right
                else if (Mathf.Abs(mouseDragPos.y) < RightRange && mouseDragPos.x > RightRange)
                {
                    joyDir = JoyDirection.RIGHT;
                    joyController();
                    hasSelection = true;
                    FunctionTimer.Create(() => ResetTimer(), hitTimer);
                }
                
                 else if ( disengageButton == false)
                {
                    joyDir = JoyDirection.NONE;
                    hasSelection = false;
                }


            }
           

        }

        //mouse button left click
        if (Input.GetMouseButtonUp(0))
        {
            clickRelease = true;
            joyController();
            HoldMouse?.Invoke(false);
        }
    }
    /// <summary>
    /// joy controller state machine
    /// </summary>
    void joyController()
    {

        switch (joyDir)
        {
            //Joy up
            case JoyDirection.UP:
                if (clickRelease==false)
                {
                    Debug.Log("up");
                    DragUp?.Invoke(joyDistance);
                 
                }
                else
                {
                    Debug.Log("up release");
                     DragUpRelease?.Invoke(joyDistance);
                    clickRelease = false;

                }

                break;
                //Joy Down
            case JoyDirection.DOWN:
                if (!clickRelease)
                {
                   DragDown?.Invoke(joyDistance);
                   
                    Debug.Log("down");
                  
                }
                else
                {
                    DragDownRelease?.Invoke(joyDistance);
                    clickRelease = false;

                }

                break;
            //Joy Right
            case JoyDirection.RIGHT:
                if (!clickRelease)
                {
                    DragRight?.Invoke(this, EventArgs.Empty);
                    
                    Debug.Log("right");
                }
                else
                {
                    Debug.Log("up release");
                    DragRightRelease?.Invoke(this, EventArgs.Empty);
                    clickRelease = false;
                    
                }

                break;
            //Joy Left
            case JoyDirection.LEFT:
                if (!clickRelease)
                {
                     DragLeft?.Invoke(true);
                    Debug.Log("left");
                   
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

