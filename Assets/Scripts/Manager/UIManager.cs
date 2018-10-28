using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private List<PlayerUI> playerUIList = new List<PlayerUI>();

    [SerializeField] private List<Text> playerPoints = new List<Text>();
    private Text centerText;

    public void CenterTextUpdate(string value)
    {
        centerText.text = value;
    }

    public void HarvestPointUpdate(int playerNumber, int harvestPoints)
    {
        playerPoints[playerNumber].text = "Harvest: " + harvestPoints.ToString();
    }

    public void UpdateText(int playerNumber, UnitTypes unitType, int number, int injured = 0)
    {
        playerUIList[playerNumber].UpdateText(unitType, number, injured);
    }
}
