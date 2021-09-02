using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Coder of code: Keira

public class PanelMovement : MonoBehaviour
{

    public GameObject offScreenPosition;
    public GameObject onScreenPosition;

    //So I can hide the button
    public GameObject buttonOpenMenu;

    //Panel Transition
    bool slideOnScreen;
    bool slideOffScreen;


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
        if (slideOnScreen)
        {
            buttonOpenMenu.SetActive(false);

            //Transition effect here
            gameObject.transform.position = onScreenPosition.transform.position;

            //Transition effect end here

            if (gameObject.transform.position == onScreenPosition.transform.position) {
                slideOnScreen = false;
            }

        }

        if (slideOffScreen)
        {
            buttonOpenMenu.SetActive(true);

            //Transition effect here
            gameObject.transform.position = offScreenPosition.transform.position;
            
            //Transition effect end here

            if (gameObject.transform.position == offScreenPosition.transform.position)
            {
            slideOffScreen = false;
            }

        }

    }

    public void OnPressOpenMenu()
    {
        slideOnScreen = true;
    }

    public void OnPressCloseMenu()
    {
        slideOffScreen = true;
    }

}
