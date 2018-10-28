using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestTimerScript : MonoBehaviour {

    public Image radialFill;
    
    public void Fill(float percentage)
    {
        radialFill.fillAmount = percentage;
    }
        
}
