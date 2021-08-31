using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;
    public DeckHand deckHand;
    private int standClicks = 0;

    // access the player and dealers hand
    public CardPlayer playerScript;
    public CardPlayer dealerScript;

    //access the player and the dealer's script
    public TMP_Text scoreText;
    public TMP_Text dealerScoreText;
    public TMP_Text betsText;
    public TMP_Text cashText;
    public TMP_Text mainText;
    //public TMP_Text standBtnText;
    bool canPlaceBets;
    // Card hiding dealers 2nd card
    public GameObject hideCard;
    //how much is in pot 
    int pot = 0;



    // Start is called before the first frame update
    void Start()
    {
        //adding OnClick Listeners to the buttons
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        betBtn.onClick.AddListener(() => BetClicked());


    }

    private void DealClicked()
    {
        canPlaceBets = true;
        //reset round, hide text, prep for new hand
        playerScript.ResetHand();
        dealerScript.ResetHand();
        // HIde deal hand score at start of deal
        dealerScoreText.gameObject.SetActive(false);
        mainText.gameObject.SetActive(false);
        //**************************multiple? V
        dealerScoreText.gameObject.SetActive(false);

        deckHand.Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
        //update the score displayed
        scoreText.text = "Hand: " + playerScript.handValue.ToString();
        dealerScoreText.text = "Hand: " + dealerScript.handValue.ToString();
        // enable to hide one of the dealer's cards
        hideCard.GetComponent<Image>().enabled = true;

        //adjust buttons visability
        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        standBtn.GetComponentInChildren<TMP_Text>().text = "Stand";
        // Set standard pot size
        pot = 40;
        betsText.text = "Bets: $" + (pot/2).ToString();
        playerScript.GetAdjustMoney -= 20;
        cashText.text = playerScript.GetAdjustMoney.ToString();
    }


    private void HitClicked()
    {
        canPlaceBets = false;
        Debug.Log("reg");
        //check that there is still room on the table 
        if (playerScript.cardIndex <= 10) 
        {
           playerScript.GetCard();
            scoreText.text = "Hand: " + playerScript.handValue.ToString();
            if (playerScript.handValue > 20) RoundOver();
        }

    }


    private void StandClicked()
    {
        canPlaceBets = false;
        standClicks++;
        if (standClicks > 1) RoundOver();
        {
            HitDealer();
            standBtn.GetComponentInChildren<TMP_Text>().text =  "Call";
        }
    }

    private void HitDealer()
    {
        canPlaceBets = false;
        while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            dealerScoreText.text =  "Hand: " + dealerScript.handValue.ToString();
            if (dealerScript.handValue > 20) RoundOver();

        }
    }
    //check for winner 
    void RoundOver()
    {
        // Booleans (true/false) for bust and blackjack/21
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;
        // If stand has been clicked less than twice, no 21s or busts, quit function 
        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;
        bool roundOver = true;
        // all bust , bets returned
        if (playerBust && dealerBust)
        {
            mainText.text = "All Bust: bets returned";
            playerScript.GetAdjustMoney = (pot / 2);
            

        }
        // if player busts, dealer didnt, or if dealer has more points, deal wins 
        else if (playerBust || (!dealerBust &&  dealerScript.handValue > playerScript.handValue ))
        {
            mainText.text = "Dealer Wins!";
            //playerScript.GetAdjustMoney -= pot;
        }
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {

            mainText.text = "You win!";
            playerScript.GetAdjustMoney += pot;
        }
        else if (playerScript.handValue == dealerScript.handValue)
        {

            mainText.text = "Push: Bets returned";
            playerScript.GetAdjustMoney += (pot / 2);
        }
        else
        {
            roundOver = false;
        }
        // Set ui for next move / hand / turn
        if (roundOver)
        {
            hitBtn.gameObject.SetActive(false);
            standBtn.gameObject.SetActive(false);
            dealBtn.gameObject.SetActive(true);
            mainText.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);
            hideCard.GetComponent<Image>().enabled = false;
            cashText.text = "$" + playerScript.GetAdjustMoney.ToString();
            standClicks = 0;
        }
    }

    //add money to pot if bet clicked
    void BetClicked()
    {
        if (canPlaceBets)
        {
        pot += 40;
        betsText.text = " Bets: $" + pot.ToString();
        playerScript.GetAdjustMoney -= 20;
        cashText.text = "$" +playerScript.GetAdjustMoney.ToString();
        betsText.text = " Bets: $" + (pot / 2).ToString();
        }
    }
   
}
