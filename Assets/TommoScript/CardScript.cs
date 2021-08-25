using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    //value of card. 2 lf clubs = 2, ect
    private int value = 0;
    private Image m_image;

    private void Awake()
    {
        m_image = GetComponent<Image>();
            //sprRender = GetComponent<SpriteRenderer>();
    }

    public int GetValueOfCard { get => value; set => this.value = value; }
    public void SetSprite(Sprite newSprite)
    {
       
        gameObject.GetComponent<Image>().sprite = newSprite;

    }
    public string GetSpriteName()
    {
        return m_image.sprite.name;
    }
    public void ResetCard()
    {
        Sprite back = GameObject.Find("Deck").GetComponent<DeckHand>().GetCardBack();
        gameObject.GetComponent<Image>().sprite = back;
       // m_image.sprite = back;
        //sprRender.sprite = back;
        value = 0;


    }
    
}
