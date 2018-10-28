using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarHandler : MonoBehaviour
{
    public Slider slider;
    public Camera camera;
    public Transform referenceUnit;
    public GameObject visibility;
    public bool initComplete = false;

    public void Init(Transform reference, Camera cam)
    {
        if (slider == null) slider = GetComponent<Slider>();
        referenceUnit = reference;
        camera = cam;
        initComplete = true;
        visibility.SetActive(false);
    }

    private void Update()
    {
        if (!initComplete) return;

        if (referenceUnit == null) Destroy(gameObject);
        else
        {
            gameObject.transform.position = camera.WorldToScreenPoint(referenceUnit.position);
            gameObject.transform.position += new Vector3(-10, 35, 0);
        }
    }

    public void Damage(float percentage)
    {
        slider.value = percentage;
        visibility.SetActive(true);
    }

    public void Heal(float percentage)
    {
        slider.value = percentage;
        if(percentage >= 1f)
        {
            visibility.SetActive(false);
        }
    }

}
