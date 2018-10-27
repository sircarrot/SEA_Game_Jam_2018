using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMove : MonoBehaviour
{
    public int hp = 100;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (hp < 0)
        {
            Destroy(gameObject);
        }
	}

    public void hurt()
    {
        hp -= 10;
        Debug.Log("I'm hurt");
    }
}
