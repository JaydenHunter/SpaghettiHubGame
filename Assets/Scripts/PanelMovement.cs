using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Coder of code: Keira

public class PanelMovement : MonoBehaviour
{

    //gameobjects of the same size as the panel, that you can position in the editor.
    //The panel will go to the location of these empty objects.
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

        //If the panel is in the process of sliding onto the screen
        if (slideOnScreen)
        {
            buttonOpenMenu.SetActive(false);

            //Transition effect here
            gameObject.transform.position = onScreenPosition.transform.position;

            //Transition effect end here

            //When the panel is in the right position, stop transitioning
            if (gameObject.transform.position == onScreenPosition.transform.position) {
                slideOnScreen = false;
            }

        }

        //If the panel is in the process of sliding off of the screen
        if (slideOffScreen)
        {
            buttonOpenMenu.SetActive(true);

            //Transition effect here
            gameObject.transform.position = offScreenPosition.transform.position;
            
            //Transition effect end here

            //When the panel is in the right position, stop transitioning
            if (gameObject.transform.position == offScreenPosition.transform.position)
            {
            slideOffScreen = false;
            }

        }

    }

    /// <summary>
    /// When this function is called, it will tell the menu to slide onto the screen, or "open the menu"
    /// </summary>
    public void OnPressOpenMenu()
    {
        slideOnScreen = true;
    }
    

    /// <summary>
    /// When this function is called, it will tell the menu to slide off of the screen, or "close the menu"
    /// </summary>
    public void OnPressCloseMenu()
    {
        slideOffScreen = true;
    }
}
