using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public  Button button;
    bool isDobleMode = false;
    public ChessType chessColore = ChessType.Black;
    protected ChessBoard instances = null;
    protected virtual void Start()
    {
        instances=ChessBoard.Instacne;
        button = GameObject.Find("RetractBtn").GetComponent<Button>();
        if (PlayerPrefs.GetInt("Double")==10)
        {
            isDobleMode = true; 
        }
    }
    protected virtual  void FixedUpdate()
    { 
        if (chessColore== ChessBoard.Instacne.turn&& ChessBoard.Instacne.timer >0.3f)
         PlayChess();
        if (!isDobleMode)
        {
            ChangeBtnColor(); 
        }
        
    }
    public virtual void PlayChess()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 pos =   Camera.main.ScreenToWorldPoint( Input.mousePosition); 
             // print((int)(pos.x + 7.5f) +"  "+ (int)(pos.y + 7.5f));
            instances.PlayChess(new int[2] { (int)(pos.x + 7.5f), (int)(pos.y + 7.5f) });
            instances.timer = 0;
        }
    }

  protected virtual  void ChangeBtnColor()
    {
        if (chessColore == ChessType.Watch)
        {
            return;
        }
        if (ChessBoard.Instacne.turn == chessColore)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }

    }
}
