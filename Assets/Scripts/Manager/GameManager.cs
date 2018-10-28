using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    [Header("Serialized")]
    public AudioLibrary audioLibrary;
    public CaptionsLibrary captionsLibrary;

    public Camera cam;
    public GameObject HPBar;
    public GameObject HPBarCanvases;
    public GameObject uiManager;

    [SerializeField] private GameObject[] headquarters = new GameObject[2];
    [SerializeField] private GameObject[] unitPrefabs = new GameObject[2];

    [Header("Producer Points")]
    public int player1point = 0, player2point = 0;

    public int initialUnitSpawn = 5;
    [HideInInspector] public bool inGame = false;

    [Header("Script variables")]
    public Transform unitList;
    public List<CharMovement> catObjects = new List<CharMovement>();
    public List<CharMovement> dogObjects = new List<CharMovement>();
    //public Transform[] playerMap;
    public AudioManager audioManager;

    public void Init()
    {
        audioManager = Toolbox.Instance.GetManager<AudioManager>();

        audioManager.BGMPlayer(audioLibrary.mainBGM, AudioManager.PlayBGMType.Repeat, 0.7f);

        if (unitList == null)
        {
            unitList = new GameObject("Unit List").transform;
        }

        // Temporary until we have title page
        StartGame();
    }

    public void SpawnUnit(PlayerSide playerSide,int spawnSeq)
    {
        GameObject[] target = GameObject.FindGameObjectsWithTag("CatPatrol");
        //int xOffset = 1;
        if (((int)playerSide) > 0)
        {
            //xOffset = -1;
            target = GameObject.FindGameObjectsWithTag("DogPatrol");
        }
        //Vector3 charPosition = new Vector3(headquarters[(int)playerSide].transform.position.x + xOffset * 1.3f, headquarters[(int)playerSide].transform.position.y, headquarters[(int)playerSide].transform.position.z + 2.2f -  spawnSeq*1.2f);
        Vector3 charPosition = new Vector3(target[spawnSeq].transform.position.x, target[spawnSeq].transform.position.y, target[spawnSeq].transform.position.z);
        Quaternion charRotation = new Quaternion(0, 0, 0, 0);
        //Instantiate(unitPrefabs[(int)playerSide], unitList);
        Transform unit = Instantiate(unitPrefabs[(int)playerSide], charPosition, charRotation, unitList).transform;

        CharMovement charMovement = unit.GetComponent<CharMovement>();
        charMovement.gameManager = this;

        UIManager uiMan = uiManager.GetComponent<UIManager>();
        charMovement.uiManager = uiMan;
        switch (playerSide)
        {
            case PlayerSide.Cats:
                catObjects.Add(charMovement);
                break;

            case PlayerSide.Dogs:
                dogObjects.Add(charMovement);
                break;
        }

        HPBarHandler handler = Instantiate(HPBar, HPBarCanvases.transform).GetComponent<HPBarHandler>();
        handler.Init(unit, cam);
        charMovement.hPBar = handler;

        audioManager.PlaySoundEffect(audioLibrary.spawned[(int) playerSide]);
    }

    public void DeadUnit(CharMovement unit)
    {
        PlayerSide playerSide = unit.playerSide;
        audioManager.PlaySoundEffect(audioLibrary.death[(int)playerSide]);
        switch(playerSide)
        {
            case PlayerSide.Cats:
                catObjects.Remove(unit);
                break;

            case PlayerSide.Dogs:
                dogObjects.Remove(unit);
                break;
        }

        unit.unitSpriteHandler.DeathAnimation(unit.gameObject);
        Destroy(unit);
    }

    public void StartGame()
    {
        // SetMap

        catObjects.Clear();
        dogObjects.Clear();

        // Spawn 5 units each
        for(int i = 0; i < initialUnitSpawn; ++i)
        {
            SpawnUnit(PlayerSide.Cats,i);
            SpawnUnit(PlayerSide.Dogs,i);
        }

        audioManager.PlaySoundEffect(audioLibrary.gameStart);

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
            Destroy(child.gameObject);
        }

        StartGame();
    }


    public void AddHarvestPoint(int playerNum)
    {
        int playerPoints = 0;
        if (playerNum > 0)
        {
            player2point += 5;
            if (player2point >= 100)
            {
                if (dogObjects.Count < 50) { SpawnUnit(PlayerSide.Dogs, 0); }
                player2point = 0;
            }
            playerPoints = player2point;
        }
        else
        {
            player1point += 5;
            if (player1point >= 100)
            {
                if (catObjects.Count < 50) { SpawnUnit(PlayerSide.Cats, 0); }
                player1point = 0;
            }
            playerPoints = player1point;
        }
        UIManager uiMan = uiManager.GetComponent<UIManager>();
        uiMan.HarvestPointUpdate(playerNum, playerPoints);
    }
}

public enum PlayerSide
{
    Cats,
    Dogs
}