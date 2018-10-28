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

    private float animationScale = 1.3f;
    private float animationDuration = 0.3f;
    private IEnumerator[] textAnimationCoroutine = new IEnumerator[5];

    public void ResetText()
    {
        attackerTxt.text = "";
        harvesterTxt.text = "";
        healerTxt.text = "";
        freeTxt.text = "";
    }

    public void UpdateText(UnitTypes unitType, int number ,int injured = 0)
    {
        string injuredText = (injured <= 0) ? "" : (" (" + injured + ")");
        Text targetText = null;
        string targetString = "";
        switch(unitType)
        {
            case UnitTypes.Attacker:
                targetText = attackerTxt;
                break;

            case UnitTypes.Harvester:
                targetText = harvesterTxt;
                break;

            case UnitTypes.Healer:
                targetText = healerTxt;
                break;

            case UnitTypes.Free:
                targetText = freeTxt;
                break;

            case UnitTypes.Total:
                targetText = totalTxt;
                targetString = "Total: ";
                return;
        }

        targetText.text = targetString + number + injuredText;
        if (textAnimationCoroutine[(int) unitType] != null) { StopCoroutine(textAnimationCoroutine[(int) unitType]); }
        textAnimationCoroutine[(int) unitType] = SizeAnimation(targetText.transform);
        StartCoroutine(textAnimationCoroutine[(int) unitType]);
    }
    
    private IEnumerator SizeAnimation(Transform textbox)
    {
        Debug.Log("Animating");

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
