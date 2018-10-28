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
            ChangeJob(gameManager.dogObjects, UnitTypes.Healer);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeJob(gameManager.dogObjects, UnitTypes.Healer);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeJob(gameManager.dogObjects, UnitTypes.Free);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeJob(gameManager.dogObjects, UnitTypes.Free);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeJob(gameManager.catObjects, UnitTypes.Harvester);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeJob(gameManager.dogObjects, UnitTypes.Harvester);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeJob(gameManager.catObjects, UnitTypes.Attacker);

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeJob(gameManager.dogObjects, UnitTypes.Attacker);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            gameManager.RestartGame();
        }
    }

    public bool ChangeJob(List<CharMovement> gameObjectArray, UnitTypes unitTypes)
    {
        CharMovement target = null;

        switch (unitTypes)
        {
            // Prioritizes lower HP unit to be free
            case UnitTypes.Free:
                for (int i = 0; i < gameObjectArray.Count; ++i)
                {
                    if (target != null)
                    {
                        if (gameObjectArray[i].hp < target.hp && gameObjectArray[i].currentJob != UnitTypes.Free)
                        {
                            target = gameObjectArray[i];
                        }
                    }
                    else if (gameObjectArray[i].currentJob != UnitTypes.Free)
                    {
                        target = gameObjectArray[i];
                    }
                }

                if (target != null)
                {
                    target.changeJob(unitTypes);
                    return true;
                }
                break;

                // Prioritizes low health units, 2nd priority being free units, 3rd priority being any unit other than healer
            case UnitTypes.Healer:
                for (int i = 0; i < gameObjectArray.Count; ++i)
                {
                    if (target != null)
                    {
                        if (gameObjectArray[i].hp < target.hp && gameObjectArray[i].currentJob == UnitTypes.Free)
                        {
                            target = gameObjectArray[i];
                        }
                    }
                    else if (gameObjectArray[i].currentJob == UnitTypes.Free)
                    {
                        target = gameObjectArray[i];
                    }
                    //else if (gameObjectArray[i].currentJob != UnitTypes.Healer)
                    //{
                    //    target = gameObjectArray[i];
                    //}
                }

                if (target != null)
                {
                    target.changeJob(unitTypes);
                    return true;
                }
                break;

            case UnitTypes.Harvester:
                for (int i = 0; i < gameObjectArray.Count; ++i)
                {
                    if (gameObjectArray[i].currentJob == UnitTypes.Free)
                    {
                        gameObjectArray[i].changeJob(unitTypes);
                        return true;
                    }
                }
                break;

            case UnitTypes.Attacker:
                for (int i = 0; i < gameObjectArray.Count; ++i)
                {
                    if (target != null)
                    {
                        if (gameObjectArray[i].hp > target.hp && gameObjectArray[i].currentJob == UnitTypes.Free)
                        {
                            target = gameObjectArray[i];
                        }
                    }
                    else if (gameObjectArray[i].currentJob == UnitTypes.Free && gameObjectArray[i].hp >= 20)
                    {
                        target = gameObjectArray[i];
                    }

                    if (target != null)
                    {
                        target.changeJob(unitTypes);
                        return true;
                    }
                }
                break;
        }
        return false;


    }
}
