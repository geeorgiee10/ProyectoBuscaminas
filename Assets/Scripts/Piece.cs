using System;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class Piece : MonoBehaviour
{


    [SerializeField] private int x, y;
    [SerializeField] private bool bomb, check, isFlag;
    [SerializeField] private GameObject bombSprite, flagSprite;
    


    public void setX(int x)
    {
        this.x = x;
    }

    public void setY(int y)
    {
        this.y = y;
    }

    public void setBomb(bool bomb)
    {
        this.bomb = bomb;
    }

    public bool isBomb()
    {
        return bomb;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public void setCheck(bool v)
    {
        this.check = v;
    }

    public bool isCheck()
    {
        return check;
    }

    public void DrawBomb()
    {
        if (!isCheck() && !isFlag)
        {
            setCheck(true);

            if (isBomb())
            {
                GetComponent<SpriteRenderer>().material.color = Color.red;
                // Añadir Sprite de bomba
                bombSprite.SetActive(true);

                GameManager.instance.endMenu.SetActive(true);

                Generator.gen.ShowAllMap();

                GameManager.instance.endgame = true;
                
            }
            else
            {

                int bombsNumber = Generator.gen.GetBombsAround(x, y);

                if (bombsNumber != 0)
                {
                    transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = bombsNumber.ToString();
                }
                else
                {
                    // Cambiar color casilla porque ya esta comprobadas
                    GetComponent<SpriteRenderer>().material.color = Color.gray;
                    Generator.gen.CheckPieceAround(x, y);
                }

            }
        }


    }

    // Dibujar bandera con el boton secundario
    public void drawFlag()
    {
        if (isCheck())
            return;

        isFlag = !isFlag;
        flagSprite.SetActive(isFlag);
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.instance.endgame)
            {
                DrawBomb();
            }
            
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (!GameManager.instance.endgame)
            {
                drawFlag();
            }
            
        }
        
    }
    
    

}
