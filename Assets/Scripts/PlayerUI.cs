using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public Text attackerTxt;
    public Text harvesterTxt;
    public Text healerTxt;
    public Text freeTxt;
    public Text totalTxt;

    public int attackerNum = -1;
    public int harvesterNum = -1;
    public int healerNum = -1;
    public int freeNum = -1;

    private float animationScale = 1.3f;
    private float animationDuration = 0.3f;
    private IEnumerator[] textAnimationCoroutine = new IEnumerator[5];

    public void ResetText()
    {
        attackerTxt.text = "0";
        harvesterTxt.text = "0";
        healerTxt.text = "0";
        freeTxt.text = "0";
    }

    public void UpdateText(UnitTypes unitType, int number ,int injured = 0)
    {
        injured = 0; // Hard coded to never show
        string injuredText = (injured <= 0) ? "" : (" (" + injured + ")");
        Text targetText = null;
        string targetString = "";
        switch(unitType)
        {
            case UnitTypes.Attacker:
                targetText = attackerTxt;
                if (attackerNum == number) return;
                attackerNum = number;
                break;

            case UnitTypes.Harvester:
                targetText = harvesterTxt;
                if (harvesterNum == number) return;
                harvesterNum = number;
                break;

            case UnitTypes.Healer:
                targetText = healerTxt;
                if (healerNum == number) return;
                healerNum = number;
                break;

            case UnitTypes.Free:
                targetText = freeTxt;
                if (freeNum == number) return;
                freeNum = number;
                break;

            case UnitTypes.Total:
                //targetText = totalTxt;
                //targetString = "";
                return;
        }

        targetText.text = targetString + number + injuredText;
        if (textAnimationCoroutine[(int) unitType] != null) { StopCoroutine(textAnimationCoroutine[(int) unitType]); }
        textAnimationCoroutine[(int) unitType] = SizeAnimation(targetText.transform);
        StartCoroutine(textAnimationCoroutine[(int) unitType]);
    }
    
    private IEnumerator SizeAnimation(Transform textbox)
    {
        float timer = animationDuration;
        textbox.localScale = new Vector3(animationScale, animationScale, 1);

        while (timer > 0)
        {
            float calcScale = textbox.localScale.x - ((animationScale - 1f) * Time.deltaTime / animationDuration);
            //Debug.Log(calcScale);
            textbox.localScale = new Vector3(calcScale, calcScale, 1);
            yield return null;
            timer -= Time.deltaTime;
        }
        textbox.localScale = new Vector3(1,1,1);

    }


}
