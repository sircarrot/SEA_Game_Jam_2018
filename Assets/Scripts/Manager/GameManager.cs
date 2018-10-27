using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    public Transform unitList;
    public bool inGame = false;

    [SerializeField] private GameObject[] unitPrefabs = new GameObject[2];
    private PlayerData[] playerData = new PlayerData[2];


    public void Init()
    {
        if (unitList == null)
        {
            unitList = new GameObject("Unit List").transform;
        }

        // Temporary until we have title page
        StartGame();
    }

    public void SpawnUnit(PlayerSide playerSide)
    {
        Debug.Log("Spawn Unit: " + playerSide.ToString());
        Instantiate(unitPrefabs[(int)playerSide], unitList);
    }

    //public void DeadUnit()
    //{
        
    //}

    public void StartGame()
    {
        // SetMap

        // Spawn 5 units each
        for(int i = 0; i < 5; ++i)
        {
            SpawnUnit(PlayerSide.Cats);
            SpawnUnit(PlayerSide.Dogs);
        }

        inGame = true;
    }

    public void EndGame()
    {
        inGame = false;
    }

    public void RestartGame()
    {
        // Destroy/Pool all units

        StartGame();
    }

    public class PlayerData
    {
        public int[] unitList = new int[5];
        //public int kills;


    }

    public enum PlayerSide
    {
        Cats,
        Dogs
    }
}
