using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dreamer", menuName = "DreamerStats")]
public class Dreamer : ScriptableObject
{
    [Header("UI")]
    public Sprite characterSprite;

    [Header("DreamerStats")]
    [Space]
    [Header("Estres")]
    public int estres_level_min;
    public int estres_level_max;
    [Header("Edad")]
    public int edad_level_min;
    public int edad_level_max;
    [Header("Descanso")]
    public int descanso_level_min;
    public int descanso_level_max;

    // Internos
    private int estres_level;
    private int edad;
    private int descanso_level;

    private void OnEnable()
    {
        estres_level = Random.Range(estres_level_min, estres_level_max + 1);
        edad = Random.Range(edad_level_min, edad_level_max + 1);
        descanso_level = Random.Range(descanso_level_min, descanso_level_max);
    }

    public int evalDream(int estres_interaction, int edad_interaction, int descanso_interaction)
    {

        return Mathf.Abs(estres_level - estres_interaction) +
               Mathf.Abs(edad - edad_interaction) +
               Mathf.Abs(descanso_level - descanso_interaction);
    }

    public int getEstres() { return estres_level;}
    public int getEdad() { return edad; }
    public int getDescanso() { return descanso_level; }
}
