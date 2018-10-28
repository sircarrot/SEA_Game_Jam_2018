using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptionScript : MonoBehaviour {

    public Camera camera;
    public Text dialogueBox;
    public Transform referenceUnit;
    private bool initComplete = false;

    private void Update()
    {
        if (!initComplete) return;

        if (referenceUnit == null) Destroy(gameObject);
        else
        {
            gameObject.transform.position = camera.WorldToScreenPoint(referenceUnit.position);
            gameObject.transform.position += new Vector3(-10, 40, 0);
        }
    }

    public void InitText(string caption, Transform unit, Camera cam)
    {
        camera = cam;
        dialogueBox.text = caption;
        referenceUnit = unit;
        initComplete = true;
    }

}
