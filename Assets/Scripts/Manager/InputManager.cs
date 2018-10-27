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
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Cat");
            for(int i = 0; i < gameObjectArray.Length; ++i)
            {
                CharMovement charMovement = gameObjectArray[i].GetComponent<CharMovement>();
                if (charMovement.currentJob == UnitTypes.Free)
                {
                    charMovement.changeJob(UnitTypes.Healer);
                    break;
                }
            }


        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Healer, player 2
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Dog");
            for (int i = 0; i < gameObjectArray.Length; ++i)
            {
                CharMovement charMovement = gameObjectArray[i].GetComponent<CharMovement>();

                if (charMovement.currentJob == UnitTypes.Free)
                {
                    charMovement.changeJob(UnitTypes.Healer);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // Free, player 1
            GameObject[] gameObjectArray= GameObject.FindGameObjectsWithTag("Cat");
            for (int i = 0; i < gameObjectArray.Length; ++i)
            {
                CharMovement charMovement = gameObjectArray[i].GetComponent<CharMovement>();

                if (charMovement.currentJob != UnitTypes.Free)
                {
                    charMovement.changeJob(UnitTypes.Free);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Free, player 2
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Dog");
            for (int i = 0; i < gameObjectArray.Length; ++i)
            {
                CharMovement charMovement = gameObjectArray[i].GetComponent<CharMovement>();

                if (charMovement.currentJob != UnitTypes.Free)
                {
                    charMovement.changeJob(UnitTypes.Free);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            // Harvester, player 1
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Cat");
            for (int i = 0; i < gameObjectArray.Length; ++i)
            {
                CharMovement charMovement = gameObjectArray[i].GetComponent<CharMovement>();

                if (charMovement.currentJob == UnitTypes.Free)
                {
                    charMovement.changeJob(UnitTypes.Harvester);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Harvester, player 2
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Dog");
            for (int i = 0; i < gameObjectArray.Length; ++i)
            {
                CharMovement charMovement = gameObjectArray[i].GetComponent<CharMovement>();

                if (charMovement.currentJob == UnitTypes.Free)
                {
                    charMovement.changeJob(UnitTypes.Harvester);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            // Striker, player 1
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Cat");
            for (int i = 0; i < gameObjectArray.Length; ++i)
            {
                CharMovement charMovement = gameObjectArray[i].GetComponent<CharMovement>();

                if (charMovement.currentJob == UnitTypes.Free)
                {
                    charMovement.changeJob(UnitTypes.Attacker);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Striker, player 2
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Dog");
            for (int i = 0; i < gameObjectArray.Length; ++i)
            {
                CharMovement charMovement = gameObjectArray[i].GetComponent<CharMovement>();

                if (charMovement.currentJob == UnitTypes.Free)
                {
                    charMovement.changeJob(UnitTypes.Attacker);
                    break;
                }
            }
        }
    }
}
