using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AIController : MonoBehaviour
{
    // Declaración de variables (necesitarás algunas más)
    public float turnTime = 0.5f;



    // El Bot comienza a Jugar. Este Código no hay que cambiarlo
    void Start()
    {
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
           
            // Para cada casilla comprobada   

                // Regla 1: todas ocultas son minas: click_derecho (Flag) 
            

                // Regla 2: todas ocultas son seguras: clic_izquierdo (Flag)
            
        

        return action;
    }

    void RandomPlay()
    {
        // De todas las celdas que no se han chequeado, click_izquierdo en una de forma aleatoria;
    }
}

