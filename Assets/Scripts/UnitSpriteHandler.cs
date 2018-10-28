using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpriteHandler : MonoBehaviour
{
    [Header("For Death")]
    public GameObject wings;
    public GameObject baseColorObject;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer baseSprite;
    [SerializeField] private SpriteRenderer baseColor;
    [SerializeField] private SpriteRenderer unitJob;

    [SerializeField] private Sprite lineArt;
    [SerializeField] private Sprite hurtArt;
    [SerializeField] private Sprite attacker;
    [SerializeField] private Sprite healer;
    [SerializeField] private Sprite harvester;
    [SerializeField] private Sprite[] spriteColor;

    private Transform shakeObject;
    private IEnumerator spriteCoroutine;
    private float duration = 0.5f;
    private float scale = 0.1f;
    private Vector3 originalPos;

    public void Init()
    {
        shakeObject = gameObject.transform;

        int colorIndex = Random.Range(0, spriteColor.Length);

        baseSprite.sprite = lineArt;
        baseColor.sprite = spriteColor[colorIndex];
    }

    public void ChangeJob(UnitTypes unitTypes)
    {
        switch (unitTypes)
        {
            case UnitTypes.Attacker:
                unitJob.sprite = attacker;
                break;

            case UnitTypes.Free:
                unitJob.sprite = null;
                break;

            case UnitTypes.Harvester:
                unitJob.sprite = harvester;
                break;

            case UnitTypes.Healer:
                unitJob.sprite = healer;
                break;
        }
    }

    public void ShakingAnimation()
    {
        if (spriteCoroutine != null) StopCoroutine(spriteCoroutine);
        spriteCoroutine = ShakingCoroutine();
        StartCoroutine(spriteCoroutine);
    }

    public IEnumerator ShakingCoroutine()
    {
        originalPos = shakeObject.localPosition;
        float timer = duration;
        while (timer > 0)
        {
            Vector3 newPos = (Random.insideUnitSphere - new Vector3(0.5f, 0.5f, 0.5f)) * scale;
            newPos = new Vector3(newPos.x, 0, 0);
            shakeObject.localPosition = originalPos + newPos;
            //Debug.Log(newPos);
            yield return null;
            timer -= Time.deltaTime;
        }
        shakeObject.localPosition = originalPos;
    }

    public void DeathAnimation(GameObject unit)
    {
        StartCoroutine(DeathCoroutine(unit));
    }

    public IEnumerator DeathCoroutine(GameObject unit)
    {
        float timer = 1f;
        while (timer > 0)
        {
            Vector3 newPos = new Vector3(0, 0, 0.1f);
            wings.SetActive(true);
            baseColorObject.transform.localPosition += newPos;
            timer -= Time.deltaTime;
            yield return null;
        }

        Destroy(unit);
    }
}
