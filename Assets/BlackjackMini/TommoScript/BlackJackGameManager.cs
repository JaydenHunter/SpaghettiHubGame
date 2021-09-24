///Tomas Munro's Script  
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Tween_Library.Scripts;
using Tween_Library.Scripts.Effects;
using Random = UnityEngine.Random;

public class BlackJackGameManager : MonoBehaviour
{
    //private EffectBuilder
    public List<AudioClip> audioClips;
    private AudioSource audioSource;
    public List<float> audioVolume;
    public Image dealBtn;
    public Image hitBtn;
    public Image standBtn;
    public Image betBtn;
    public Image returnBtn;
    public DeckHand deckHand;
    private int standClicks = 0;
    private bool _bCanBet = true;
    JoyController controller;
    private SceneLoader sceneLoader;

    // access the player and dealers hand
    public CardPlayer playerScript;
    public CardPlayer dealerScript;
    

    //access the player and the dealer's script
    public TMP_Text scoreText;
    public TMP_Text dealerScoreText;
    public TMP_Text betsText;
    public TMP_Text cashText;
    public TMP_Text mainText;
    public TMP_Text instructions;
     
    // Card hiding dealers 2nd card
    public GameObject hideCard;

    //how much is in pot 
    int pot = 0;
    public enum _eBlackJackStates
    {
        BETTING, DEALING, MIDGAME, END
    }
    _eBlackJackStates _currentState = _eBlackJackStates.BETTING;
    public float min;
    public float max;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();
        audioSource = GetComponent<AudioSource>();
        //adding OnClick Listeners to the buttons
        cashText.text = "Bank : $" + playerScript.GetAdjustMoney.ToString();
        controller = GetComponent<JoyController>();

