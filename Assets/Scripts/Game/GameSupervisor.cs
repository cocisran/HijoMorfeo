using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSupervisor : MonoBehaviour {
    [Header("Events")]
    public GameEvent onDreamScoreChange;
    public GameEvent changeDreamerEvent;
    [SerializeField] public AngerBar angerBar;
    [SerializeField] public Estadisticas estadisticas;

    [Header("Maxima diferencia de calificaci�n")]
    public int maxDiferencia;

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

    [Header("Dreamer Source")]
    public string pathDreamers;

    private Dreamer[] dreamers;

    [Header("UI")]
    public TextMeshProUGUI header;
    public TextMeshProUGUI dreamHistory;
    public GameObject DreamEvaluator;

    [Header("GameVariables")]
    public string history;
    public static GameStages CurrentGameStage;
    public String historyStart = "Hab�a una vez.. ";
    public int desestres = 0;
    public int edad = 0;
    public int descanso = 0;
    public Dreamer currentDreamer = null;

    private bool gameGoesOn;


    public enum GameStages {
        SelectCharacter,
        SelectScenary,
        SelectSituation,
        DreamEvaluation
    }

    void OnEnable() {

        gameGoesOn = true;
        CurrentGameStage = GameStages.SelectCharacter;
        header.text = historyStart;

        // Para optimizar busqueda
        characterCards = Resources.LoadAll<Card>(pathCharacterCards);
        placeCards = Resources.LoadAll<Card>(pathPlaceCards);
        situationCards = Resources.LoadAll<Card>(pathSituationCards);
        dreamers = Resources.LoadAll<Dreamer>(pathDreamers);

        // Creamos el primer so�ador
        changeCurrentDreamer();
        // Asignamos las primeras cartas
        getNextCards();

    }

    void changeCurrentDreamer() {
        int index = UnityEngine.Random.Range(0, dreamers.Length);
        currentDreamer = dreamers[index];
        currentDreamer.resetStats();

        //  modificar el sprite
        changeDreamerEvent.Raise(this, currentDreamer.characterSprite);

        estadisticas.UpdateCansancio(currentDreamer.getDescanso().ToString());
        estadisticas.UpdateAnios(currentDreamer.getEdad().ToString());
        estadisticas.UpdateEstres(currentDreamer.getEstres().ToString());
    }
    void getNextCards() {
        if (CurrentGameStage == GameStages.DreamEvaluation)
            return;

        Card[] source = new Card[0];
        switch (CurrentGameStage) {
            case GameStages.SelectCharacter: {
                    source = characterCards;
                    break;
                }
            case GameStages.SelectScenary: {
                    source = placeCards;
                    break;
                }
            case GameStages.SelectSituation: {
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

    void NextStage() {
        int num_stages = Enum.GetValues(typeof(GameStages)).Length;
        int current_stage = (int)CurrentGameStage;
        CurrentGameStage = (GameStages)Enum.ToObject(typeof(GameStages), (current_stage + 1) % num_stages);
    }

    public void UserSelectedOption(Component caller, object data) {
        if (!gameGoesOn)
            return;

        Card card_selected = (Card)data;

        estadisticas.UpdateCansancio(currentDreamer.getDescanso().ToString());
        estadisticas.UpdateAnios(currentDreamer.getEdad().ToString());
        estadisticas.UpdateEstres(currentDreamer.getEstres().ToString());

        // Realizamos nuestras acciones de fase y preparamos la siguiente
        switch (CurrentGameStage) {
            case GameStages.SelectCharacter: {
                    header.text = card_selected.onSelectedHeader;
                    history = historyStart + " " + card_selected.descripcion + " " + card_selected.onSelectedHeader;
                    break;
                }
            case GameStages.SelectScenary: {
                    header.text = card_selected.onSelectedHeader;
                    history += " " + card_selected.descripcion + " " + card_selected.onSelectedHeader;
                    break;
                }
            case GameStages.SelectSituation: {
                    history += " " + card_selected.descripcion;
                    header.text = historyStart;
                    NextStage();
                    break;
                }
        }
       
        if (CurrentGameStage != GameStages.DreamEvaluation) {
            desestres += card_selected.desestres;
            edad += card_selected.edad;
            descanso += card_selected.descanso;
        } else {
            // CHECK
            int dream_score = currentDreamer.evalDream(desestres, edad, descanso);
            Debug.Log(dream_score);
            dream_score = Mathf.Clamp((dream_score * 100) / 150, 0, 100);
            dream_score = 100 - dream_score;
            Debug.Log(String.Format("###Evaluaci�n final: {0}", dream_score));
            if (dream_score <= maxDiferencia) {
                angerBar.UpdateAngerBar(maxDiferencia);
            }
            DreamEvaluator.SetActive(true);
            onDreamScoreChange.Raise(this, dream_score / 10);
            Invoke("newRound", 1.5f);
            // Aqui se debe notificar a donde corresponda de la fase de evaluacion
            
        }

        dreamHistory.text = history;
   
        Debug.Log("**GAME STATE**");
        Debug.Log(String.Format("\t\tElecciones\n\tEdad : {0} \tDesestres : {1}\tDescanso {2}", edad, desestres, descanso));
        Debug.Log(String.Format("\t\tEstadisticas so�ador\n\tEdad : {0} \tEstres : {1}\tDescanso {2}",
                  currentDreamer.getEdad(), currentDreamer.getEdad(), currentDreamer.getDescanso()));
        Debug.Log(angerBar.ObtenerVida());
        if (angerBar.ObtenerVida() == 100) {
            Debug.Log("Yamuereteprro");
            SceneManager.LoadScene("Game_over");
        }
        NextStage();
        getNextCards();
    }

    void newRound()
    {
        DreamEvaluator.SetActive(false);
        getNextCards();
        changeCurrentDreamer();
        desestres = edad = descanso = 0;
        history = "";
        dreamHistory.text = history;
    }
}
