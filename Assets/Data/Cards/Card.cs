using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Card", menuName = "CardStats")]
public class Card: ScriptableObject
{
    [Header("UI")]
    public string descripcion = "descripcion corta";
    public string onSelectedHeader = "texto de transicion";
    public Sprite cardSprite;

    // Ver si cambiar a objeto
    [Header("GameControl")]
    public string[] etiquetas;
    public GameSupervisor.GameStages gameStage;

    [Header("CardStats")]
    public int desestres;
    public int edad;
    public int descanso;

    [Header("CardExtra")]
    public AudioSource cardSound;

}
