using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlayer : MonoBehaviour
{
    //-- this script is for both for player and dealer
    // Start is called before the first frame update
    //Get other scripts
    public CardScript cardScript;
    public DeckHand deckScript;
    // total value of player/dealers hand
    public int handValue = 0;

    // Betting money
    private int money = 1000;

    //Array of card objects on table 
    public GameObject[] hand;
    //index of next card to be turned over
    public int cardIndex = 0;
    //tracking aces for 1 to 11 conversions
    List<CardScript> aceList = new List<CardScript>();

   public void StartHand()
    {
        GetCard();
        GetCard();
    }
    //add a hand to the player/dealer's hand
    public int GetCard()
    {
        // Get a card, use deal card to assign sprite and value to card on table
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        //show card on game screen
        hand[cardIndex].GetComponent<Image>().enabled = true;
       //hand[cardIndex].GetComponent<Renderer>().enabled = true;
        // Add card value to running total of the hand 
        handValue += cardValue;
        //if value is 1, it is an ace
        if (cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());

        }
        //check if we should use an 11 instead of a 1
        //ace check()
        cardIndex++;
        return handValue;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
