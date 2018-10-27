using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarHandler : MonoBehaviour {

    public Camera camera;
    public Transform referenceUnit;    
    public bool initComplete = false;

    public void Init(Transform reference, Camera cam)
    {
        referenceUnit = reference;
        camera = cam;
        initComplete = true;
    }

    private void Update()
    {
        if (!initComplete) return;

        if (referenceUnit == null) Destroy(gameObject);
        else
        {
            gameObject.transform.position = camera.WorldToScreenPoint(referenceUnit.position);
            gameObject.transform.position += new Vector3(0, 35, 0);
        }

    }
}
