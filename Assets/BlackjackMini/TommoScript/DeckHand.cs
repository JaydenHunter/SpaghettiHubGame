///Tomas Munro's Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHand : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[53];
    int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetCardValues();

    }
    /// <summary>
    /// get card values
    /// </summary>
    void GetCardValues()
    {
        int num = 0;
        // Loop to assign values to the cards
        for (int i = 0; i < cardSprites.Length; i++)
        {
            num = i;
            // Count up to the amout of cards, 52
            num %= 13;
            // if there is a remainder after x/13, then remainder
            // is used as the value, unless over 10, the use 10
            if (num > 10 || num == 0)
            {
                num = 10;
            }
            cardValues[i] = num++;
        }
    }
    /// <summary>
    /// shuffle cards 
    /// </summary>
    public void Shuffle()
    {
        // Standard array data swapping technique
        for (int i = cardSprites.Length - 1; i > 0; --i)
        {
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardSprites.Length - 1) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;
        }
        currentIndex = 1;

    }

    /// <summary>
    /// deals card to hand
    /// </summary>
    /// <param name="cardScript"></param>
    /// <returns></returns>
    public int DealCard(CardScript cardScript)
    {
       cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.GetValueOfCard = cardValues[currentIndex];
        ++currentIndex;
        return cardScript.GetValueOfCard;
    }
    /// <summary>
    /// get card back 
    /// </summary>
    /// <returns></returns>
    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }

}