        BlackJackStateManager();

    


    }

    /// <summary>
    /// Controls current state of game and dictates what events need to be subscribed and unsubscribe to the joy controller
    /// also turns all the ui ect that needs to be active ect
    /// </summary>
    void BlackJackStateManager()
    {
        switch (_currentState)
        {
            //PLACE BETS phase
            case _eBlackJackStates.BETTING:
                //disable all othe
                controller.DragUp += BetClicked;
                controller.DragDown += BetMinus;

                controller.DragLeftRelease += ReturnToMain;
                controller.DragLeft += ReturnToMainSelected;
                

                //controller.DragDown += BetMinus;
                betsText.text = "$0 : Bet";
                pot = 0;
                cashText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-306, -20f);
                //betBtn.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(47f, -195);
                betsText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-106.3f, -228.4f);
                playerScript.ResetHand();
                dealerScript.ResetHand();
                //betBtn.gameObject.SetActive(true);
                returnBtn.gameObject.SetActive(false);
                betsText.gameObject.SetActive(true);
                dealBtn.gameObject.SetActive(false);
                hitBtn.gameObject.SetActive(false);
                standBtn.gameObject.SetActive(false);
                scoreText.gameObject.SetActive(false);
                dealerScoreText.gameObject.SetActive(false);
                cashText.gameObject.SetActive(true);
                mainText.gameObject.SetActive(false);
                instructions.text = "BetDrag up/down : +Bet/-Bet" + "\n" + "Drag right &release : Deal  ";


                //Dealing phase
                break;
            case _eBlackJackStates.DEALING:
                //Debug.Log("pooop");
                controller.DragLeftRelease -= ReturnToMain;
                controller.DragLeft -= ReturnToMainSelected;
                controller.DragUp -= BetClicked;
                controller.DragDown -= BetMinus;
                controller.DragRightRelease -= DealClicked;
                controller.DragRight -= DealSelected;
                instructions.text = "DragLeft Release : HIT"+ "\n"+ " DragRight Release : STAND";
                controller.DragLeftRelease += HitClicked;
                controller.DragRightRelease += StandClicked;
                controller.DragLeft += HitSelected;
                controller.DragRight += StandSelected;
                hitBtn.gameObject.SetActive(false);
                standBtn.gameObject.SetActive(false);
                cashText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-269f, 128f);
                betsText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-571f, -168f);
                scoreText.gameObject.SetActive(true);

                dealerScoreText.gameObject.SetActive(true);
                cashText.gameObject.SetActive(true);
                mainText.gameObject.SetActive(true);

                break;
                //unused
            case _eBlackJackStates.MIDGAME:

                break;
                // end/results state 
            case _eBlackJackStates.END:
               
                controller.DragLeftRelease -= HitClicked;
                controller.DragRightRelease -= StandClicked;
                controller.DragLeft -= HitSelected;
                controller.DragRight -= StandSelected;
                mainText.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                mainText.gameObject.GetComponent<BasicUIEffect>().uiEffect.ExecuteAllEffects();

                //betBtn.gameObject.SetActive(false);
                betsText.gameObject.SetActive(true);
                dealBtn.gameObject.SetActive(false);
                hitBtn.gameObject.SetActive(false);
                standBtn.gameObject.SetActive(false);
                scoreText.gameObject.SetActive(true);
                dealerScoreText.gameObject.SetActive(true);
                cashText.gameObject.SetActive(true);
                mainText.gameObject.SetActive(true);
               

                break;


        }
    }
    /// <summary>
    /// player is hovering over stand 
    /// end player turn, dealers turn
    /// </summary>
    /// <param name="sender"> your event params</param>
    /// <param name="e"></param>
    void StandSelected(object sender, EventArgs e)
    {
        audioSource.pitch = Random.Range(min, max);
        audioSource.PlayOneShot(audioClips[0], audioVolume[0]);
        standBtn.gameObject.SetActive(true);
        hitBtn.gameObject.SetActive(false);
        //uiEffectedImage = standBtn.GetComponent<Image>();
        standBtn.GetComponent<BasicUIEffect>().uiEffect.ExecuteAllEffects();
    }
    /// <summary>
    /// player is hovering over the hit button 
    /// </summary>
    /// <param name="sender">your event params</param>
    /// <param name="e">your event params</param>
    void HitSelected(bool pressed)
    {
        audioSource.pitch = Random.Range(min, max);
        audioSource.PlayOneShot(audioClips[0], audioVolume[0]);
        standBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        //uiEffectedImage = standBtn.GetComponent<Image>();
        hitBtn.GetComponent<BasicUIEffect>().uiEffect.ExecuteAllEffects();
    }
    /// <summary>
    /// player is hovering over deal selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void DealSelected(object sender, EventArgs e)
    {
        audioSource.pitch = Random.Range(min, max);
        audioSource.PlayOneShot(audioClips[0], audioVolume[0]);
        //uiEffectedImage = standBtn.GetComponent<Image>();
        dealBtn.GetComponent<BasicUIEffect>().uiEffect.ExecuteAllEffects();
        OffReturn();
        //_effect.ExecuteAllEffects();
    }

    void ReturnToMain(object sender, EventArgs e)
    {
        sceneLoader.LoadMainScene();
    }
    void ReturnToMainSelected(bool down)
    {
        returnBtn.gameObject.SetActive(true);
        returnBtn.GetComponent<BasicUIEffect>().uiEffect.ExecuteAllEffects();
    }
    void OffReturn()
    {
        returnBtn.gameObject.SetActive(false);
        
    }


    /// <summary>
    /// player is happy with their bet, deal cards
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DealClicked(object sender, EventArgs e)
    {
        audioSource.pitch = Random.Range(0, 2);
        audioSource.PlayOneShot(audioClips[0], audioVolume[0]);
        dealBtn.GetComponent<BasicUIEffect>().uiEffect.ExecuteAllEffects();
       
        _bCanBet = false;
        _currentState = _eBlackJackStates.DEALING;
        BlackJackStateManager();

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
        
        standBtn.GetComponentInChildren<TMP_Text>().text = "Stand";
        //
        // Set standard pot size
        //pot = 40;
        betsText.text = "$" + (pot / 2).ToString() + " : Bet";
        //playerScript.GetAdjustMoney -= 20;
        cashText.text = "Bank : $" + playerScript.GetAdjustMoney.ToString();
       
        //}
    }

    /// <summary>
    /// player has sected to hit which gives player another card
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HitClicked(object sender, EventArgs e)
    {
        audioSource.pitch = Random.Range(min, max);
        audioSource.PlayOneShot(audioClips[0], audioVolume[0]);
        //check that there is still room on the table 
        if (playerScript.cardIndex <= 10)
        {
           
            playerScript.GetCard();
            scoreText.text = "Hand: " + playerScript.handValue.ToString();
            if (playerScript.handValue > 20) RoundOver();
        }

    }

    /// <summary>
    /// player is happy with current hand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StandClicked(object sender, EventArgs e)
    {
        audioSource.pitch = Random.Range(min, max);
        audioSource.PlayOneShot(audioClips[0], audioVolume[0]);
        Debug.Log("called stand");
        hitBtn.gameObject.SetActive(false);
        standBtn.gameObject.SetActive(true);

        _bCanBet = false;
        standClicks++;
        if (standClicks > 1) RoundOver();
        {
            
            HitDealer();

            standBtn.GetComponentInChildren<TMP_Text>().text = "Call";
        }



    }
