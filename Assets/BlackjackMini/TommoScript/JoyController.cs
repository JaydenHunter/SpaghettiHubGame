using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;

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
    public event EventHandler DragUp;
    public event EventHandler DragDown;
    public event EventHandler DragLeft;
    public event EventHandler DragRight;

    public event EventHandler DragUpRelease;
    public event EventHandler DragDownRelease;
    public event EventHandler DragLeftRelease;
    public event EventHandler DragRightRelease;
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
    void OnDrawGizmosSelected()
    {


        UnityEditor.Handles.color = Color.green;
        //joy sweet spot lines
        UnityEditor.Handles.DrawWireDisc(mousePosA, Vector3.forward, minRangeDetect);

    }
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
            //find current distance between
            //find float distance and compare it to min range
            clickRelease = false;
            if (!hasSelection)
            {

                if (mouseDragPos.y > minRangeDetect && Mathf.Abs(mouseDragPos.x) < minRangeDetect)
                {

                    joyDir = JoyDirection.UP;
                    joyController();

                    // FunctionTimer.Create(() => MyFunction, 4;
                }
                //down
                if (mouseDragPos.y < -minRangeDetect && Mathf.Abs(mouseDragPos.x) < minRangeDetect)
                {

                    joyDir = JoyDirection.DOWN;
                    joyController();
                }
                //left
                if (Mathf.Abs(mouseDragPos.y) < minRangeDetect && mouseDragPos.x < -minRangeDetect)
                {

                    joyDir = JoyDirection.LEFT;
                    joyController();
                }
                //right
                if (Mathf.Abs(mouseDragPos.y) < minRangeDetect && mouseDragPos.x > minRangeDetect)
                {

                    joyDir = JoyDirection.RIGHT;
                    joyController();
                }
                 if (Mathf.Abs(mouseDragPos.y) < minRangeDetect && Mathf.Abs(mouseDragPos.x) < minRangeDetect)
                {
                    joyDir = JoyDirection.NONE;
                    hasSelection = false;
                }
                

            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            mousePosA = Vector3.zero;
            clickRelease = true;
            joyController();
            //hasSelection = false;

            //mOVEoBJ();
        }


    }
    void joyController()
    {
        switch (joyDir)
        {
            case JoyDirection.UP:
                if (!clickRelease)
                {
                    Debug.Log("up");
                    FunctionTimer.Create(() => DragUp?.Invoke(this, EventArgs.Empty), delayTimer);
                    hasSelection = true;
                    FunctionTimer.Create(() => ResetTimer(), hitTimer);
                }
                else
                {
                    Debug.Log("up release");
                     DragUpRelease?.Invoke(this, EventArgs.Empty);
                    clickRelease = false;

                }

                break;

            case JoyDirection.DOWN:
                if (!clickRelease)
                {
                    FunctionTimer.Create(() => DragDown?.Invoke(this, EventArgs.Empty), delayTimer);
                    hasSelection = true;
                    Debug.Log("down");
                    FunctionTimer.Create(() => ResetTimer(), hitTimer);
                }
                else
                {
                    FunctionTimer.Create(() => DragDownRelease?.Invoke(this, EventArgs.Empty), delayTimer);
                    clickRelease = false;

                }

                break;

            case JoyDirection.RIGHT:
                if (!clickRelease)
                {
                    FunctionTimer.Create(() => DragRight?.Invoke(this, EventArgs.Empty), delayTimer);
                    hasSelection = true;
                    // Debug.Log("right");
                    FunctionTimer.Create(() => ResetTimer(), hitTimer);
                    Debug.Log("right");
                }
                else
                {
                    Debug.Log("up release");
                    FunctionTimer.Create(() => DragRightRelease?.Invoke(this, EventArgs.Empty), delayTimer);
                    clickRelease = false;
                    
                }

                break;
            case JoyDirection.LEFT:
                if (!clickRelease)
                {
                    FunctionTimer.Create(() => DragLeft?.Invoke(this, EventArgs.Empty), delayTimer);
                    hasSelection = true;
                    Debug.Log("left");
                    FunctionTimer.Create(() => ResetTimer(), hitTimer);
                }
                else
                {
                    FunctionTimer.Create(() => DragLeftRelease?.Invoke(this, EventArgs.Empty), delayTimer);
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

