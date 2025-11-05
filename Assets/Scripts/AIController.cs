using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.Collections;

public class AIController : MonoBehaviour
{
    // Declaración de variables (necesitarás algunas más)
    public float turnTime = 0.5f;
    public GameObject[][] map;

    public List<GameObject> mapPieceCheckedWithBombs = new List<GameObject>();

    public int height, width;


    // El Bot comienza a Jugar. Este Código no hay que cambiarlo
    void Start()
    {
        map = Generator.gen.map;
        height = Generator.gen.height;
        width = Generator.gen.width;
        StartCoroutine(Play());
    }


    System.Collections.IEnumerator Play()
    {
        yield return new WaitForSeconds(1f);

        while (!GameManager.instance.endgame)
        {
            bool actionDone = LogicPlay();
            if (!actionDone)
            {
                // Si no hay lógica aplicable, jugar aleatoriamente
                RandomPlay();
            }

            yield return new WaitForSeconds(turnTime);
        }
    }


    // Lógica general del bot

    bool LogicPlay()
    {
        bool action = false;


        // Buscamos todas las casilla comprobadas con bombas alrededor (check == true)
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (map[i][j].GetComponent<Piece>().isCheck())
                {
                    if(Generator.gen.GetBombsAround(i,j) != 0)
                    {
                        mapPieceCheckedWithBombs.Add(map[i][j]);
                    }
                }
            }

        }
           
            // Para cada casilla comprobada   

                // Regla 1: todas ocultas son minas: click_derecho (Flag) 
            

                // Regla 2: todas ocultas son seguras: clic_izquierdo (Flag)
            
        

        return action;
    }

    void RandomPlay()
    {
        // De todas las celdas que no se han chequeado, click_izquierdo en una de forma aleatoria;
    }

    public int piezasNoCheckeadasAlrededor(int x, int y)
    {
        int cont = 0;
        // Arriba izquierda
        if (x > 0 && y < height - 1 && !map[x - 1][y + 1].GetComponent<Piece>().isCheck())
            cont++;
        // Arriba
        if (y < height - 1 && !map[x][y + 1].GetComponent<Piece>().isCheck())
            cont++;
        //Arriba derecha
        if (x < width - 1 && y < height - 1 && !map[x + 1][y + 1].GetComponent<Piece>().isCheck())
            cont++;
        // Izquierda
        if (x > 0 && !map[x - 1][y].GetComponent<Piece>().isCheck())
            cont++;
        // Derecha
        if (x < width - 1 && !map[x + 1][y].GetComponent<Piece>().isCheck())
            cont++;
        //Abajo Izquierda
        if (x > 0 && y > 0 && !map[x - 1][y - 1].GetComponent<Piece>().isCheck())
            cont++;
        //Abajo
        if (y > 0 && !map[x][y - 1].GetComponent<Piece>().isCheck())
            cont++;
        //Abajo Derecha
        if (x < width - 1 && y > 0 && !map[x + 1][y - 1].GetComponent<Piece>().isCheck())
            cont++;


        return cont;
    }
    

    public void regla1(){
        foreach (GameObject piece in mapPieceCheckedWithBombs)
        {
            int x = piece.GetComponent<Piece>().getX();
            int y = piece.GetComponent<Piece>().getY();

            int numBombsAround = Generator.gen.GetBombsAround(x, y);

            int numPiecesNotCheckedAround = piezasNoCheckeadasAlrededor(x, y);

            //Numero de bombas alrededor es igual al numero de piezas que no han sido checkeadas que estan alrededor se dibujan banderas
            if (numPiecesNotCheckedAround == numBombsAround)
            {
                if (x > 0 && y < height - 1 && !map[x - 1][y + 1].GetComponent<Piece>().isCheck())
                    map[x - 1][y + 1].GetComponent<Piece>().DrawFlag();
                // Arriba
                if (y < height - 1 && !map[x][y + 1].GetComponent<Piece>().isCheck())
                    map[x][y + 1].GetComponent<Piece>().DrawFlag();
                //Arriba derecha
                if (x < width - 1 && y < height - 1 && !map[x + 1][y + 1].GetComponent<Piece>().isCheck())
                    map[x + 1][y + 1].GetComponent<Piece>().DrawFlag();
                // Izquierda
                if (x > 0 && !map[x - 1][y].GetComponent<Piece>().isCheck())
                    map[x - 1][y].GetComponent<Piece>().DrawFlag();
                // Derecha
                if (x < width - 1 && !map[x + 1][y].GetComponent<Piece>().isCheck())
                    map[x + 1][y].GetComponent<Piece>().DrawFlag();
                //Abajo Izquierda
                if (x > 0 && y > 0 && !map[x - 1][y - 1].GetComponent<Piece>().isCheck())
                    map[x - 1][y - 1].GetComponent<Piece>().DrawFlag();
                //Abajo
                if (y > 0 && !map[x][y - 1].GetComponent<Piece>().isCheck())
                    map[x][y - 1].GetComponent<Piece>().DrawFlag();
                //Abajo Derecha
                if (x < width - 1 && y > 0 && !map[x + 1][y - 1].GetComponent<Piece>().isCheck())
                    map[x + 1][y - 1].GetComponent<Piece>().DrawFlag();
                }
        }
    }
}

