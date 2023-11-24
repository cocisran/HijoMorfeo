using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSupervisor : MonoBehaviour
{
    [Header("Cartas")]
    public GameObject[] cards;

    private List<GameObject> characterCards;
    private List<GameObject> placeCards;
    private List<GameObject> situationCards;

    public GameObject option1;
    public GameObject option2;

    [Header("UI")]
    public TextMeshProUGUI header;
  
    [Header("GameVariables")]
    public string history;
    public static GameStages CurrentGameStage;
    public String historyStart = "Había una vez..";

    private bool gameGoesOn;
    

    public enum GameStages
    {
        SelectCharacter,
        SelectScenary,
        SelectSituation,
        DreamEvaluation
    }

    void OnEnable()
    {
        gameGoesOn = true;
        CurrentGameStage = GameStages.SelectCharacter;
        header.text = historyStart;

        // Para obtimizar busqueda
        characterCards = new List<GameObject>();
        placeCards = new List<GameObject>(); 
        situationCards = new List<GameObject>(); 
        foreach (GameObject card in cards)
        {
            CardBehavior cardBehavior = card.GetComponent<CardBehavior>();
            switch (cardBehavior.cardInfo.gameStage)
            {
                case GameStages.SelectCharacter:
                    {
                        characterCards.Add(card);
                        break;
                    }
                case GameStages.SelectScenary:
                    {
                        placeCards.Add(card);
                        break;
                    }
                case GameStages.SelectSituation:
                    {
                        situationCards.Add(card);
                        break;
                    }
            }
        }

        // Asignamos las primeras cartas
        getNextCards();
        
    }

    void getNextCards()
    {
        if (CurrentGameStage == GameStages.DreamEvaluation)
            return;

        List<GameObject> source = new List<GameObject>();
        switch (CurrentGameStage)
        {
            case GameStages.SelectCharacter:
                {
                    source = characterCards;
                    break;
                }
            case GameStages.SelectScenary:
                {
                    source = placeCards;
                    break;
                }
            case GameStages.SelectSituation:
                {
                    source = situationCards;
                    break;
                }
        }
        int option1Index = UnityEngine.Random.Range(0, source.Count);
        int option2Index = UnityEngine.Random.Range(0, source.Count);
        if (option2Index == option1Index)
            option2Index = (option2Index + 1) % characterCards.Count;

        GameObject newOption1 = Instantiate(source[option1Index], option1.transform.position, Quaternion.identity);
        GameObject newOption2 = Instantiate(source[option2Index], option2.transform.position, Quaternion.identity);

        Destroy(option1);
        Destroy(option2);

        option1 = newOption1;
        option2 = newOption2;

    }

    void NextStage()
    {
        int num_stages = Enum.GetValues(typeof(GameStages)).Length;
        int current_stage = (int)CurrentGameStage;
        CurrentGameStage = (GameStages) Enum.ToObject(typeof(GameStages), (current_stage + 1) % num_stages);
    }

    public void UserSelectedOption(Component caller, object data)
    {
        if (!gameGoesOn)
            return;

        Card card_selected = (Card)data;


        // Realizamos nuestras acciones de fase y preparamos la siguiente
        switch (CurrentGameStage)
        {
            case GameStages.SelectCharacter:
                {
                    header.text = card_selected.onSelectedHeader;
                    history = historyStart + " " + card_selected.descripcion + " " + card_selected.onSelectedHeader;
                    break;
                }
            case GameStages.SelectScenary:
                {
                    header.text = card_selected.onSelectedHeader;
                    history +=  card_selected.descripcion + " " + card_selected.onSelectedHeader ;
                    break;
                }
            case GameStages.SelectSituation:
                {
                    history += " " + card_selected.descripcion;
                    header.text = historyStart;
                    break;
                }
        }

        NextStage();
        getNextCards();
        Debug.Log(history);
    }
}
