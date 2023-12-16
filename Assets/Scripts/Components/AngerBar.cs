using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngerBar : MonoBehaviour {

    public Slider slider;


    public void CambiarVidaActual(float vidaActual) {
        slider.value = vidaActual;
    }

    public void IniciarBarraVida(float valor) {
        CambiarVidaActual(valor);
    }

    // Start is called before the first frame update
    void Start() {
        slider = GetComponent<Slider>();
        slider.value = 0f;
    }

    public void UpdateAngerBar(Component caller, object data) {

        if (data is float) {
            float score = (float)data;
            CambiarVidaActual(score);
        }
    }

    public void UpdateAngerBar(float value) {
        CambiarVidaActual(slider.value + value);
    }
}
