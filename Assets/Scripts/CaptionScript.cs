using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptionScript : MonoBehaviour {

    public Camera camera;
    public Text dialogueBox;
    public Transform referenceUnit;
    public CanvasGroup canvasGroup;
    private bool initComplete = false;

    private void Update()
    {
        if (!initComplete) return;

        if (referenceUnit == null) Destroy(gameObject);
        else
        {
            gameObject.transform.position = camera.WorldToScreenPoint(referenceUnit.position) + new Vector3(-10, 60, 0); ;
            canvasGroup.alpha = 1f;
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
