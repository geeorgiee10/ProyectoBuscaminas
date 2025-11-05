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
    public void Start()
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
        mapPieceCheckedWithBombs.Clear();
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

                foreach (GameObject piece in mapPieceCheckedWithBombs)
                {
                    
                // Regla 1: todas ocultas son minas: click_derecho (Flag) 
                    if(regla1(piece)){
                        action = true;
                    }

                    // Regla 2: todas ocultas son seguras: clic_izquierdo (Flag)

                    if(regla2(piece)){
                        action = true;
                    }
                }
        

        return action;
    }

    void RandomPlay()
    {
        // De todas las celdas que no se han chequeado, click_izquierdo en una de forma aleatoria;
        List<GameObject> piecesNotChecked = new List<GameObject>();
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if(!map[i][j].GetComponent<Piece>().isCheck() && !map[i][j].GetComponent<Piece>().isFlag())
                {
                    piecesNotChecked.Add(map[i][j]);
                }
            }
        }

        if(piecesNotChecked.Count != 0)
        {
            int aleatorio = Random.Range(0, piecesNotChecked.Count);
            piecesNotChecked[aleatorio].GetComponent<Piece>().OnLeftClick();
        }
    }

    public int piezasNoCheckeadasAlrededor(int x, int y)
    {
        int cont = 0;
        // Arriba izquierda
        if (x > 0 && y < height - 1 && !map[x - 1][y + 1].GetComponent<Piece>().isCheck() && !map[x - 1][y + 1].GetComponent<Piece>().isFlag())
            cont++;
        // Arriba
        if (y < height - 1 && !map[x][y + 1].GetComponent<Piece>().isCheck() && !map[x][y + 1].GetComponent<Piece>().isFlag())
            cont++;
        //Arriba derecha
        if (x < width - 1 && y < height - 1 && !map[x + 1][y + 1].GetComponent<Piece>().isCheck() && !map[x + 1][y + 1].GetComponent<Piece>().isFlag())
            cont++;
        // Izquierda
        if (x > 0 && !map[x - 1][y].GetComponent<Piece>().isCheck() && !map[x - 1][y].GetComponent<Piece>().isFlag())
            cont++;
        // Derecha
        if (x < width - 1 && !map[x + 1][y].GetComponent<Piece>().isCheck() && !map[x + 1][y].GetComponent<Piece>().isFlag())
            cont++;
        //Abajo Izquierda
        if (x > 0 && y > 0 && !map[x - 1][y - 1].GetComponent<Piece>().isCheck() && !map[x - 1][y - 1].GetComponent<Piece>().isFlag())
            cont++;
        //Abajo
        if (y > 0 && !map[x][y - 1].GetComponent<Piece>().isCheck() && !map[x][y - 1].GetComponent<Piece>().isFlag())
            cont++;
        //Abajo Derecha
        if (x < width - 1 && y > 0 && !map[x + 1][y - 1].GetComponent<Piece>().isCheck() && !map[x + 1][y - 1].GetComponent<Piece>().isFlag())
            cont++;


        return cont;
    }
    

    public bool regla1(GameObject piece)
    {
        bool accion = false;

        
            int x = piece.GetComponent<Piece>().getX();
            int y = piece.GetComponent<Piece>().getY();

            int numBombsAround = Generator.gen.GetBombsAround(x, y);

            int numPiecesNotCheckedAround = piezasNoCheckeadasAlrededor(x, y);

            //Numero de bombas alrededor es igual al numero de piezas que no han sido checkeadas que estan alrededor se dibujan banderas
            if (numPiecesNotCheckedAround == numBombsAround)
            {
                if (x > 0 && y < height - 1 && !map[x - 1][y + 1].GetComponent<Piece>().isCheck() && !map[x - 1][y + 1].GetComponent<Piece>().isFlag()){
                    map[x - 1][y + 1].GetComponent<Piece>().DrawFlag();
                    accion = true;
                }
                    
                // Arriba
                if (y < height - 1 && !map[x][y + 1].GetComponent<Piece>().isCheck() && !map[x][y + 1].GetComponent<Piece>().isFlag()){
                    map[x][y + 1].GetComponent<Piece>().DrawFlag();
                    accion = true;
                }
                    
                //Arriba derecha
                if (x < width - 1 && y < height - 1 && !map[x + 1][y + 1].GetComponent<Piece>().isCheck() && !map[x + 1][y + 1].GetComponent<Piece>().isFlag()){
                    map[x + 1][y + 1].GetComponent<Piece>().DrawFlag();
                    accion = true;
                }
                    
                // Izquierda
                if (x > 0 && !map[x - 1][y].GetComponent<Piece>().isCheck() && !map[x - 1][y].GetComponent<Piece>().isFlag()){
                    map[x - 1][y].GetComponent<Piece>().DrawFlag();
                    accion = true;
                }
                    
                // Derecha
                if (x < width - 1 && !map[x + 1][y].GetComponent<Piece>().isCheck() && !map[x + 1][y].GetComponent<Piece>().isFlag()){
                    map[x + 1][y].GetComponent<Piece>().DrawFlag();
                    accion = true;
                }
                    
                //Abajo Izquierda
                if (x > 0 && y > 0 && !map[x - 1][y - 1].GetComponent<Piece>().isCheck() && !map[x - 1][y - 1].GetComponent<Piece>().isFlag()){
                    map[x - 1][y - 1].GetComponent<Piece>().DrawFlag();
                    accion = true;
                }
                    
                //Abajo
                if (y > 0 && !map[x][y - 1].GetComponent<Piece>().isCheck() && !map[x][y - 1].GetComponent<Piece>().isFlag()){
                    map[x][y - 1].GetComponent<Piece>().DrawFlag();
                    accion = true;
                }
                    
                //Abajo Derecha
                if (x < width - 1 && y > 0 && !map[x + 1][y - 1].GetComponent<Piece>().isCheck() && !map[x + 1][y - 1].GetComponent<Piece>().isFlag()){
                    map[x + 1][y - 1].GetComponent<Piece>().DrawFlag();
                    accion = true;
                }
                    
            }
        

        return accion;
    }

    public bool regla2(GameObject piece)
    {
        bool accion = false;

        
            int x = piece.GetComponent<Piece>().getX();
            int y = piece.GetComponent<Piece>().getY();

            int numBombsAround = Generator.gen.GetBombsAround(x, y);

            int flagsAround = 0;

            if (x > 0 && y < height - 1 && !map[x - 1][y + 1].GetComponent<Piece>().isFlag()){
                flagsAround++;
            }
                    
            // Arriba
            if (y < height - 1 && !map[x][y + 1].GetComponent<Piece>().isFlag()){
                flagsAround++;
            }
                    
            //Arriba derecha
            if (x < width - 1 && y < height - 1 && !map[x + 1][y + 1].GetComponent<Piece>().isFlag()){
               flagsAround++;
            }
                    
            // Izquierda
            if (x > 0 && !map[x - 1][y].GetComponent<Piece>().isFlag()){
                flagsAround++;
            }
                    
            // Derecha
            if (x < width - 1 && !map[x + 1][y].GetComponent<Piece>().isFlag()){
                flagsAround++;
            }
                    
            //Abajo Izquierda
            if (x > 0 && y > 0 && !map[x - 1][y - 1].GetComponent<Piece>().isFlag()){
                flagsAround++;
            }
                    
            //Abajo
            if (y > 0 && !map[x][y - 1].GetComponent<Piece>().isFlag()){
                flagsAround++;
            }
                    
            //Abajo Derecha
            if (x < width - 1 && y > 0 && !map[x + 1][y - 1].GetComponent<Piece>().isFlag()){
                flagsAround++;
            }



            if(flagsAround == numBombsAround){
                // Arriba izquierda
                if (x > 0 && y < height - 1)
                {
                    if (!map[x - 1][y + 1].GetComponent<Piece>().isCheck() && !map[x - 1][y + 1].GetComponent<Piece>().isFlag())
                    {
                        map[x - 1][y + 1].GetComponent<Piece>().OnLeftClick();
                        accion = true;
                    }
                }

                // Arriba
                if (y < height - 1)
                {
                    if (!map[x][y + 1].GetComponent<Piece>().isCheck() && !map[x][y + 1].GetComponent<Piece>().isFlag())
                    {
                        map[x][y + 1].GetComponent<Piece>().OnLeftClick();
                        accion = true;
                    }
                }

                //Arriba derecha
                if (x < width - 1 && y < height - 1)    
                {
                    if (!map[x + 1][y + 1].GetComponent<Piece>().isCheck() && !map[x + 1][y + 1].GetComponent<Piece>().isFlag())
                    {
                        map[x + 1][y + 1].GetComponent<Piece>().OnLeftClick();
                        accion = true;
                    }
                }

                // Izquierda
                if (x > 0)
                {
                    if (!map[x - 1][y].GetComponent<Piece>().isCheck() && !map[x - 1][y].GetComponent<Piece>().isFlag())
                    {
                        map[x - 1][y].GetComponent<Piece>().OnLeftClick();
                        accion = true;
                    }
                }

                // Derecha
                if (x < width - 1)
                {
                    if (!map[x + 1][y].GetComponent<Piece>().isCheck() && !map[x + 1][y].GetComponent<Piece>().isFlag())
                    {
                        map[x + 1][y].GetComponent<Piece>().OnLeftClick();
                        accion = true;
                    }
                }

                // Abajo izquierda
                if (x > 0 && y > 0)
                {
                    if (!map[x - 1][y - 1].GetComponent<Piece>().isCheck() && !map[x - 1][y - 1].GetComponent<Piece>().isFlag())
                    {
                        map[x - 1][y - 1].GetComponent<Piece>().OnLeftClick();
                        accion = true;
                    }
                }

                // Abajo
                if (y > 0)
                {
                    if (!map[x][y - 1].GetComponent<Piece>().isCheck() && !map[x][y - 1].GetComponent<Piece>().isFlag())
                    {
                        map[x][y - 1].GetComponent<Piece>().OnLeftClick();
                        accion = true;
                    }
                }

                // Abajo derecha
                if (x < width - 1 && y > 0)
                {
                    if (!map[x + 1][y - 1].GetComponent<Piece>().isCheck() && !map[x + 1][y - 1].GetComponent<Piece>().isFlag())
                    {
                        map[x + 1][y - 1].GetComponent<Piece>().OnLeftClick();
                        accion = true;
                    }
                }

            }
            
        

        return accion;
    }
}

