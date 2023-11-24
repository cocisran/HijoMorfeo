using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTextByEvent : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void setText(object new_text)
    {
        text.text =  new_text.ToString();
    }

    public void updateTextOnEvent(Component sender, object data)
    {
        setText(data);
    }

}
