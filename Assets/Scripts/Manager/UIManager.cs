using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private Text centerText;
    private List<PlayerUI> playerUIList = new List<PlayerUI>();

    public void UpdateText(int playerNumber, UnitTypes unitType, int number, int injured = 0)
    {
        playerUIList[playerNumber].UpdateText(unitType, number, injured);
    }
}
