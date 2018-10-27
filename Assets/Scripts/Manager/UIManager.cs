﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    //public Text player1point, player2point;
    private Text centerText;
    private List<PlayerUI> playerUIList = new List<PlayerUI>();

    public void CenterTextUpdate(string value)
    {
        centerText.text = value;
    }

    public void UpdateText(int playerNumber, UnitTypes unitType, int number, int injured = 0)
    {
        playerUIList[playerNumber].UpdateText(unitType, number, injured);
    }
}
