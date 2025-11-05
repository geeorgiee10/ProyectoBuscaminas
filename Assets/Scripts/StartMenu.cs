using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StartMenu : MonoBehaviour
{
     public TMP_InputField widthInput;
     public TMP_InputField heightInput;
     public TMP_InputField bombsInput;


    public static StartMenu instance;


    public void Start()
    {
        instance = this;
    }

}
