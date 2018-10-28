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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!gameManager.inGame) gameManager.RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gameManager.RestartGame();
        }

        if (!gameManager.inGame) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            if(ChangeJob(gameManager.catObjects, UnitTypes.Healer))
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(ChangeJob(gameManager.dogObjects, UnitTypes.Healer))
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if(ChangeJob(gameManager.catObjects, UnitTypes.Free))
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(ChangeJob(gameManager.dogObjects, UnitTypes.Free))
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if(ChangeJob(gameManager.catObjects, UnitTypes.Harvester))
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (ChangeJob(gameManager.dogObjects, UnitTypes.Harvester))
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if(ChangeJob(gameManager.catObjects, UnitTypes.Attacker))
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (ChangeJob(gameManager.dogObjects, UnitTypes.Attacker))
            {
                return;
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.inGame == false)
        {
            SceneManager.LoadScene("MenuScene");
        }*/
    }

    public bool ChangeJob(List<CharMovement> gameObjectArray, UnitTypes unitTypes)
    {
        CharMovement target = null;
        bool result = false;
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
                    result = true;
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
                }

                if (target != null)
                {
                    target.changeJob(unitTypes);
                    result = true;
                }
                break;

            case UnitTypes.Harvester:
                for (int i = 0; i < gameObjectArray.Count; ++i)
                {
                    if (gameObjectArray[i].currentJob == UnitTypes.Free)
                    {
                        target = gameObjectArray[i];
                        target.changeJob(unitTypes);
                        result = true;
                        break;
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
                }

                if (target != null)
                {
                    target.changeJob(unitTypes);
                    result = true;
                }
                break;
        }

        if(result)
        {
            RearrangeList(gameObjectArray, target);
        }
        return false;
    }

    public void RearrangeList(List<CharMovement> charMovements, CharMovement target)
    {
        charMovements.Remove(target);
        charMovements.Add(target);
    }
}
