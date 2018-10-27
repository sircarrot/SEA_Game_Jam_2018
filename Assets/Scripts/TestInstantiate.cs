using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInstantiate : MonoBehaviour
{
    public int player1point, player2point;
    [SerializeField] private GameObject[] headquarters = new GameObject[2];
    [SerializeField] private GameObject[] unitPrefabs = new GameObject[2];
    private Transform unitList;
    //public GameObject cat;
    //public Transform house;
    // Use this for initialization
    void Start ()
    {
        //Vector3 catPosition = new Vector3(house.position.x + 0.5f, house.position.y + 0.5f, house.position.z + 0.5f);
        //Quaternion catRotation = new Quaternion(0, 0, 0, 0);
        //Instantiate(cat, catPosition, catRotation);
        player1point = 0;
        player2point = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpawnUnit(PlayerSide playerSide, int spawnSeq)
    {
        Debug.Log("Spawn Unit: " + playerSide.ToString());
        Debug.Log((int)playerSide);
        int xOffset = 1;
        if (((int)playerSide) > 0)
        {
            xOffset = -1;
        }
        Vector3 charPosition = new Vector3(headquarters[(int)playerSide].transform.position.x + xOffset * 1.3f, headquarters[(int)playerSide].transform.position.y, headquarters[(int)playerSide].transform.position.z + 2.2f - spawnSeq * 1.2f);
        Quaternion charRotation = new Quaternion(0, 0, 0, 0);
        Instantiate(unitPrefabs[(int)playerSide], charPosition, charRotation, unitList);
    }

    public void addHarvestPoint(int playerNum)
    {
        if (playerNum > 0)
        {
            player2point += 5;
            if (player2point >= 100)
            {
                SpawnUnit(PlayerSide.Dogs, 0);
                player2point = 0;
            }
        }
        else
        {
            player1point += 5;
            if (player1point >= 100)
            {
                SpawnUnit(PlayerSide.Cats, 0);
                player1point = 0;
            }
        }
    }

    public enum PlayerSide
    {
        Cats,
        Dogs
    }
}
