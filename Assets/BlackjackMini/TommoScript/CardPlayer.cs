using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlayer : MonoBehaviour
{
    public GameManager gameManager;
    //-- this script is for both for player and dealer
    // Start is called before the first frame update
    //Get other scripts
    public CardScript cardScript;
    public DeckHand deckScript;
    // total value of player/dealers hand
    public int handValue = 0;

    // Betting money
    private int money;

    //Array of card objects on table 
    public GameObject[] hand;
    //index of next card to be turned over
    public int cardIndex = 0;
    //tracking aces for 1 to 11 conversions
    List<CardScript> aceList = new List<CardScript>();
    private void Start()
    {
        money = gameManager.money;
    }
    public void StartHand()
    {
       
        GetCard();
        GetCard();
    }
    //add a hand to the player/dealer's handw
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
        AceCheck();
        cardIndex++;
        return handValue;
    }

    public void AceCheck()
    {
        //for each ace in the listr check
        foreach (CardScript ace in aceList)
        {
            if (handValue +10 < 22 && ace.GetValueOfCard == 1)
            {
                //if converting, adjust card object value and hand
                ace.GetValueOfCard = 11;
                handValue += 10;
            }
            else if (handValue > 21 && ace.GetValueOfCard == 11)
            {
                //if converting, adjust gameobject value and hand value
                ace.GetValueOfCard = 1;
                handValue -= 10;
            }
        }
        //aceList.Add(hand[cardIndex].GetComponent<CardScript>());
    }
    //Get money
    public int GetAdjustMoney { get => gameManager.money; set => gameManager.money = value; }
    public void ResetHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
        hand[i].GetComponent<CardScript>().ResetCard();
        hand[i].GetComponent<Image>().enabled = false;

        }
        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript>();
    }
    
  
}
