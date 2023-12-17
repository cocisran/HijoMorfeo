using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DreamScore : MonoBehaviour

{

    public Sprite[] dreamScoreBar;
    private SpriteRenderer spriteRenderer;
    private int maxScore = 10;
    private int currentScore = 1;
    private int currentSprite = 0;
    private float changeInterval = 0.5f;
    private float timer = 0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void setScore(int score)
    {
        currentScore = Mathf.Clamp(score, 1, maxScore);
        Debug.Log(String.Format("Current Score {0}", currentScore));
        currentSprite = currentScore * 2 - 2;

        spriteRenderer.sprite = dreamScoreBar[currentSprite];
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {

            timer = 0;

            if(currentSprite % 2 == 0)
            {
                currentSprite++;
                spriteRenderer.sprite = dreamScoreBar[currentSprite];
            } else if (currentSprite % 2 != 0)
            {
                currentSprite--;
                spriteRenderer.sprite = dreamScoreBar[currentSprite];
            }
        }
    }

    public void UpdateDreamScoreBar(Component caller, object data)
    {
        if (data is int)
        {
            int score = (int)data;
            setScore(score);
        }
    }
}
