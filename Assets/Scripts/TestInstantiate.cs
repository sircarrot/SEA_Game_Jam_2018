using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInstantiate : MonoBehaviour
{
    public GameObject cat;
    public Transform house;
    // Use this for initialization
    void Start ()
    {
        Vector3 catPosition = new Vector3(house.position.x + 0.5f, house.position.y + 0.5f, house.position.z + 0.5f);
        Quaternion catRotation = new Quaternion(0, 0, 0, 0);
        Instantiate(cat, catPosition, catRotation);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
