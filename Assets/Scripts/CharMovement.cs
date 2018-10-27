using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharMovement : MonoBehaviour
{
    enum Jobs { Free, Attack, Heal, Harvest };
    Jobs currentJob = Jobs.Attack;
    public GameObject enemy;
    private GameObject[] enemyArray;
    NavMeshAgent agent;
    enum AttackStage { chase, attack, cooldown};
    AttackStage attckStage = AttackStage.chase;
    public GameObject myHouse;
    public Sprite Normal, Hurt;
    SpriteRenderer charSprite;
    Transform childSprite;

    int hp = 100;
    public int attackDamage = 10;
    float attackCooldown = 0.1f;
    float animCooldown = 0.0f;

    int targetIndex = 0;
    public GameObject[] target;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        childSprite = this.gameObject.transform.GetChild(0);
        charSprite = childSprite.GetComponent<SpriteRenderer>();
        Debug.Log(charSprite);
        charSprite.sprite = Normal;
    }

    // Update is called once per frame
    void Update()
    {
        //temp setting to change mode
        if(Input.GetKeyDown(KeyCode.X))
        {
            currentJob = Jobs.Free;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentJob = Jobs.Attack;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentJob = Jobs.Heal;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentJob = Jobs.Harvest;
        }

        if (currentJob == Jobs.Free)
        {
            freeMode();
        }
        else if (currentJob == Jobs.Attack)
        {
            attackMode();
        }
        else if (currentJob == Jobs.Heal)
        {
            healMode();
        }
        else if (currentJob == Jobs.Harvest)
        {
            harvestMode();
        }

        if (hp < 0)
        {
            Destroy(gameObject);
        }

        if (animCooldown > 0)
        {
            animCooldown -= Time.deltaTime * 1.0f;
        }
        else
        {
            charSprite.sprite = Normal;
        }
    }
    
    void freeMode()
    {

    }

    void attackMode()
    {
        if (gameObject.tag == "Cat")
        {
            enemyArray = GameObject.FindGameObjectsWithTag("Dog");
        }
        else
        {
            enemyArray = GameObject.FindGameObjectsWithTag("Cat");
        }     
        float closestDistance = 100000000;
        foreach (GameObject dog in enemyArray)
        {
            Vector3 currentPosition = transform.position;
            float distToTarget = Vector3.Distance(dog.transform.position,currentPosition);
            if (distToTarget < closestDistance)
            {
                closestDistance = distToTarget;
                enemy = dog;
            }
        }
        if (closestDistance < 3.0f)
        {
            if (attackCooldown > 0)
            {
                attckStage = AttackStage.cooldown;
                attackCooldown -= Time.deltaTime * 1.0f;
            }
            else
            {
                attckStage = AttackStage.attack;
                attackCooldown = 2.0f;
            }
        }
        else
        {
            attckStage = AttackStage.chase;
        }
        //Debug.Log(closestDistance);
        if (attckStage == AttackStage.chase)
        {
            if (enemy != null)
            {
                agent.isStopped = false;
                agent.destination = enemy.transform.position;
            }
        }
        else if (attckStage == AttackStage.attack)
        {
            if (enemy != null)
            {
                agent.isStopped = true;
                enemy.GetComponent<CharMovement>().hurt();//inflict damage
            }
        }
        else if (attckStage == AttackStage.cooldown)
        {
            agent.isStopped = true;
        }
    }

    void healMode()
    {

    }

    void harvestMode()
    {
        int numOfTargetPoints = target.Length;
        //int randomNumber = Random.Range(0, numOfTargetPoints - 1);
        int randomNumber = 0;

        Vector3 currentPosition = transform.position;
        float distToTarget = Vector3.Distance(target[randomNumber].transform.position, currentPosition);

        if (distToTarget < 1.0f)
        {
            agent.isStopped = false;
            agent.destination = myHouse.transform.position;
        }
        else
        {
            agent.isStopped = true;
        }
        //agent.destination = myHouse.transform.position;
    }

    public void hurt()
    {
        hp -= 10;
        charSprite.sprite = Hurt;
        animCooldown = 0.5f;
    }
}
