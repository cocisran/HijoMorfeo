using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSupervisor : MonoBehaviour
{
    [Header("Cartas")]
    public GameObject cardModel;
    public GameObject option1;
    public GameObject option2;


    [Header("Cartas Source")]
    public string pathCharacterCards;
    public string pathPlaceCards;
    public string pathSituationCards;

    private Card[] characterCards;
    private Card[] placeCards;
    private Card[] situationCards;

    

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
        characterCards = Resources.LoadAll<Card>(pathCharacterCards);
        placeCards = Resources.LoadAll<Card>(pathPlaceCards);
        situationCards = Resources.LoadAll<Card>(pathSituationCards);

        // Asignamos las primeras cartas
        getNextCards();
        
    }

    void getNextCards()
    {
        if (CurrentGameStage == GameStages.DreamEvaluation)
            return;

        Card[] source = new Card[0];
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
        int option1Index = UnityEngine.Random.Range(0, source.Length);
        int option2Index = UnityEngine.Random.Range(0, source.Length);
        if (option2Index == option1Index)
            option2Index = (option2Index + 1) % source.Length;


        CardBehavior behavior = cardModel.GetComponent<CardBehavior>();
        behavior.cardInfo = source[option1Index];
        GameObject newOption1 = Instantiate(cardModel, option1.transform.position, Quaternion.identity);

        behavior.cardInfo = source[option2Index];
        GameObject newOption2 = Instantiate(cardModel, option2.transform.position, Quaternion.identity);

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
