using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamerBehavior : MonoBehaviour
{
    public SpriteRenderer sprite;


    public void ChangeSprite(Component caller, object data)
    {
        Sprite dreamerSprite = (Sprite)data;
        sprite.sprite = dreamerSprite;
    }

}
