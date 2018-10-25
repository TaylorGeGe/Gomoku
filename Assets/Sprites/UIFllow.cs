using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFllow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ChessBoard.Instacne.chessStack.Count>0)
        {
            transform.position = ChessBoard.Instacne.chessStack.Peek().position;
        }
	}

    public void OnRelayBtn()
    {
        SceneManager.LoadScene(1);
    }
    public void ReturnBtn()
    {
        SceneManager.LoadScene(0);
    }
}
