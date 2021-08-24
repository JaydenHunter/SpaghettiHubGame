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
    void GetCardValues()
    {
        int num = 0;
        //loop to set all values of card
        for (int i = 0; i < cardSprites.Length; i++)
        {
            num = i;
            // Count up to the amountr of cards, 52
            num %= 13;
            // if there is a remainder after x/13, then remainder
            // is used as the value, 
     
            if (num > 10 || num == 0)
            {
                num = 10;
            }
            cardValues[i] = num++;

        }
        currentIndex = 1;
    }
    public void Shuffle()
    {
        for (int i = 0; i < cardSprites.Length -1; i++)
        {
        int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardSprites.Length - 1) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            int value = cardValues[j];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;
        }

    }
    public int DealCard()
    {
        return 0;
    }
    
    public int DealCard(CardScript cardScript)
    {
        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.GetValueOfCard = cardValues[currentIndex++];
        return cardScript.GetValueOfCard;
    }
    public void gogo(int blue)
    {

    }
    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
