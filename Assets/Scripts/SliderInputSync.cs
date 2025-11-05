using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderInputSync : MonoBehaviour
{
    public Slider slider; 
    public TMP_InputField inputField; 
    void Start()
    {
        // Configurar eventos para sincronizar valores
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        inputField.onEndEdit.AddListener(OnInputFieldValueChanged);

        // Inicializar valores
        inputField.text = slider.value.ToString();
    }

    // Cuando el Slider cambia
    private void OnSliderValueChanged(float value)
    {
        inputField.text = value.ToString();
    }

    // Cuando el InputField cambia
    private void OnInputFieldValueChanged(string value)
    {
        if (float.TryParse(value, out float result))
        {
            slider.value = result; // Actualiza el Slider
        }
    }
}
