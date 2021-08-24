using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;
    public DeckHand deckHand;

    // access the player and dealers hand
    public CardPlayer playerScript;
    public CardPlayer dealerScript;
    // Start is called before the first frame update
    void Awake()
    {
        //adding OnClick Listeners to the buttons
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());


        
    }

    private void DealClicked()
    {
        deckHand.Shuffle();
        //deckScript.Shuffle();
        Debug.Log("reg");
        playerScript.StartHand();
        dealerScript.StartHand();

    }


    private void HitClicked()
    {
        Debug.Log("reg");
        throw new NotImplementedException();
    }


    private void StandClicked()
    {
        Debug.Log("reg");
        throw new NotImplementedException();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