/// <summary>
/// player asks for another card
/// </summary>
    private void HitDealer()
    {
        audioSource.pitch = Random.Range(0, 2);
        audioSource.PlayOneShot(audioClips[0], audioVolume[0]);

        _bCanBet = false;
            while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
            {
                dealerScript.GetCard();
                dealerScoreText.text = "Hand: " + dealerScript.handValue.ToString();
                if (dealerScript.handValue > 20) RoundOver();
            }
        
    }
    /// <summary>
    /// round is over function
    /// </summary>
    //check for winner 
    void RoundOver()
    {
        instructions.text = "Won " + pot;
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
            instructions.text = "Draw ";

        }
        // if player busts, dealer didnt, or if dealer has more points, deal wins 
        else if (playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue))
        {
            for (int i = 0; i < dealerScript.cardIndex; i++)
            {
                dealerScript.hand[i].GetComponent<BasicUIEffect>().uiEffect.ExecuteAllEffects();
               
            }
            instructions.text = "lost -" + (pot/2);
            mainText.text = "Dealer Wins!, " + dealerScript.handValue;
            //play sad music
            audioSource.PlayOneShot(audioClips[1], audioVolume[1]);
            //playerScript.GetAdjustMoney -= pot;
        }
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            for (int i = 0; i < playerScript.cardIndex; i++)
            {
                playerScript.hand[i].GetComponent<BasicUIEffect>().uiEffect.ExecuteAllEffects();
            }
            audioSource.PlayOneShot(audioClips[2], audioVolume[2]);
            mainText.text = "You win!";
            instructions.text = "Won " + pot;
            playerScript.GetAdjustMoney += pot;
        }
        else if (playerScript.handValue == dealerScript.handValue)
        {

            instructions.text = "Draw";
            mainText.text = "Draw"+ dealerScript.handValue;
            playerScript.GetAdjustMoney += (pot / 2);
        }
        else
        {
            roundOver = false;
        }
        // Set ui for next move / hand / turn
        if (roundOver)
        {

            _bCanBet = true;
            hitBtn.gameObject.SetActive(false);
            standBtn.gameObject.SetActive(false);
            //dealBtn.gameObject.SetActive(false);
            mainText.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);
            hideCard.GetComponent<Image>().enabled = false;
            cashText.text = "Bank : $" + playerScript.GetAdjustMoney.ToString();
            standClicks = 0;
            _currentState = _eBlackJackStates.END;
            BlackJackStateManager();
            _currentState = _eBlackJackStates.BETTING;
            FunctionTimer.Create(() => BlackJackStateManager(), 6f);
        }

    }

    //add money to pot if bet clicked
    /// <summary>
    /// player bet increases depending on how far they dragged the joystick
    /// </summary>
    /// <param name="distance"></param>
    void BetClicked(float distance)
    {
        OffReturn();
        //Debug.Log("up");

        if (pot > 0 && !dealBtn.gameObject.activeSelf)
        {
            controller.DragRightRelease += DealClicked;
            controller.DragRight += DealSelected;
            dealBtn.gameObject.SetActive(true);
            //event here button large shrink
        }
        if (playerScript.GetAdjustMoney > 0)
        {

            audioSource.pitch = Random.Range(min,max);
            audioSource.PlayOneShot(audioClips[0], audioVolume[0]);
            Debug.Log("sdfgjsdlgjsdlgkjl");
            Debug.Log(distance);

            int cash = (int)Mathf.Abs(distance / 100);
            if (cash > playerScript.GetAdjustMoney)
            {
                pot += (playerScript.GetAdjustMoney) * 2;
                playerScript.GetAdjustMoney = 0;

            }
            else
            {
                // cash = (cash > 1) ? cash : 1;
                Debug.Log(cash);

                playerScript.GetAdjustMoney -= cash;
                pot += cash * 2;
                //betsText.text = "$ " + pot.ToString();
                //playerScript.GetAdjustMoney -= 20;
               

            }
            cashText.text = "Bank : $" + playerScript.GetAdjustMoney.ToString();
            betsText.text = "$" + (pot / 2).ToString() + " : Bet";
        }

    }
    /// <summary>
    /// minuses bet amount
    /// </summary>
    /// <param name="distance"></param>
    void BetMinus(float distance)
    {
        OffReturn();
        if (pot < 0)
        {

            controller.DragRightRelease -= DealClicked;
            controller.DragRight -= DealSelected;
            dealBtn.gameObject.SetActive(false);
            //event here button large shrink
        }


        if (pot > 0)
        {
            int cash = (int)Mathf.Abs(distance / 100);
            if (cash > pot / 2)
            {
                playerScript.GetAdjustMoney += pot / 2;
                pot = 0;

            }
            else
            {
                audioSource.pitch = Random.Range(min, max);
                audioSource.PlayOneShot(audioClips[0], audioVolume[0]);
                pot -= cash/2;
                //betsText.text = "$ " + pot.ToString();
                playerScript.GetAdjustMoney += cash / 2;
                
            }
            cashText.text = "Bank : $" + playerScript.GetAdjustMoney.ToString();
            betsText.text = "$" + (pot / 2).ToString() + " : Bet";
        }

    }

}
