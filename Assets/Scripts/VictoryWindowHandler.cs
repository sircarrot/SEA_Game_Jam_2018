using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryWindowHandler : MonoBehaviour
{
    public Text winningText;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void TextUpdate(string value)
    {
        winningText.text = value;
    }
}
