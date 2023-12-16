using UnityEngine;
using TMPro; // Necesario para trabajar con TextMeshPro

public class Estadisticas : MonoBehaviour
{
    // Define una variable p�blica para cada TextMeshPro
    public TextMeshProUGUI cansancio;
    public TextMeshProUGUI anios;
    public TextMeshProUGUI energia;

    // M�todo para actualizar el texto del primer TextMeshPro
    public void UpdateCansancio(string newText)
    {
        if (cansancio != null)
            cansancio.text = newText;
        else
            Debug.LogError("TextMeshPro1 no est� asignado.");
    }

    // M�todo para actualizar el texto del segundo TextMeshPro
    public void UpdateEnergia(string newText)
    {
        if (energia != null)
            energia.text = newText;
        else
            Debug.LogError("TextMeshPro2 no est� asignado.");
    }

    // M�todo para actualizar el texto del tercer TextMeshPro
    public void UpdateAnios(string newText)
    {
        if (anios != null)
            anios.text = newText;
        else
            Debug.LogError("TextMeshPro3 no est� asignado.");
    }
}