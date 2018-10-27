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

    int hp = 100;
    int attackDamage = 10;
    float attackCooldown = 2.0f;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

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
    }
    
    void freeMode()
    {

    }

    void attackMode()
    {
        enemyArray = GameObject.FindGameObjectsWithTag("Dog");
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
                enemy.GetComponent<DogMove>().hurt();//inflict damage
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
        Vector3 currentPosition = transform.position;
        float distToTarget = Vector3.Distance(myHouse.transform.position, currentPosition);
        if (distToTarget > 5.0f)
        {
            agent.isStopped = false;
            agent.destination = myHouse.transform.position;
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
