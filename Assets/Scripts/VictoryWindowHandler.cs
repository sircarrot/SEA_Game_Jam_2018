using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryWindowHandler : MonoBehaviour
{
    public Text winningText;
    public Text mainText;
    float blinkingTmr = 0;
    bool toggle = false;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        blinkingTmr += Time.deltaTime;
        if (blinkingTmr > 1)
        {
            blinkingTmr = 0;
            toggle = !toggle;
        }

        if (toggle) { mainText.text = "Press SPACEBAR to Start"; }
        else { mainText.text = ""; }
    }

    public void TextUpdate(string value)
    {
        winningText.text = value;
    }
}
