using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public List<PlayerUI> playerUIList = new List<PlayerUI>();

    public void TestFunction()
    {
        playerUIList[0].UpdateText(UnitTypes.Attacker, 10, 5);        
    }
}
