using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private bool initComplete;

    public void Init()
    {
        initComplete = true;
    }

    private void Update()
    {
        if (!initComplete) return;
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            // Healer, player 1
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Healer, player 2
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // Free, player 1
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Free, player 2
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // Harvester, player 1
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Harvester, player 2
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            // Striker, player 1
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Striker, player 2
        }
    }
}
