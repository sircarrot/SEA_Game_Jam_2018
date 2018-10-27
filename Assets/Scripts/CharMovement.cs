using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharMovement : MonoBehaviour
{
    //enum Jobs { Free, Attack, Heal, Harvest };
    public UnitTypes currentJob = UnitTypes.Free;
    private GameObject enemy;
    private GameObject[] enemyArray;
    NavMeshAgent agent;
    enum AttackStage { chase, attack, cooldown};
    enum HealStage { chase, heal, cooldown };
    AttackStage attckStage = AttackStage.chase;
    HealStage healStage = HealStage.chase;
    //public Sprite Normal, Hurt;
    SpriteRenderer charSprite;
    Transform childSprite;
    Transform HPTemp;
    TextMesh HPInd;

    int hp = 100;
    public int attackDamage = 10;
    float attackCooldown = 0.1f;
    float healCooldown = 0.1f;
    float animCooldown = 0.0f;

    int targetIndex = 0;
    private GameObject[] target;
    float roamCountdown;
    private int numOfTargetPoints, randomNumber;

    private GameObject Toolbox, myHouse;
    private int playerSide;

    // Use this for initialization
    void Start()
    {
        Toolbox = GameObject.Find("Toolbox");
        roamCountdown = Random.Range(2, 5);
        agent = GetComponent<NavMeshAgent>();
        childSprite = this.gameObject.transform.GetChild(0);
        HPTemp = this.gameObject.transform.GetChild(1);
        charSprite = childSprite.GetComponent<SpriteRenderer>();
        HPInd = HPTemp.GetComponent<TextMesh>();
        //Debug.Log(charSprite);
        //charSprite.sprite = Normal;

        if (gameObject.tag == "Cat")
        {
            target = GameObject.FindGameObjectsWithTag("CatPatrol");
            playerSide = 0;
            myHouse = GameObject.Find("Player1");
        }
        else
        {
            target = GameObject.FindGameObjectsWithTag("DogPatrol");
            playerSide = 1;
            myHouse = GameObject.Find("Player2");
        }

        numOfTargetPoints = target.Length;
        randomNumber = Random.Range(0, numOfTargetPoints - 1);

    }

    // Update is called once per frame
    void Update()
    {
        HPInd.text = hp.ToString();
        if (currentJob == UnitTypes.Free)
        {
            freeMode();           
        }
        else if (currentJob == UnitTypes.Attacker)
        {
            attackMode();
        }
        else if (currentJob == UnitTypes.Healer)
        {
            healMode();
        }
        else if (currentJob == UnitTypes.Harvester)
        {
            harvestMode();
        }

        if (animCooldown > 0)
        {
            animCooldown -= Time.deltaTime * 1.0f;
        }
        else
        {
            //charSprite.sprite = Normal;
        }
    }
    
    void freeMode()
    {
        Vector3 currentPosition = transform.position;
        float distToTarget = Vector3.Distance(myHouse.transform.position, currentPosition);
        if (distToTarget < 3.0f)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
            agent.destination = myHouse.transform.position;
        }
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
        if (closestDistance < 1.0f)
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
        GameObject[] teamMembersArray = GameObject.FindGameObjectsWithTag(gameObject.tag);
        GameObject nearestTeamMember = teamMembersArray[0];
        float closestDistance = 100000000;
        foreach (GameObject teamMember in teamMembersArray)
        {
            if (teamMember.GetComponent<CharMovement>().hp < 100)
            { 
                Vector3 currentPosition = transform.position;
                float distToTarget = Vector3.Distance(teamMember.transform.position, currentPosition);
                if (distToTarget < closestDistance)
                {
                    closestDistance = distToTarget;
                    nearestTeamMember = teamMember;
                }
            }
        }
        if (closestDistance < 1.0f)
        {
            if (healCooldown > 0)
            {
                healStage = HealStage.cooldown;
                healCooldown -= Time.deltaTime * 1.0f;
            }
            else
            {
                healStage = HealStage.heal;
                healCooldown = 2.0f;
            }
        }
        else
        {
            healStage = HealStage.chase;
        }
        //Debug.Log(closestDistance);
        if (healStage == HealStage.chase)
        {
            if (nearestTeamMember != null)
            {
                agent.isStopped = false;
                agent.destination = nearestTeamMember.transform.position;
                //Debug.Log("trigger this");
            }
        }
        else if (healStage == HealStage.heal)
        {
            if (nearestTeamMember != null)
            {
                agent.isStopped = true;
                nearestTeamMember.GetComponent<CharMovement>().heal();//heal
            }
        }
        else if (healStage == HealStage.cooldown)
        {
            agent.isStopped = true;
        }
    }

    void harvestMode()
    {       
        Vector3 currentPosition = transform.position;
        float distToTarget = Vector3.Distance(target[randomNumber].transform.position, currentPosition);

        if (distToTarget < 3.0f)
        {
            agent.isStopped = true;
            roamCountdown -= Time.deltaTime;
            if (roamCountdown < 0)
            {
                randomNumber = Random.Range(0, numOfTargetPoints - 1);
                roamCountdown = Random.Range(1, 3);
                Toolbox.GetComponent<GameManager>().addHarvestPoint(playerSide);
            }
        }
        else
        {
            agent.isStopped = false;
            agent.destination = target[randomNumber].transform.position;
        }
        //Debug.Log(distToTarget);

    }

    public void hurt()
    {
        hp -= 10;
        if (hp <= 20)
        {
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
            changeJob(UnitTypes.Free);
        }
        //charSprite.sprite = Hurt;
        //animCooldown = 0.5f;
        //childSprite.GetComponent<unitSpriteHandler>().ShakingAnimation();
    }

    public void heal()
    {
        if (hp <= 90)
        {
            hp += 10;
        }
        else
        {
            hp = 100;
        }
    }

    public void changeJob(UnitTypes newJob)
    {
        if (newJob == UnitTypes.Free)
        {
            currentJob = UnitTypes.Free;
            childSprite.GetComponent<unitSpriteHandler>().ChangeJob(UnitTypes.Free);
        }
        else if (newJob == UnitTypes.Attacker)
        {
            currentJob = UnitTypes.Attacker;
            childSprite.GetComponent<unitSpriteHandler>().ChangeJob(UnitTypes.Attacker);
        }
        else if (newJob == UnitTypes.Healer)
        {
            currentJob = UnitTypes.Healer;
            childSprite.GetComponent<unitSpriteHandler>().ChangeJob(UnitTypes.Healer);
        }
        else if (newJob == UnitTypes.Harvester)
        {
            currentJob = UnitTypes.Harvester;
            childSprite.GetComponent<unitSpriteHandler>().ChangeJob(UnitTypes.Harvester);
        }
    }
}
