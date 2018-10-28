using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptionScript : MonoBehaviour {

    public Text dialogueBox;

    public InitText(string caption)
    {
        dialogueBox.text = caption
    }

}
