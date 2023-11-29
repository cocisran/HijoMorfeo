using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardBehavior : MonoBehaviour
{
    [Header("CardData")]
    public Card cardInfo;

    [Header("Events")]
    public GameEvent stageChangeEvent;

    [Header("CardComponets")]
    public SpriteRenderer sprite;
    public TextMeshPro description;

    private void OnEnable()
    {
        description.text = cardInfo.descripcion;
        sprite.sprite = cardInfo.cardSprite;
    }

    private void OnMouseDown()
    {
       // sprite.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        stageChangeEvent.Raise(this, cardInfo);
    }

}
