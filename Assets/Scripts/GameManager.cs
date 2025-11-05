using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject startMenu;
    public GameObject endMenu;

    public int flagCounter = 0;
    public int checkConuter = 0;


    public static GameManager instance;
    public bool endgame;
    public bool win = true;


    private void Awake()
    {
        if(instance == null)
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
       
    }


    public void GameStart()
    {
        int height = int.Parse(StartMenu.instance.heightInput.GetComponentInChildren<TMP_InputField>().text.ToString());
        int width = int.Parse(StartMenu.instance.widthInput.GetComponentInChildren<TMP_InputField>().text.ToString());
        int bombsNumber = int.Parse(StartMenu.instance.bombsInput.GetComponentInChildren<TMP_InputField>().text.ToString());
        
        Generator.gen.SetHeight(height);
        Generator.gen.SetWidth(width);
        Generator.gen.SetBombsNumber(bombsNumber);

        if (Generator.gen.Validate() == 0) {
            Generator.gen.Generate();
            startMenu.SetActive(false);
        }
        else
        {
           // Mostrar error en pantalla.


        }
        

    }

    public void EndGame() 
    {
        endgame = true;
        for ( int x = 0; x < Generator.gen.map.Length; x++)
        {
            for ( int y = 0; y < Generator.gen.map[x].Length; y++)
            {
                Generator.gen.map[x][y].GetComponent<Piece>().DrawBomb();
            }
        }


        endMenu.SetActive(true);
        if (win)
            endMenu.transform.GetChild(0).gameObject.SetActive(true);
        else
            endMenu.transform.GetChild(1).gameObject.SetActive(true);



    }

}
