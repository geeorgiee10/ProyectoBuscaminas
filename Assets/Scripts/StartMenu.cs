using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

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
