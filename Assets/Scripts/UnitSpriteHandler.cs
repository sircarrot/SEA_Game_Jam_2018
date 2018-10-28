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

    private Vector3 baseColorOriginalPosition;

    private Transform shakeObject;
    private IEnumerator spriteCoroutine;
    private float duration = 0.5f;
    private float scale = 0.1f;
    private Vector3 originalPos;
    private bool producing = false;
    private bool death = false;

    public void Init()
    {
        shakeObject = gameObject.transform;

        int colorIndex = Random.Range(0, spriteColor.Length);

        baseColorOriginalPosition = baseColor.transform.localPosition;

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
                ProducerAnimation();
                return;

            case UnitTypes.Healer:
                unitJob.sprite = healer;
                break;
        }
        ProducerAnimationOff();
    }

    public void ShakingAnimation()
    {
        if (death) return;

        if (spriteCoroutine != null) StopCoroutine(spriteCoroutine);

        ResetTransform();
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

        if(producing)
        {
            ProducerAnimation();
        }
    }

    public void DeathAnimation(GameObject unit)
    {
        death = true;

        if (spriteCoroutine != null) StopCoroutine(spriteCoroutine);

        ResetTransform();
        spriteCoroutine = DeathCoroutine(unit);
        StartCoroutine(spriteCoroutine);
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

    public void ProducerAnimation()
    {
        if (death) return;

        producing = true;
        if (spriteCoroutine != null) StopCoroutine(spriteCoroutine);

        ResetTransform();

        spriteCoroutine = ProducerCoroutine();
        StartCoroutine(spriteCoroutine);
    }
    
    public void ProducerAnimationOff()
    {
        if (death) return;

        producing = false;

        ResetTransform();
        if(spriteCoroutine != null) StopCoroutine(spriteCoroutine);
    }

    public void ResetTransform()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
        baseColor.transform.localPosition = baseColorOriginalPosition;
        baseColor.transform.localScale = new Vector3(1, 1, 1);
    }

    public IEnumerator ProducerCoroutine()
    {
        while (true)
        {
            float duration = 0.2f;
            // Press
            
            while(duration > 0f)
            {
                yield return null;
                baseColor.transform.localScale += new Vector3(0.2f * Time.deltaTime / 0.2f, 0, -0.2f * Time.deltaTime / 0.2f);
                duration -= Time.deltaTime;
            }

            // Up
            duration = 0.3f;
            while (duration > 0f)
            {
                yield return null;

                if(duration > 0.1f)
                {
                    baseColor.transform.localScale -= new Vector3(0.2f * Time.deltaTime / 0.2f, 0, -0.2f * Time.deltaTime / 0.2f);
                }

                baseColor.transform.localPosition += new Vector3(0, 0, 0.2f * Time.deltaTime / 0.2f);
                duration -= Time.deltaTime;
            }

            baseColor.transform.localScale = new Vector3(1,1,1);
            // Down
            duration = 0.3f;
            while (duration > 0f)
            {
                yield return null;
                baseColor.transform.localPosition -= new Vector3(0, 0, 0.2f * Time.deltaTime / 0.2f);
                duration -= Time.deltaTime;
            }

            baseColor.transform.localScale = new Vector3(1,1,1);
            baseColor.transform.localPosition -= new Vector3(0, 0, 0);

            // Pause
            duration = 0.2f;
            while (duration > 0f)
            {
                yield return null;
                duration -= Time.deltaTime;
            }
        }
    }
}
