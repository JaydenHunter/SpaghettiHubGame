using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    //value of card. 2 lf clubs = 2, ect
    private int value = 0;
    SpriteRenderer sprRender;
    Image m_image;

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
        return sprRender.sprite.name;
    }
    public void ResetCard()
    {
        Sprite back = GameObject.Find("DeckController").GetComponent<DeckHand>().GetCardBack();
        m_image.sprite = back;
        //sprRender.sprite = back;
        value = 0;


    }
    
}
