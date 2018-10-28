using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Camera camera;
    public GameObject speechBubble;
    public GameObject movingCanvas;

    [SerializeField] private List<PlayerUI> playerUIList = new List<PlayerUI>();
    private Text centerText;

    public void CenterTextUpdate(string value)
    {
        centerText.text = value;
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

    public void CreateCaption(string captionString, Transform unit)
    {
        CaptionScript caption = Instantiate(speechBubble, movingCanvas.transform).GetComponent<CaptionScript>();

        caption.InitText(captionString, unit, camera);

        StartCoroutine(DestroyBubble(caption.gameObject));
    }

    private IEnumerator DestroyBubble(GameObject bubble)
    {
        float duration = 2f;
        while(duration > 0)
        {
            yield return null;
            duration -= Time.deltaTime;
        }

        Destroy(bubble);
    }
}
