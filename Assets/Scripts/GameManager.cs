using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject startMenu;

    public GameObject endMenu;

    public GameObject inputError;

    public static GameManager instance;

    public bool endgame;



    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        DontDestroyOnLoad(gameObject);

        startMenu.SetActive(true);
        endMenu.SetActive(false);
        inputError.SetActive(false);
    }

    public void GameStart()
    {
        Generator.gen.setWidth(int.Parse(StartMenu.instance.widthInput.GetComponentInChildren<TMP_InputField>().text.ToString()));
        Generator.gen.setHeight(int.Parse(StartMenu.instance.heightInput.GetComponentInChildren<TMP_InputField>().text.ToString()));
        Generator.gen.setBombsNumber(int.Parse(StartMenu.instance.bombsInput.GetComponentInChildren<TMP_InputField>().text.ToString()));

        if (Generator.gen.Validate() == 0)
        {
            Generator.gen.Generate();

            startMenu.SetActive(false);
        }
        else
        {
            // Mostrar error en pantalla
            inputError.SetActive(true);
        }

        
    }

}
