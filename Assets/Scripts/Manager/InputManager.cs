using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IManager {

    private GameManager gameManager;
    private bool initComplete;

    public void Init()
    {
        gameManager = Toolbox.Instance.GetManager<GameManager>();
        initComplete = true;
    }

    private void Update()
    {
        if (!initComplete) return;
        if (!gameManager.inGame) return;
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            // Healer, player 1
            GameObject[] player1Array = GameObject.FindGameObjectsWithTag("Cat");
            foreach (GameObject cat in player1Array)
            {
                if (cat.GetComponent<CharMovement>().currentJob == UnitTypes.Free)
                {
                    cat.GetComponent<CharMovement>().changeJob(UnitTypes.Healer);
                    break;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Healer, player 2
            GameObject[] player1Array = GameObject.FindGameObjectsWithTag("Dog");
            foreach (GameObject cat in player1Array)
            {
                if (cat.GetComponent<CharMovement>().currentJob == UnitTypes.Free)
                {
                    cat.GetComponent<CharMovement>().changeJob(UnitTypes.Healer);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // Free, player 1
            GameObject[] player1Array = GameObject.FindGameObjectsWithTag("Cat");
            foreach (GameObject cat in player1Array)
            {
                if (cat.GetComponent<CharMovement>().currentJob != UnitTypes.Free)
                {
                    cat.GetComponent<CharMovement>().changeJob(UnitTypes.Free);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Free, player 2
            GameObject[] player1Array = GameObject.FindGameObjectsWithTag("Dog");
            foreach (GameObject cat in player1Array)
            {
                if (cat.GetComponent<CharMovement>().currentJob != UnitTypes.Free)
                {
                    cat.GetComponent<CharMovement>().changeJob(UnitTypes.Free);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // Harvester, player 1
            GameObject[] player1Array = GameObject.FindGameObjectsWithTag("Cat");
            foreach (GameObject cat in player1Array)
            {
                if (cat.GetComponent<CharMovement>().currentJob == UnitTypes.Free)
                {
                    cat.GetComponent<CharMovement>().changeJob(UnitTypes.Harvester);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Harvester, player 2
            GameObject[] player1Array = GameObject.FindGameObjectsWithTag("Dog");
            foreach (GameObject cat in player1Array)
            {
                if (cat.GetComponent<CharMovement>().currentJob == UnitTypes.Free)
                {
                    cat.GetComponent<CharMovement>().changeJob(UnitTypes.Harvester);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            // Striker, player 1
            GameObject[] player1Array = GameObject.FindGameObjectsWithTag("Cat");
            foreach (GameObject cat in player1Array)
            {
                if (cat.GetComponent<CharMovement>().currentJob == UnitTypes.Free)
                {
                    cat.GetComponent<CharMovement>().changeJob(UnitTypes.Attacker);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Striker, player 2
            GameObject[] player1Array = GameObject.FindGameObjectsWithTag("Dog");
            foreach (GameObject cat in player1Array)
            {
                if (cat.GetComponent<CharMovement>().currentJob == UnitTypes.Free)
                {
                    cat.GetComponent<CharMovement>().changeJob(UnitTypes.Attacker);
                    break;
                }
            }
        }
    }
}
