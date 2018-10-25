using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetPlayer : NetworkBehaviour {
    public Button button;
    [SyncVar]
    public ChessType chessColore = ChessType.Black;
    protected NetChessBoard instances = null;
    void Start()
    {

        instances = NetChessBoard.Instacne;

        if (isLocalPlayer)
        {
            CmdSetPlayer();
        }
        if (chessColore == ChessType.Watch)
        {
            return;
        }
        button = GameObject.Find("RetractBtn").GetComponent<Button>();
        button.onClick.AddListener(RetractBtn);
      //  Debug.Log(Network.player.ipAddress); 192.168.2.102
 

    }
    protected void FixedUpdate()
    {
        if (chessColore == instances.turn && instances.timer > 0.3f && isLocalPlayer &&instances.playerNumber>1)
            PlayChess();
        if (chessColore != ChessType.Watch && isLocalPlayer && instances.gameStart == false)
        {
            instances.GameEnd();
        }
        if (chessColore != ChessType.Watch && isLocalPlayer)
            ChangeButColor();
    }
    public void PlayChess()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // print((int)(pos.x + 7.5f) +"  "+ (int)(pos.y + 7.5f));
            CmdChess(pos);
        }
    }

    //在客户端发出指令 在服务端调用
    [Command]
    public void CmdChess(Vector2 pos)
    {
        instances.PlayChess(new int[2] { (int)(pos.x + 7.5f), (int)(pos.y + 7.5f) });
        instances.timer = 0;
    }
    [Command]
    public void CmdSetPlayer()
    {
        instances.playerNumber++;
        if (instances.playerNumber == 1)
        {
            chessColore = ChessType.Black;
        }
        else if (instances.playerNumber == 2)
        {
            chessColore = ChessType.White;
        }
        else
        {
            chessColore = ChessType.Watch;
        }
    }

    void ChangeButColor()
    {
        if (chessColore==ChessType.Watch)
        {
            return;
        }
        if (instances.turn==chessColore)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void RetractBtn ()
    { 
        CmdRetractButton();
    }

    [Command]
    public void CmdRetractButton()
    {
        instances.RetractChess();
    }
}
