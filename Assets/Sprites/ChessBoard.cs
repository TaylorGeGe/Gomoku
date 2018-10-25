using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChessBoard : MonoBehaviour {

   private static ChessBoard _instance;
    public static ChessBoard Instacne
    {
        get
        {
            return _instance;
        }

    }
    //到谁下棋
    public ChessType turn = ChessType.Black;

    //棋盘坐标
    public int[,] grid;

    //黑白棋子
    public GameObject[] prefabs;

    //等待思考时间
    public float timer = 0;

    //是否可以下棋
    public  bool gameStart = false;
    Transform parent;
    /// <summary>
    /// 游戏悔棋操作的栈
    /// </summary>
   public   Stack<Transform> chessStack = new Stack<Transform>();

    public Text GameoverText;
  
    private void Awake()
    {
        _instance = this; 
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    private void Start()
    {
        grid = new int[15, 15];

        parent = GameObject.Find("Parent").transform;
    }
    //开始下棋
    public bool PlayChess(int[] pos)
    {
        //gameStart等于fasel 就不让他下棋 游戏结束
        if (!gameStart) return false ;
        pos[0] = Mathf.Clamp(pos[0], 0, 14);
        pos[1] = Mathf.Clamp(pos[1], 0, 14);

        if (grid[pos[0], pos[1]] != 0) return false;
        if (turn==ChessType.Black)
        {
          GameObject go=  Instantiate(prefabs[0], new Vector3(pos[0]-7, pos[1]-7, 0), Quaternion.identity,parent);
            chessStack.Push(go.transform);
            grid[pos[0], pos[1]] = 1;
            //判断是否胜利
           if (ChessWinner(pos))
            {
                GameEnd();
            }
            turn = ChessType.White;
             
        }
        else if(turn==ChessType.White)
        {
          GameObject go=  Instantiate(prefabs[1], new Vector3(pos[0] - 7, pos[1] - 7, 0), Quaternion.identity, parent);
            chessStack.Push(go.transform);
            grid[pos[0], pos[1]] = 2;
            //判断是否胜利
            if (ChessWinner(pos))
            {
                GameEnd();
            }
            turn = ChessType.Black; 
        }
        return true; 
    }
  void GameEnd()
    {
        //Debug.Log("游戏结束"+turn +"硬了");
        GameoverText.transform.parent.gameObject.SetActive(true);
        switch (turn)
        {
            case ChessType.Watch:
                break;
            case ChessType.Black:
                GameoverText.text = "黑棋胜！";
                break;
            case ChessType.White:
                GameoverText.text = "白棋胜！";
                break;
            default:
                break;
        }
        gameStart = false;
    }

    public bool ChessWinner(int [] pos)
    {
        if (CheckLine(pos, new int[2] { 1, 0 })) return true;
        if (CheckLine(pos, new int[2] { 0, 1 })) return true;
        if (CheckLine(pos, new int[2] { 1, 1 })) return true;
        if (CheckLine(pos, new int[2] { 1, -1 })) return true;

        return false;
    }
    public bool CheckLine(int[] pos, int[] offset)
    { 
        int linkNum = 1;
        //右边
        for (int i = offset[0], j = offset[1]; (pos[0] + i >= 0 && pos[0] +i< 15)
            && (pos[1] + j >= 0 && pos[1] +j< 15);
            i += offset[0], j += offset[1])
        {
            if (grid[pos[0] + i, pos[1] + j] == (int)turn)
            {
                linkNum++;
            }
            else
            {
                break;
            }
        }
        //左边
        for (int i = -offset[0], j = -offset[1]; (pos[0] + i >= 0 && pos[0]+i < 15)
          && (pos[1] + j >= 0 && pos[1] +j< 15);
          i -= offset[0], j -= offset[1])
        {
            if (grid[pos[0] + i, pos[1] + j] == (int)turn)
            {
                linkNum++;
            }
            else
            {
                break;
            }
        } 
        if (linkNum > 4) return true; 
        return false;
    }

    public void RetractChess()
    {
        if (chessStack.Count>1)
        {
            Transform go1 = chessStack.Pop();
            Transform go2 = chessStack.Pop(); 
            grid[(int)(go1.position.x + 7), (int)(go1.position.y + 7)] = 0;
            grid[(int)(go2.position.x + 7), (int)(go2.position.y + 7)] = 0;
            Destroy(go1.gameObject);
            Destroy(go2.gameObject);
        }
       
    }
}
public enum ChessType
{
    Watch,
    Black,
    White
}