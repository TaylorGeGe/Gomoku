using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<Player> player = new List<Player>();

    private void Awake()
    {
        PlayerPrefs.SetInt("Double", 1);
        int player1 = PlayerPrefs.GetInt("player1" ); //黑棋 
        int player2= PlayerPrefs.GetInt("player2" );//白旗
        for (int i = 0; i < player.Count; i++)
        {
            if (player1 ==i)
            {
                player[i].chessColore = ChessType.Black;
               
            }
            else if(player2 == i)
            {
                player[i].chessColore = ChessType.White;  
            }
            else
            {
                player[i].chessColore = ChessType.Watch;
            }
        }
    }

    public  void SetPlayer(int i)
    {
        PlayerPrefs.SetInt("player1", i);
    }
    public void SetPlayer2(int i)
    {
        PlayerPrefs.SetInt("player2", i);
    }

    public void PlayeGame()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayeNetGame()
    {
        SceneManager.LoadScene(2);
    }
    public void ChessChangeColor()
    {
        for (int i = 0; i < player.Count; i++)
        {
            if (player[i].chessColore == ChessType.Black)
            {
                SetPlayer2(i);
            }
            else if (player[i].chessColore == ChessType.White)
            {
                SetPlayer(i);
            }
            else
            {
                player[i].chessColore = ChessType.Watch;
            }
        }
        SceneManager.LoadScene(1);
    }

    public void DoubleMode()
    {
        PlayerPrefs.SetInt("Double", 10);
    }
    public void OnReturn()
    {
        PlayerPrefs.SetInt("Double",1);
        SceneManager.LoadScene(0);
    }
}
