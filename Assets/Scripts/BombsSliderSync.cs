using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BombsSliderSync : MonoBehaviour
{
    public Slider bombsSlider, widthSlider, heightSlider;
    public TMP_InputField inputField;
    void Start()
    {
        // Configurar eventos para sincronizar valores
        bombsSlider.onValueChanged.AddListener(delegate { BombsSliderChanged();});
        widthSlider.onValueChanged.AddListener(delegate { SliderChanged(); });
        heightSlider.onValueChanged.AddListener(delegate { SliderChanged(); });
        inputField.onEndEdit.AddListener(OnInputFieldValueChanged);

        // Inicializar valores
        inputField.text = bombsSlider.value.ToString();
    }

    // Cuando el Slider cambia
    public void BombsSliderChanged()
    {
        inputField.text = bombsSlider.value.ToString();
    }

    public void SliderChanged()
    {
        bombsSlider.maxValue = widthSlider.value * heightSlider.value - 1;

        if(bombsSlider.maxValue < 0 )
            bombsSlider.maxValue = 0;
    }


    // Cuando el InputField cambia
    private void OnInputFieldValueChanged(string value)
    {
        if (float.TryParse(value, out float result))
        {
            bombsSlider.value = result; // Actualiza el Slider
        }
    }
}
