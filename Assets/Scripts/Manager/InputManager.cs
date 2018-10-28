using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (Input.GetKeyDown(KeyCode.A))
        {
            List<CharMovement> gameObjectArray = gameManager.catObjects;
            for (int i = 0; i < gameObjectArray.Count; ++i)
            {
                if (gameObjectArray[i].currentJob == UnitTypes.Free)
                {
                    gameObjectArray[i].changeJob(UnitTypes.Healer);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            List<CharMovement> gameObjectArray = gameManager.dogObjects;
            for (int i = 0; i < gameObjectArray.Count; ++i)
            {
                if (gameObjectArray[i].currentJob == UnitTypes.Free)
                {
                    gameObjectArray[i].changeJob(UnitTypes.Healer);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            List<CharMovement> gameObjectArray = gameManager.catObjects;
            for (int i = 0; i < gameObjectArray.Count; ++i)
            {
                if (gameObjectArray[i].currentJob != UnitTypes.Free)
                {
                    gameObjectArray[i].changeJob(UnitTypes.Free);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            List<CharMovement> gameObjectArray = gameManager.dogObjects;
            for (int i = 0; i < gameObjectArray.Count; ++i)
            {
                if (gameObjectArray[i].currentJob != UnitTypes.Free)
                {
                    gameObjectArray[i].changeJob(UnitTypes.Free);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            List<CharMovement> gameObjectArray = gameManager.catObjects;
            for (int i = 0; i < gameObjectArray.Count; ++i)
            {
                if (gameObjectArray[i].currentJob == UnitTypes.Free)
                {
                    gameObjectArray[i].changeJob(UnitTypes.Harvester);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            List<CharMovement> gameObjectArray = gameManager.dogObjects;
            for (int i = 0; i < gameObjectArray.Count; ++i)
            {
                if (gameObjectArray[i].currentJob == UnitTypes.Free)
                {
                    gameObjectArray[i].changeJob(UnitTypes.Harvester);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            List<CharMovement> gameObjectArray = gameManager.catObjects;
            for (int i = 0; i < gameObjectArray.Count; ++i)
            {
                if (gameObjectArray[i].currentJob == UnitTypes.Free && gameObjectArray[i].hp >= 20)
                {
                    gameObjectArray[i].changeJob(UnitTypes.Attacker);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            List<CharMovement> gameObjectArray = gameManager.dogObjects;
            for (int i = 0; i < gameObjectArray.Count; ++i)
            {
                if (gameObjectArray[i].currentJob == UnitTypes.Free && gameObjectArray[i].hp >= 20)
                {
                    gameObjectArray[i].changeJob(UnitTypes.Attacker);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gameManager.RestartGame();
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.inGame == false)
        {
            SceneManager.LoadScene("MenuScene");
        }*/
    }
}
