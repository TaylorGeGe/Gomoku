using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AiLevelOne : Player
{
    protected Dictionary<string, float> toScore = new Dictionary<string, float>();
    protected float[,] score = new float[15,15]; //评分数组
    protected ChessBoard chessBoard = null;

     
  public virtual void Awake()
    {
        chessBoard = ChessBoard.Instacne;
    }
    protected override void ChangeBtnColor()
    {
    }
    protected override void Start()
    { 
        //下一个子  就会用 a来表示
        //沿着这个子遍历   如果碰到了颜色一样的棋子 就多一个a  （aa） 
        //如果碰到异 子 直接返回不扫了   接着网右边扫  如果碰到空棋子 就加一个_ 然后也不会扫， 

        toScore.Add("_aa_",100);
        toScore.Add("aa_",50);
        toScore.Add("_aa",50);

        toScore.Add("_aaa_", 1000);
        toScore.Add("_aaa", 500);
        toScore.Add("aaa_", 500);

        toScore.Add("_aaaa_", 10000);
        toScore.Add("_aaaa", 5000);
        toScore.Add("aaaa_", 5000);

        toScore.Add("aaaaa", float.MaxValue);
        toScore.Add("_aaaaa", float.MaxValue);
        toScore.Add("aaaaa_", float.MaxValue);
        toScore.Add("_aaaaa_", float.MaxValue);

    }

     
 //检查一行
    public virtual  void  CheckOneLine(int[] pos, int[] offset ,int chess)
    { 
        //已经下一个子了
        string str = "a";
        //右边
        for (int i = offset[0], j = offset[1]; (pos[0] + i >= 0 && pos[0] + i < 15)    && (pos[1] + j >= 0 && pos[1]+j < 15);
            i += offset[0], j += offset[1])
        {
            //右边找到相同棋子
            if (chessBoard.grid[pos[0] + i, pos[1] + j] == chess)
            {
                str += "a";
            }
            //碰到 没有棋子
            else if(chessBoard.grid[pos[0] + i, pos[1] + j] == 0)
            {
                str += "_";
                break;
            }
            //碰到异子棋
            else
            {
                break;
            }
        }
        //左边
        for (int i = -offset[0], j = -offset[1]; (pos[0] + i >= 0 && pos[0] + i < 15)
          && (pos[1] + j >= 0 && pos[1] +j< 15);
          i -= offset[0], j -= offset[1])
        {
            //右边找到相同棋子
            if (chessBoard.grid[pos[0] + i, pos[1] + j] == chess)
            {
                str = "a"+str;
            }
            //碰到 没有棋子
            else if (chessBoard.grid[pos[0] + i, pos[1] + j] == 0)
            {
                str = "_" +str;
                break;
            }
            //碰到异子棋
            else
            {
                break;
            }
        }

        if (toScore.ContainsKey(str))
        {
            score[pos[0], pos[1]] = toScore[str];
        } 
    }

    public void SetScore(int [] pos)
    {
        score[pos[0], pos[1]] = 0;

        CheckOneLine(pos, new int[2] { 1, 0 }, 1);
        CheckOneLine(pos, new int[2] { 1, 1 }, 1);
        CheckOneLine(pos, new int[2] { 1, -1 }, 1);
        CheckOneLine(pos, new int[2] { 0, 1 }, 1);

        CheckOneLine(pos, new int[2] { 1, 0 }, 2);
        CheckOneLine(pos, new int[2] { 1, 1 }, 2);
        CheckOneLine(pos, new int[2] { 1, -1 }, 2);
        CheckOneLine(pos, new int[2] { 0, 1 }, 2); 
    }
    /// <summary>
    /// AI先下棋
    /// </summary>
    public override void PlayChess()
    {
        if (ChessBoard.Instacne.chessStack.Count == 0)
        {
            if (ChessBoard.Instacne.PlayChess(new int[2] { 7, 7 }))
                ChessBoard.Instacne.timer = 0;
            return;
        }

        float maxScore = 0;
        int[] maxPos = new int[2] { 0, 0 };
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (ChessBoard.Instacne.grid[i, j] == 0)
                {
                    SetScore(new int[2] { i, j });
                    if (score[i, j] >= maxScore)
                    {
                        maxPos[0] = i;
                        maxPos[1] = j;
                        maxScore = score[i, j];
                    }
                }
            }
        }

        if (ChessBoard.Instacne.PlayChess(maxPos))
            ChessBoard.Instacne.timer = 0;
    }
}
