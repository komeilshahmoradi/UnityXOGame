
using UnityEngine;
using UnityEngine.UI;

public class Ai
{
    char human, bot;
    char[,] board;
    int MaxDepth;
    public Ai(char human,char bot,Levels levels)
    {
        this.human = human;
        this.bot = bot;
        board = new char[3, 3];

        if (levels == Levels.Easy)
            MaxDepth = 1;
        else if (levels == Levels.Medium)
            MaxDepth = 2;
        else if (levels == Levels.Hard)
            MaxDepth = 3;
    }

    public Vector2 FindBestMove(Text[] textButton)
    {
        SetBoardAndAvi(textButton);
        int bestVal = -1000;
        Vector2 bestMove = new Vector2(-1, -1);
        
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (board[i, j].Equals('-'))
                {
                    board[i, j] = bot;

                    int moveVal = MinMax(board, 0, false);

                    board[i, j] = '-';

                    if(moveVal > bestVal)
                    {
                        bestMove.x = i;
                        bestMove.y = j;
                        bestVal = moveVal;
                    }
                }
            }
        }
        return bestMove;
    }

    private int MinMax(char[,] board,int depth,bool isBot)
    {
        int score = Evaluate(board);
        if (score == 10 || score == -10)
            return score;
        if (!IsMoveLeft(board))
            return 0;
        if (depth == MaxDepth)
            return score;
        if (isBot)
        {
            int best = -1000;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (board[i, j].Equals('-'))
                    {
                        board[i, j] = bot;
                        best = Mathf.Max(best, MinMax(board, depth + 1, !isBot));
                        board[i, j] = '-';
                    }
                }
            }
            return best;
        }
        else
        {
            int best = 1000;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (board[i, j].Equals('-'))
                    {
                        board[i, j] = human;
                        best = Mathf.Min(best, MinMax(board, depth + 1, !isBot));
                        board[i, j] = '-';
                    }
                }
            }
            return best;
        }
    }

    private bool IsMoveLeft(char[,] board)
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (board[i, j].Equals('-'))
                    return true;
            }
        }
        return false;
    }

    private int Evaluate(char[,] board)
    {
        for(int row = 0; row < 3; row++)
        {
            if(board[row,0] == board[row,1] && board[row, 1] == board[row, 2])
            {
                if (board[row, 0] == bot)
                    return +10;
                else if(board[row,0] == human)
                    return -10;
            }
        }

        for(int col = 0; col < 3; col++)
        {
            if(board[0,col] == board[1,col] && board[1,col] == board[2, col])
            {
                if (board[0, col] == bot)
                    return +10;
                else if (board[0, col] == human)
                    return -10;
            }
        }

        if(board[0,0] == board[1,1] & board[1,1] == board[2, 2])
        {
            if (board[0, 0] == bot)
                return 10;
            else if (board[0, 0] == human)
                return -10;
        }

        if(board[0,2] == board[1,1] && board[1,1] == board[2, 0])
        {
            if (board[0, 2] == bot)
                return 10;
            else if (board[0, 2] == human)
                return -10;
        }
        return 0;
    }

    private void SetBoardAndAvi(Text[] textButton)
    {
        int index = -1;
        
        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                index += 1;
                if (textButton[index].text.Equals(string.Empty))
                {                   
                    board[i, j] = '-';
                }
                else
                {                  
                    board[i, j] = char.Parse(textButton[index].text);
                }
            }
        }
    }
}
