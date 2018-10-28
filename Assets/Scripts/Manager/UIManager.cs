using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject speechBubble;
    public GameObject movingCanvas;

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

    public void ResetText()
    {
        for(int i = 0; i < playerUIList.Count; ++i)
        {
            playerUIList[i].ResetText();
        }
    }

    public void CreateCaption()
    {
        GameObject bubble = Instantiate(speechBubble, movingCanvas.transform);

        StartCoroutine(DestroyBubble(bubble));
    }

    private IEnumerator DestroyBubble(GameObject bubble)
    {
        float duration = 1;
        while(duration > 0)
        {
            yield return null;
            duration -= Time.deltaTime;
        }

        Destroy(bubble);
    }
}
