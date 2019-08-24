using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridSpace : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    Controller controller;

    public void SetGameControllerRef(Controller controller)
    {
        this.controller = controller;
    }

    public void SetSpace()
    {
        buttonText.text = controller.GetPlayerSide();
        button.interactable = false;
        controller.EndTurn();      
    }

}
