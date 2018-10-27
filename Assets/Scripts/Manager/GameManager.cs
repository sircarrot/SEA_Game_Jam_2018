﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    public int initialUnitSpawn = 5;
    [HideInInspector] public bool inGame = false;

    [SerializeField] private GameObject[] headquarters = new GameObject[2];
    [SerializeField] private GameObject[] unitPrefabs = new GameObject[2];
    private Transform unitList;

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
        Instantiate(unitPrefabs[(int)playerSide],  unitList);
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
