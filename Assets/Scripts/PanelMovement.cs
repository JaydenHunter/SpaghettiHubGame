using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using tmpro;


public class PanelMovement : MonoBehaviour
{

    public GameObject offScreenPosition;
    public GameObject onScreenPosition;

    //public TMP_Button openMenuButton;
    //public TMP_Button closeMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        //Set menu to be offscreen on game start
        gameObject.transform.position = offScreenPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
