using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class MonsterAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject hands;
    [SerializeField] private LayerMask WhatIsGround, WhatIsPlayer,whaIsWall;
    [SerializeField] public PlayableDirector playableDirector;
    [SerializeField] public GamePlayCanvas canvas;

    [Header("Patrolling")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private Vector3 walkPoint;
    [SerializeField] public bool walkPointSet;

    [Header("Chasing")]
    [SerializeField] private float chaseSpeed;

    [Header("Attacking")]
    [SerializeField] private float timeBetweenAttacks;
    private bool alreayAttacked;
    [SerializeField] private GameObject catchCamera;
    [SerializeField] private float waitToActivateTimeLine;
    [SerializeField] private AudioSource stopIdleSound;

    [Header("States")]
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSightRange, playerInAttackRange;

    [Header("WalkPoints")]
    [SerializeField] private Transform pointsFirstFloor;
    [SerializeField] private Transform pointsSecondFloor;
    [SerializeField] private Transform pointsFirstFloorNew;
    private List<Transform> walkPointsFirstFloor = new List<Transform>();
    private List<Transform> walkPointsSecondFloor = new List<Transform>();
    private List<Transform> walkPointsfirstFloorAfter = new List<Transform>();
    [HideInInspector] public bool firstFloor=true;
    [HideInInspector] public bool secondFloor=false;

    [Header("Eyes")]
    [SerializeField] private Eyes eyes;
    [Header("Noise")]
    [SerializeField] private Noise noise;

    [Header("Time")]
    [SerializeField] private float minStopTime;
    [SerializeField] private float maxStopTime;
    private float timer=0;
    private Ears ears;
    private NavMeshPath path;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        ears =GetComponent<Ears>();
        path = new NavMeshPath();
    }
    private void Start()
    {
        firstFloor = true;

        /*add walkpoints to lists*/
        foreach (Transform t in pointsFirstFloor)
        {
            walkPointsFirstFloor.Add(t);
        }
        foreach (Transform t in pointsSecondFloor)
        {
            walkPointsSecondFloor.Add(t);
        }
        foreach (Transform t in pointsFirstFloorNew)
        {
            walkPointsfirstFloorAfter.Add(t);
        }
    }
    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if (noise.GetIsHearing()==true || eyes.GetIsSeeingPlayer ()== true)//if monster sees or hears the player.
        {
           ChasePlayer();
        }
        else if(!playerInAttackRange)  //Player not in attack range,enemy patroling
        { Patrolling(); }

        if(playerInAttackRange)
        { AttackPlayer(); }      //player is in attack range,attackin the player.
    }
    private void Patrolling()
    {
        float stopTime = Random.Range(minStopTime, maxStopTime);//random stop time when reach the point.
        timer += Time.deltaTime;

        if (timer > stopTime)
        {
            if (!walkPointSet)
                SearchWalkPoint();//set new walk point

            if (walkPointSet)
                agent.SetDestination(walkPoint);//set monster destination.

            /*monster patroling animation*/
            GetComponent<Animator>().SetBool("monsterRunning", false);
            GetComponent<Animator>().SetBool("monsterMoving", true);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            agent.speed = walkSpeed;//setting walk speed.

            if (distanceToWalkPoint.magnitude < 1f)
            {
                timer = 0;
                walkPointSet = false;
            }
        }
        else
        {
            if (!walkPointSet)//monster standing.
            {
                GetComponent<Animator>().SetBool("monsterMoving", false);
            }
        }
    }
    private void SearchWalkPoint()//setting new walk point.
    {
        int randomNum;
       
        if (firstFloor)
        {
            if (player.GetComponent<CheckpointScript>().thirdFriend)
            {
                /*random walk point*/
                randomNum = Random.Range(0, walkPointsfirstFloorAfter.Count);
                walkPoint = walkPointsfirstFloorAfter[randomNum].position;
            }
            else
            {
                /*random walk point*/
                randomNum = Random.Range(0, walkPointsFirstFloor.Count);
                walkPoint = walkPointsFirstFloor[randomNum].position;
            }
          
        }
        else if(secondFloor)
        {
            /*random walk point*/
            randomNum = Random.Range(0, walkPointsSecondFloor.Count);
            walkPoint = walkPointsSecondFloor[randomNum].position;
        }
        
        walkPointSet = true;
    }
    private void ChasePlayer()//chase player.
    {
        agent.CalculatePath(player.position, path);

        if (path.status == NavMeshPathStatus.PathComplete)//if player reachable.
        {
            GetComponent<Animator>().SetBool("monsterRunning", true);//monster running.
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);// set monster destination to player.
        }
    }
    private void AttackPlayer()//attack player when in range.
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreayAttacked)
        {
            Time.timeScale = 1f;

            /*monster attacl animation*/
            GetComponent<Animator>().SetBool("monsterAttack", true);
            alreayAttacked = true;
            playableDirector.Play();

            Invoke(nameof(ResetAttack), timeBetweenAttacks);//time between attacks.
            StartCoroutine(ActicateTimeLine());
        }
    }
    IEnumerator  ActicateTimeLine()// activate monster killing player timeline.
    {
        yield return new WaitForSeconds(waitToActivateTimeLine);
        canvas.TextOff();
        player.GetComponent<PlayerActions>().firstSprintInteraction = false;
        player.GetComponent<PlayerActions>().firstCrouchInteraction = false;
        catchCamera.SetActive(true);
        stopIdleSound.Pause();
        hands.SetActive(false);
        player.gameObject.GetComponent<StarterAssets.FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void ResetAttack()
    {
        GetComponent<Animator>().SetBool("monsterAttack", false);
        alreayAttacked = false;   
    }
    public void ResetPlayableDirector()
    {
        playableDirector.Stop();
        playableDirector.time = 0;
        playableDirector.Evaluate();

        hands.SetActive(true);
        catchCamera.SetActive(false);
    }
}
