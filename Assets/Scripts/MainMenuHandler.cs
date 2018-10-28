using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public Text mainText;
    float blinkingTmr = 0;
    bool toggle = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        blinkingTmr += Time.deltaTime;
        if (blinkingTmr > 1)
        {
            blinkingTmr = 0;
            toggle = !toggle;
        }

        if (toggle) { mainText.text = "Press SPACEBAR to Start"; }
        else { mainText.text = ""; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MasterScene");
        }
    }
}
