using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Text[] textButton;

    string playerSide;// x or o
    bool isPlayerTurn;
    int moveCount;

    GameObject ChoosePanel;
    GameObject DifficultyPanel;
    GameObject ResultPanel;

    Text Turn;

    Ai ai;
    Levels levels;

    void Awake()
    {
        ChoosePanel = GameObject.Find("Choose Panel");
        DifficultyPanel = GameObject.Find("Difficulty Panel");
        ResultPanel = GameObject.Find("Result Panel");      
    }

    void Start()
    {
        SetGameControllerRefOnButton();
        foreach(var item in textButton)
        {
            item.text = string.Empty;
        }
        isPlayerTurn = true;
        moveCount = 0;
    }

    void SetGameControllerRefOnButton()
    {
        for(int i = 0; i < textButton.Length; i++)
        {
            textButton[i].GetComponentInParent<GridSpace>().SetGameControllerRef(this);
        }
    }

    public void EndTurn()
    {        
        moveCount++;
        if (CheckStute())
            return;
        ChangeSide();
        if (!isPlayerTurn)
        {
            Vector2 tmpPos = ai.FindBestMove(textButton);
            int pos = ConvertToOneD(tmpPos);
            textButton[pos].text = playerSide;
            textButton[pos].GetComponentInParent<Button>().interactable = false;
            EndTurn();
        }
    }

    private int ConvertToOneD(Vector2 vector)
    {
        if (vector.x == 0 && vector.y == 0)
            return 0;
        else if (vector.x == 0 && vector.y == 1)
            return 1;
        else if (vector.x == 0 && vector.y == 2)
            return 2;
        else if (vector.x == 1 && vector.y == 0)
            return 3;
        else if (vector.x == 1 && vector.y == 1)
            return 4;
        else if (vector.x == 1 && vector.y == 2)
            return 5;
        else if (vector.x == 2 && vector.y == 0)
            return 6;
        else if (vector.x == 2 && vector.y == 1)
            return 7;
        else if (vector.x == 2 && vector.y == 2)
            return 8;
        else
            return -1;
    }

    bool CheckStute()
    {        
        if (WinInDiameters() || WinInRows() || WinInColumns())
        {
            StartCoroutine(GameOver(1));
            return true;
        }           
        if (moveCount == 9)// It's Draw
        {
            StartCoroutine(GameOver(0));
            return true;
        }
        return false;    
    }

    IEnumerator GameOver(int No)
    {
        yield return new WaitForSeconds(1);
        ResultPanel.SetActive(true);
        for (int i = 0; i < textButton.Length; i++)
        {
            textButton[i].GetComponentInParent<Button>().interactable = false;
        }
        if (No == 1)
        {
            ResultPanel.GetComponentInChildren<Text>().text = playerSide + " WINNER";
        }
        else if (No == 0)
        {
            ResultPanel.GetComponentInChildren<Text>().text = "DRAW";
        }      
    }


    void ChangeSide()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        isPlayerTurn = (isPlayerTurn == true) ? false : true;
    }

    bool WinInRows()
    {
        if (textButton[0].text == playerSide && textButton[1].text == playerSide && textButton[2].text == playerSide)
            return true;
        if (textButton[3].text == playerSide && textButton[4].text == playerSide && textButton[5].text == playerSide)
            return true;
        if (textButton[6].text == playerSide && textButton[7].text == playerSide && textButton[8].text == playerSide)
            return true;
        return false;
    }
    bool WinInColumns()
    {
        if (textButton[0].text == playerSide && textButton[3].text == playerSide && textButton[6].text == playerSide)
            return true;
        if (textButton[1].text == playerSide && textButton[4].text == playerSide && textButton[7].text == playerSide)
            return true;
        if (textButton[2].text == playerSide && textButton[5].text == playerSide && textButton[8].text == playerSide)
            return true;
        return false;
    }
    bool WinInDiameters()
    {
        if (textButton[0].text == playerSide && textButton[4].text == playerSide && textButton[8].text == playerSide)
            return true;
        else if (textButton[2].text == playerSide && textButton[4].text == playerSide && textButton[6].text == playerSide)
            return true;
        return false;
    }
    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void ChooseX()
    {
        playerSide = "X";
        ChoosePanel.SetActive(false);
        ResultPanel.SetActive(false);
        ai = new Ai('X', 'O',levels);
    }
    public void ChooseO()
    {
        playerSide = "O";
        ChoosePanel.SetActive(false);
        ResultPanel.SetActive(false);
        ai = new Ai('O', 'X',levels);
    }

    public void Easy()
    {
        levels = Levels.Easy;
        DifficultyPanel.SetActive(false);
    }

    public void Medium()
    {
        levels = Levels.Medium;
        DifficultyPanel.SetActive(false);
    }

    public void Hard()
    {
        levels = Levels.Hard;
        DifficultyPanel.SetActive(false);
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu",LoadSceneMode.Single);
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
}
