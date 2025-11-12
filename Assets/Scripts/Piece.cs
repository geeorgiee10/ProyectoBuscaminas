using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Piece : MonoBehaviour, IPointerClickHandler
{
       
    
    [SerializeField] private int x, y;
    [SerializeField] private bool bomb, check;
    [SerializeField] private bool flag = false;
    public bool endgame;

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

    public bool isBomb() { 
        return bomb;
    }

    public int getX() {
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

    public bool isFlag()
    {
        return flag;
    }


    public void DrawBomb()
    {
        if (!isCheck())
        {
            setCheck(true);
            GameManager.instance.checkConuter++;

            if (isBomb())
            {

                GetComponent<SpriteRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f);
                transform.GetChild(1).gameObject.SetActive(true);
                if (!GameManager.instance.endgame)
                {
                    transform.GetChild(2).gameObject.SetActive(true);
                    GameManager.instance.endMenu.SetActive(true);
                    GameManager.instance.win = false;
                    GameManager.instance.EndGame();
                }
                    
                
            }
            else
            {
                // Cambiar color casilla porque ya est� comprobada
                GetComponent<SpriteRenderer>().material.color = new Color(0.9f, 0.9f, 0.9f);

                int bombsNumer = Generator.gen.GetBombsAround(x, y);


                if (bombsNumer != 0)
                {
                    transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = bombsNumer.ToString();
                }
                else
                {
                    Generator.gen.CheckPieceAround(x, y);
                }


                if (GameManager.instance.checkConuter == (Generator.gen.width * Generator.gen.height) - Generator.gen.bombsNumber)
                {
                    GameManager.instance.EndGame();
                }

            }
            

        } 
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.instance.endgame) return;

        if (!AIController.instance.isPlayerTurn)
            return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }

        AIController.instance.CambiarTurno();

    }



    // Dibujar bandera
    public void DrawFlag()
    {
        flag = !flag;

        transform.GetChild(3).gameObject.SetActive(!transform.GetChild(3).gameObject.activeSelf);
        if (flag)
        {
            GameManager.instance.flagCounter++;
        }
        else
        {
            GameManager.instance.flagCounter--;
        }
    }

    // Input Manager
    public void OnLeftClick()
    {
        DrawBomb();
    }


    public void OnRightClick()
    {
        if (!GameManager.instance.endgame)
            DrawFlag();
   }

    


    

}
