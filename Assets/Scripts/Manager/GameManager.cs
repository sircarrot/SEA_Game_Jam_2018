using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    public int initialUnitSpawn = 5;
    [HideInInspector] public bool inGame = false;

    [SerializeField] private GameObject[] headquarters = new GameObject[2];
    [SerializeField] private GameObject[] unitPrefabs = new GameObject[2];
    private Transform unitList;
    //public Transform[] playerMap;

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
        Debug.Log((int)playerSide);
        Vector3 charPosition = new Vector3(headquarters[(int)playerSide].transform.position.x - 10.0f, headquarters[(int)playerSide].transform.position.y, headquarters[(int)playerSide].transform.position.z);
        Quaternion charRotation = new Quaternion(0, 0, 0, 0);
        Instantiate(unitPrefabs[(int)playerSide], unitList);
    }

    //public void DeadUnit()
    //{
        
    //}

    public void StartGame()
    {
        // SetMap

        // Spawn 5 units each
        for(int i = 0; i < initialUnitSpawn; ++i)
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
        foreach(Transform child in unitList)
        {
            Destroy(child);
        }

        StartGame();
    }

    public enum PlayerSide
    {
        Cats,
        Dogs
    }
}
