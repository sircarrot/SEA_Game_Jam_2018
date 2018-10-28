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
    public UIManager uiManager;

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

    public void SpawnUnit(PlayerSide playerSide,int spawnSeq, bool sound = true)
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

        charMovement.uiManager = uiManager;
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

        if(sound)
        {
            audioManager.PlaySoundEffect(audioLibrary.spawned[(int)playerSide]);
        }
    }

    public void DeadUnit(CharMovement unit)
    {
        PlayerSide playerSide = unit.playerSide;

        string caption = captionsLibrary.GetCaption(CaptionsLibrary.CaptionsType.Death, playerSide);
        uiManager.CreateCaption(caption, unit.unitSpriteHandler.baseColorObject.transform);

        audioManager.PlaySoundEffect(audioLibrary.death[(int)playerSide]);

        switch (playerSide)
        {
            case PlayerSide.Cats:
                catObjects.Remove(unit);
                break;

            case PlayerSide.Dogs:
                dogObjects.Remove(unit);
                break;
        }

        Destroy(unit.hPBar.gameObject);
        unit.unitSpriteHandler.DeathAnimation(unit.gameObject);
        Destroy(unit);
    }

    public void FreeUnit(CharMovement unit)
    {
        PlayerSide playerSide = unit.playerSide;
        string caption = captionsLibrary.GetCaption(CaptionsLibrary.CaptionsType.Free, playerSide);
        AudioClip clip = audioLibrary.RandomCaption(playerSide);
        audioManager.PlayVoiceLine(clip);
        uiManager.CreateCaption(caption, unit.unitSpriteHandler.baseColorObject.transform);
    }

    public void StrikeUnit(CharMovement unit)
    {
        PlayerSide playerSide = unit.playerSide;
        string caption = captionsLibrary.GetCaption(CaptionsLibrary.CaptionsType.Striker, playerSide);
        AudioClip clip = audioLibrary.RandomCaption(playerSide);
        audioManager.PlayVoiceLine(clip);
        uiManager.CreateCaption(caption, unit.unitSpriteHandler.baseColorObject.transform);
    }

    public void ProducerUnit(CharMovement unit)
    {
        PlayerSide playerSide = unit.playerSide;
        string caption = captionsLibrary.GetCaption(CaptionsLibrary.CaptionsType.Producer, playerSide);
        AudioClip clip = audioLibrary.RandomCaption(playerSide);
        audioManager.PlayVoiceLine(clip);
        uiManager.CreateCaption(caption, unit.unitSpriteHandler.baseColorObject.transform);
    }

    public void HealerUnit(CharMovement unit)
    {
        PlayerSide playerSide = unit.playerSide;
        string caption = captionsLibrary.GetCaption(CaptionsLibrary.CaptionsType.Healer, playerSide);
        AudioClip clip = audioLibrary.RandomCaption(playerSide);
        audioManager.PlayVoiceLine(clip);
        uiManager.CreateCaption(caption, unit.unitSpriteHandler.baseColorObject.transform);
    }

    public void StartGame()
    {
        // SetMap

        catObjects.Clear();
        dogObjects.Clear();
        uiManager.ResetText();

        // Spawn 5 units each
        for(int i = 0; i < initialUnitSpawn; ++i)
        {
            SpawnUnit(PlayerSide.Cats,i, false);
            SpawnUnit(PlayerSide.Dogs,i, false);
        }

        uiManager.UpdateText(0, UnitTypes.Total, initialUnitSpawn);

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
        if (playerNum > 0 && dogObjects.Count < 50)
        {
            player2point += 2;
            if (player2point >= 100)
            {
                if (dogObjects.Count < 50) { SpawnUnit(PlayerSide.Dogs, 0); }
                player2point = 0;
            }
            uiManager.HarvestPointUpdate(playerNum, player2point);
        }
        else if (playerNum < 1 && catObjects.Count < 50)
        {
            player1point += 2;
            if (player1point >= 100)
            {
                if (catObjects.Count < 50) { SpawnUnit(PlayerSide.Cats, 0); }
                player1point = 0;
            }
            uiManager.HarvestPointUpdate(playerNum, player1point);
        }
        
    }
}

public enum PlayerSide
{
    Cats,
    Dogs
}