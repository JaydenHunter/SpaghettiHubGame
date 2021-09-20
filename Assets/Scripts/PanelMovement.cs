using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Coder of code: Keira
//Aditional programmer Tomas Munro : just adding controller functionality for menu 

public class PanelMovement : MonoBehaviour
{
    //Toms additions
    JoyController controller;
    public List<Image> optionsImages;
    private bool select;
    public int optionIter;
   

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
        controller = GetComponent<JoyController>();
        controller.DragRight += SetUpController;
        controller.DragRight += OnPressOpenMenu;
        controller.DragLeft += OnPressCloseMenu;
        controller.DragLeft += UnsubscribeMain;
;

        //Set menu to be offscreen on game start
        gameObject.transform.position = offScreenPosition.transform.position;
    }
    /// <summary>
    /// toms addition Setting up ui controller for menu
    /// </summary>
    //minus all events
   
   
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
    public void OnPressOpenMenu(object sender, EventArgs e)
    {
        slideOnScreen = true;
    }
    

    /// <summary>
    /// When this function is called, it will tell the menu to slide off of the screen, or "close the menu"
    /// </summary>
    public void OnPressCloseMenu(object sender, EventArgs e)
    {
        slideOffScreen = true;
    }
    //*************************************************************************************
    //TOM'S ADDITIONS
    //*************************************************************************************

    /// <summary>
    /// subscribe joy events
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void SetUpController(object sender, EventArgs e)
    {
        if (!select)
        {
            controller.DragUp += NextOption;
            controller.DragDown += PreviousOption;
        }
        select = true;
        
        
        

    }
    /// <summary>
    /// unsubscribe joy events
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void UnsubscribeMain(object sender, EventArgs e)
    {
        if (select)
        {
            controller.DragUp -= NextOption;
            controller.DragDown -= PreviousOption;
        }
        select = true;

    }
    /// <summary>
    /// next option
    /// subscribe up down events/unsubscribe last button
    /// </summary>
    /// <param name="distance"></param>
    void NextOption(float distance)
    {
       
        //unsubscribe last event
       GetOptionSubscribe((Mathf.Abs(optionIter) % 5), false);
        optionIter--;

        //subscribe to new event
       GetOptionSubscribe((Mathf.Abs(optionIter) % 5), true);
        Debug.Log("iter " + (Mathf.Abs(optionIter) % 5));
        //optionIter = optionIter > optionsImages.Count ? 0 : optionIter++;
        //diplay option ui effect
        optionsImages[Mathf.Abs(optionIter) % 5].gameObject.GetComponent<BasicUIEffect>().uiEffect.ExecuteAllEffects();

    }
    /// <summary>
    /// previous option
    /// subscribe up down events/unsubscribe last button
    /// </summary>
    /// <param name="distance"></param>
    void PreviousOption(float distance)
    {
        GetOptionSubscribe((Mathf.Abs(optionIter) % 5), false);
        optionIter++;
        //subscribe
        //switch Statment for what item they have selected
       GetOptionSubscribe((Mathf.Abs(optionIter) % 5), true);
        Debug.Log("iter " + (Mathf.Abs(optionIter) % 5) );
        //optionIter = optionIter < 1 ? 0 : optionsImages.Count -1;
        //diplay option ui effect
        optionsImages[Mathf.Abs(optionIter) % 5].gameObject.GetComponent<BasicUIEffect>().uiEffect.ExecuteAllEffects();
    }
    /// <summary>
    /// subscribe up down events/unsubscribe last button
    /// </summary>
    /// <param name="option"></param>
    /// <param name="SubUnsub"></param>
    public void GetOptionSubscribe(int option, bool SubUnsub)
    {

        switch (option)
        {
            //close 
            case 0:
                if (SubUnsub)
                { 
                    controller.DragUpRelease += optionsImages[0].gameObject.GetComponent<ToggleItem>().ToggleActive;
                }
                else
                    controller.DragUpRelease -= optionsImages[0].gameObject.GetComponent<ToggleItem>().ToggleActive;
                break;
            case 1:

                if (SubUnsub)
                {
                    controller.DragUpRelease += optionsImages[1].gameObject.GetComponent<ToggleItem>().ToggleActive;
                }
                else
                    controller.DragUpRelease -= optionsImages[1].gameObject.GetComponent<ToggleItem>().ToggleActive;
                break;
            case 2:
                if (SubUnsub)
                { 
                    controller.DragUpRelease += optionsImages[2].gameObject.GetComponent<SceneLoader>().LoadBlackJackGame;
                }
                else
                    controller.DragUpRelease -= optionsImages[2].gameObject.GetComponent<SceneLoader>().LoadBlackJackGame;

                break;
            case 3:
                if (SubUnsub)
                { 
                    controller.DragUpRelease += optionsImages[3].gameObject.GetComponent<SceneLoader>().LoadMainMenu;
                }
                else
                    controller.DragUpRelease -= optionsImages[3].gameObject.GetComponent<SceneLoader>().LoadMainMenu;
                break;
            case 4:
                if (SubUnsub)
                { 
                    controller.DragUpRelease += optionsImages[4].gameObject.GetComponent<MainMenuScript>().QuitGame;
                }
                else
                    controller.DragUpRelease -= optionsImages[4].gameObject.GetComponent<MainMenuScript>().QuitGame;
                break;

        }
    }
}
