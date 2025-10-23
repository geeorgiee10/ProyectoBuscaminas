using UnityEngine;

public class Generator : MonoBehaviour
{


    // Declaracion de variables

    [SerializeField] private GameObject piece;
    [SerializeField] private int width, height, bombsNumber;
    [SerializeField] private GameObject[][] map;


    public static Generator gen;

    private void Awake()
    {
        gen = this;
    }


    public void setWidth(int width)
    {
        this.width = width;
    }

    public void setHeight(int height)
    {
        this.height = height;
    }

    public void setBombsNumber(int bombsNumber)
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

        if (!(bombsNumber > 0 && bombsNumber < (height * width)))
            errorCode += 1;

        return errorCode;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Generate()
    {

        map = new GameObject[width][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new GameObject[height];
        }

        for (int j = 0; j < width; j++)
        {
            for (int i = 0; i < height; i++)
            {
                map[i][j] = Instantiate(piece, new Vector3(i, j, 0), Quaternion.identity);
                map[i][j].GetComponent<Piece>().setX(i);
                map[i][j].GetComponent<Piece>().setY(j);
            }
        }

        Camera.main.transform.position = new Vector3(((float)width / 2) - 0.5f, ((float)height / 2) - 0.5f, -10);

        for (int i = 0; i < bombsNumber; i++)
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

            // Cambiar color para asignar bomba
            // map[Random.Range(0, width)][Random.Range(0, height)].GetComponent<SpriteRenderer>().material.color = Color.red;
        }


    }

    public int GetBombsAround(int x, int y)
    {
        int cont = 0;

        // Casilla arriba izquierda
        if (x > 0 && y < height - 1 && map[x - 1][y + 1].GetComponent<Piece>().isBomb())
            cont++;

        // Justo encima
        if (y < height - 1 && map[x][y + 1].GetComponent<Piece>().isBomb())
            cont++;

        // Casilla arriba derecha
        if (x < width - 1 && y < height - 1 && map[x + 1][y + 1].GetComponent<Piece>().isBomb())
            cont++;

        // Casilla izquierda
        if (x > 0 && map[x - 1][y].GetComponent<Piece>().isBomb())
            cont++;

        // Casilla derecha
        if (x < width - 1 && map[x + 1][y].GetComponent<Piece>().isBomb())
            cont++;

        // Casilla abajo izquierda
        if (x > 0 && y > 0 && map[x - 1][y - 1].GetComponent<Piece>().isBomb())
            cont++;

        // Casilla justo abajo
        if (y > 0 && map[x][y - 1].GetComponent<Piece>().isBomb())
            cont++;

        // Casilla abajo derecha
        if (x < width - 1 && y > 0 && map[x + 1][y - 1].GetComponent<Piece>().isBomb())
            cont++;


        return cont;
    }

    public void CheckPieceAround(int x, int y)
    {
        // Casilla arriba izquierda
        if (x > 0 && y < height - 1)
            map[x - 1][y + 1].GetComponent<Piece>().DrawBomb();

        // Justo encima
        if (y < height - 1)
            map[x][y + 1].GetComponent<Piece>().DrawBomb();

        // Casilla arriba derecha
        if (x < width - 1 && y < height - 1)
            map[x + 1][y + 1].GetComponent<Piece>().DrawBomb();

        // Casilla izquierda
        if (x > 0)
            map[x - 1][y].GetComponent<Piece>().DrawBomb();

        // Casilla derecha
        if (x < width - 1)
            map[x + 1][y].GetComponent<Piece>().DrawBomb();

        // Casilla abajo izquierda
        if (x > 0 && y > 0)
            map[x - 1][y - 1].GetComponent<Piece>().DrawBomb();

        // Casilla justo abajo
        if (y > 0)
            map[x][y - 1].GetComponent<Piece>().DrawBomb();

        // Casilla abajo derecha
        if (x < width - 1 && y > 0)
            map[x + 1][y - 1].GetComponent<Piece>().DrawBomb();
    }


    public void ShowAllMap()
    {

        for (int j = 0; j < width; j++)
        {
            for (int i = 0; i < height; i++)
            {
                if (map[i][j].GetComponent<Piece>().isBomb())
                {
                    map[i][j].GetComponent<Piece>().DrawBomb();
                }
                
            }
        }
    }

}
