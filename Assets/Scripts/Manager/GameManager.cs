using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    public Camera cam;
    public GameObject HPBar;
    public GameObject HPBarCanvases;

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

    public void SpawnUnit(PlayerSide playerSide,int spawnSeq)
    {
        Debug.Log("Spawn Unit: " + playerSide.ToString());
        Debug.Log((int)playerSide);
        int xOffset = 1;
        if (((int)playerSide) > 0)
        {
            xOffset = -1;
        }
        Vector3 charPosition = new Vector3(headquarters[(int)playerSide].transform.position.x + xOffset * 1.3f, headquarters[(int)playerSide].transform.position.y, headquarters[(int)playerSide].transform.position.z + 2.2f -  spawnSeq*1.2f);
        Quaternion charRotation = new Quaternion(0, 0, 0, 0);
        //Instantiate(unitPrefabs[(int)playerSide], unitList);
        Transform unit = Instantiate(unitPrefabs[(int)playerSide], charPosition, charRotation, unitList).transform;

        HPBarHandler handler = Instantiate(HPBar, HPBarCanvases.transform).GetComponent<HPBarHandler>();
        handler.Init(unit, cam);
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
            SpawnUnit(PlayerSide.Cats,i);
            SpawnUnit(PlayerSide.Dogs,i);
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
