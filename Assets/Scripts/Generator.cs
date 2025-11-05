using UnityEngine;

public class Generator : MonoBehaviour
{

    // Declaración de variables
    [SerializeField] private GameObject piece;
    [SerializeField] public int width, height, bombsNumber;
    public GameObject[][] map;
    
    

    public static Generator gen;

    private void Awake()
    {
        gen = this;
    }



    public void SetWidth(int width)
    {
        this.width = width;
    }

    public void SetHeight(int height)
    {
        this.height = height;
    }

    public void SetBombsNumber(int bombsNumber)
    {
        this.bombsNumber = bombsNumber;
    }


    public int Validate()
    {
        int errorCode = 0;

        if (width <= 1)
            errorCode += 4;

        if (height <= 1)
            errorCode += 2;

        if (!(bombsNumber > 0 && bombsNumber < (width*height)))
            errorCode += 1;

        return errorCode;
    }





    public void Generate()
    {

        map = new GameObject[width][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new GameObject[height];
        }


        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                map[i][j] = Instantiate(piece, new Vector3(i, j, 0), Quaternion.identity);
                map[i][j].GetComponent<Piece>().setX(i);
                map[i][j].GetComponent<Piece>().setY(j);

            }

        }
        
        Camera.main.transform.position = new Vector3(((float)width/2) - 0.5f, ((float)height/2) - 0.5f, -10);

        for(int i = 0; i < bombsNumber; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            if (!map[x][y].GetComponent<Piece>().isBomb())
            {
                map[x][y].GetComponent<Piece>().setBomb(true);
            }
            else
            {
                i--;
            }

           // Cambiamos el color para asignar bomba
           // map[Random.Range(0, width)][Random.Range(0, height)].GetComponent<SpriteRenderer>().material.color = Color.red;
        }


    }



    public int GetBombsAround(int x, int y)
    {
        int cont = 0;
        // Arriba izquierda
        if (x > 0 && y < height - 1 && map[x - 1][y + 1].GetComponent<Piece>().isBomb())
            cont++;
        // Arriba
        if(y < height -1 && map[x][y + 1].GetComponent <Piece>().isBomb())
            cont++;
        //Arriba derecha
        if(x < width - 1 && y < height - 1 && map[x + 1][y + 1].GetComponent<Piece>().isBomb())
            cont++;
        // Izquierda
        if(x > 0 && map[x - 1][y].GetComponent<Piece>().isBomb())
            cont++;
        // Derecha
        if(x < width -1 && map[x + 1][y].GetComponent<Piece>().isBomb())
            cont++;
        //Abajo Izquierda
        if (x > 0 && y > 0 && map[x - 1][y - 1].GetComponent<Piece>().isBomb())
            cont++;
        //Abajo
        if (y > 0 && map[x][y - 1].GetComponent<Piece>().isBomb())
            cont++;
        //Abajo Derecha
        if (x < width - 1 && y > 0 && map[x + 1][y - 1].GetComponent<Piece>().isBomb())
            cont++;


        return cont;
    }

    public void CheckPieceAround(int x, int y)
    {
        if (x > 0 && y < height - 1)        
            map[x - 1][y + 1].GetComponent<Piece>().DrawBomb();      
            
        // Arriba
        if (y < height - 1 )
            map[x][y + 1].GetComponent<Piece>().DrawBomb();
        //Arriba derecha
        if (x < width - 1 && y < height - 1)
            map[x + 1][y + 1].GetComponent<Piece>().DrawBomb();
        // Izquierda
        if (x > 0 )
            map[x - 1][y].GetComponent<Piece>().DrawBomb();
        // Derecha
        if (x < width - 1)
            map[x + 1][y].GetComponent<Piece>().DrawBomb(); 
        //Abajo Izquierda
        if (x > 0 && y > 0)
            map[x - 1][y -   1].GetComponent<Piece>().DrawBomb();
        //Abajo
        if (y > 0)
            map[x][y - 1].GetComponent<Piece>().DrawBomb();
        //Abajo Derecha
        if (x < width - 1 && y > 0)
            map[x + 1][y - 1].GetComponent<Piece>().DrawBomb();
    }


}
