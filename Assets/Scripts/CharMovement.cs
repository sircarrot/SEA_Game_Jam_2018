using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharMovement : MonoBehaviour
{
    public HPBarHandler hPBar;
    public UIManager uiManager;
    public UnitSpriteHandler unitSpriteHandler;
    public GameManager gameManager;
    public GameObject attckEffect, healEffect;

    private GameObject enemy;
    private GameObject[] enemyArray;
    NavMeshAgent agent;
    public UnitTypes currentJob = UnitTypes.Free;


    AttackStage attckStage = AttackStage.chase;
    HealStage healStage = HealStage.chase;

    SpriteRenderer charSprite;
    Transform childSprite;

    public int hp;
    public int totalHp = 100;
    float attackCooldown = 0.1f;
    float healCooldown = 0.1f;
    float lazyCooldown = 10.0f;
    float lazyDuration = 4.0f;
    float gameSpeedCountdown = 30.0f;

    private GameObject[] target;
    float roamCountdown;
    private int numOfTargetPoints, randomNumber;

    private GameObject myHouse;
    public PlayerSide playerSide;

    public float healRange = 5.0f;
    public int attackDamage = 10;
    public float attackSpeed = 1.0f;
    public float movementSpeed = 5.0f;

    // Use this for initialization
    void Start()
    {
        hp = totalHp;
        roamCountdown = Random.Range(2, 5);
        agent = GetComponent<NavMeshAgent>();
        childSprite = gameObject.transform.GetChild(0);
        charSprite = childSprite.GetComponent<SpriteRenderer>();
        unitSpriteHandler.Init();

        if (gameObject.tag == "Cat")
        {
            target = GameObject.FindGameObjectsWithTag("CatPatrol");
            playerSide = PlayerSide.Cats;
            myHouse = GameObject.Find("Player1");
        }
        else
        {
            target = GameObject.FindGameObjectsWithTag("DogPatrol");
            playerSide = PlayerSide.Dogs;
            myHouse = GameObject.Find("Player2");
        }

        numOfTargetPoints = target.Length;
        randomNumber = Random.Range(0, numOfTargetPoints - 1);
        countUnit();
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = movementSpeed;
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
        //inrease movement speed & attack speed every 30 secs
        gameSpeedCountdown -= Time.deltaTime;
        if (gameSpeedCountdown <= 0)
        {
            gameSpeedCountdown = 30;
            if (attackSpeed >= 0.5) { attackSpeed -= 0.1f; }
            if (movementSpeed <= 8.0f) { movementSpeed += 1.0f; }
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
            float distToTarget = Vector3.Distance(dog.transform.position, currentPosition);
            if (distToTarget < closestDistance)
            {
                closestDistance = distToTarget;
                enemy = dog;
            }
        }
        if (closestDistance < 1.0f && attckStage != AttackStage.lazy)
        {
            if (attackCooldown > 0)
            {
                attckStage = AttackStage.cooldown;
                attackCooldown -= Time.deltaTime * 1.0f;
            }
            else
            {
                attckStage = AttackStage.attack;
                attackCooldown = attackSpeed;
            }
        }
        else if (attckStage != AttackStage.lazy)
        {
            attckStage = AttackStage.chase;
        }

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
                enemy.GetComponent<CharMovement>().hurt(attackDamage);//inflict damage
                Vector3 charPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                Quaternion charRotation = new Quaternion(0, 0, 0, 0);
                GameObject unit = Instantiate(attckEffect, charPosition, charRotation);
                Destroy(unit, 0.15f);
                lazyRandomizer();
            }
        }
        else if (attckStage == AttackStage.cooldown)
        {
            agent.isStopped = true;
        }
        else if (attckStage == AttackStage.lazy)
        {
            if (lazyDuration == 4.0f)
            {
                agent.SetDestination(RandomNavmeshLocation(10.0f));
            }
            lazyDuration -= Time.deltaTime;
            if (lazyDuration <= 0.0f)
            {
                lazyDuration = 4.0f;
                lazyRandomizer();
            }
            agent.isStopped = false;
        }
    }

    void lazyRandomizer()
    {
        //int randomTimer = Random.Range(5, 10);
        int randomDecision = Random.Range(0, 9);
        Debug.Log(randomDecision);
        if (randomDecision > 7)
        {
            attckStage = AttackStage.lazy;
        }
        else
        {
            attckStage = AttackStage.chase;
        }
    }

    void healMode()
    {
        GameObject[] teamMembersArray = GameObject.FindGameObjectsWithTag(gameObject.tag);
        GameObject nearestTeamMember = teamMembersArray[0];
        float closestDistance = 100000000;
        bool hasInjured = false;
        foreach (GameObject teamMember in teamMembersArray)
        {
            if (teamMember.GetComponent<CharMovement>().hp < 100)
            {
                hasInjured = true;
                Vector3 currentPosition = transform.position;
                float distToTarget = Vector3.Distance(teamMember.transform.position, currentPosition);
                if (distToTarget < closestDistance)
                {
                    closestDistance = distToTarget;
                    nearestTeamMember = teamMember;
                }
            }
        }
        if (hasInjured)
        {
            if (closestDistance < healRange)
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
        }

        if (healStage == HealStage.chase)
        {
            if (nearestTeamMember != null || !hasInjured)
            {
                if (closestDistance < healRange + 2.0f)
                {
                    agent.isStopped = true;
                }
                else
                {
                    agent.isStopped = false;
                    agent.destination = nearestTeamMember.transform.position;
                }
            }
            else
            {
                agent.isStopped = true;
            }
        }
        else if (healStage == HealStage.heal)
        {
            if (nearestTeamMember != null)
            {
                agent.isStopped = true;
                nearestTeamMember.GetComponent<CharMovement>().heal();//heal
                Vector3 charPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                Quaternion charRotation = new Quaternion(0, 0, 0, 0);
                GameObject unit = Instantiate(healEffect, charPosition, charRotation);
                Destroy(unit, 0.25f);
            }
        }
        else if (healStage == HealStage.cooldown)
        {
            agent.isStopped = true;
        }
        Debug.Log(healStage);
    }

    void harvestMode()
    {
        Vector3 currentPosition = transform.position;
        float distToTarget = Vector3.Distance(target[randomNumber].transform.position, currentPosition);

        if (distToTarget < 4.0f)
        {
            agent.isStopped = true;
            roamCountdown -= Time.deltaTime;
            if (roamCountdown < 0)
            {
                randomNumber = Random.Range(0, numOfTargetPoints - 1);

                roamCountdown = Random.Range(2, 5);
                gameManager.AddHarvestPoint((int) playerSide);
            }
        }
        else
        {
            agent.isStopped = false;
            agent.destination = target[randomNumber].transform.position;
        }
        //Debug.Log(distToTarget);

    }

    public void hurt(int damage)
    {
        hp -= damage;

        hPBar.Damage((float) hp / (float) totalHp);

        if (hp <= 20)
        {
            changeJob(UnitTypes.Free);
            if (hp <= 0)
            {
                gameManager.DeadUnit(this);
            }
        }
        unitSpriteHandler.ShakingAnimation();
        countUnit();
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

        hPBar.Heal((float) hp/ (float) totalHp);

        countUnit();
    }

    public void changeJob(UnitTypes newJob)
    {
        AudioClip audioClip = null;
        if (newJob == UnitTypes.Free)
        {
            currentJob = UnitTypes.Free;
            unitSpriteHandler.ChangeJob(UnitTypes.Free);
            audioClip = gameManager.audioLibrary.freed[(int)playerSide];
        }
        else if (newJob == UnitTypes.Attacker)
        {
            currentJob = UnitTypes.Attacker;
            unitSpriteHandler.ChangeJob(UnitTypes.Attacker);
            audioClip = gameManager.audioLibrary.striker;
        }
        else if (newJob == UnitTypes.Healer)
        {
            currentJob = UnitTypes.Healer;
            unitSpriteHandler.ChangeJob(UnitTypes.Healer);
            audioClip = gameManager.audioLibrary.healer;
        }
        else if (newJob == UnitTypes.Harvester)
        {
            currentJob = UnitTypes.Harvester;
            unitSpriteHandler.ChangeJob(UnitTypes.Harvester);
            audioClip = gameManager.audioLibrary.producer;
        }

        gameManager.audioManager.PlaySoundEffect(audioClip);
        countUnit();
    }

    private void countUnit()
    {
        int totFreeUnit = countUnitType(UnitTypes.Free, false);
        int totAttackUnit = countUnitType(UnitTypes.Attacker, false);
        int totHarvestUnit = countUnitType(UnitTypes.Harvester, false);
        int totHealUnit = countUnitType(UnitTypes.Healer, false);
        int injuredFreeUnit = countUnitType(UnitTypes.Free, true);
        int injuredAttackUnit = countUnitType(UnitTypes.Attacker, true);
        int injuredHarvestUnit = countUnitType(UnitTypes.Harvester, true);
        int injuredHealUnit = countUnitType(UnitTypes.Healer, true);
        uiManager.UpdateText((int)playerSide, UnitTypes.Free, totFreeUnit, injuredFreeUnit);
        uiManager.UpdateText((int)playerSide, UnitTypes.Attacker, totAttackUnit, injuredAttackUnit);
        uiManager.UpdateText((int)playerSide, UnitTypes.Harvester, totHarvestUnit, injuredHarvestUnit);
        uiManager.UpdateText((int)playerSide, UnitTypes.Healer, totHealUnit, injuredHealUnit);
    }

    private int countUnitType(UnitTypes unitType, bool injured)
    {
        int totUnit = 0;
        GameObject[] teamMembersArray = GameObject.FindGameObjectsWithTag(gameObject.tag);
        foreach (GameObject teamMember in teamMembersArray)
        {
            CharMovement unit = teamMember.GetComponent<CharMovement>();
            if (unit.currentJob == unitType)
            {
                if (injured)
                {
                    if (unit.hp < 100)
                    {
                        totUnit++;
                    }
                }
                else
                {
                    totUnit++;
                }
            }
        }
        return totUnit;
    }

    private Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        Debug.Log(finalPosition);
        return finalPosition;
    }

    private IEnumerator BubbleCoroutine()
    {
        yield return null;
    }


    private enum AttackStage
    {
        chase,
        attack,
        cooldown,
        lazy
    };

    private enum HealStage
    {
        chase,
        heal,
        cooldown
    };
}
