using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
public class GhostBehaviour : MonoBehaviour
{
    public Transform[] wanderingLocations;
    private Transform destination;
    public NavMeshAgent ghost;
    public Transform player;
    public GameObject objectRendered1;
    public GameObject objectRendered2;
    public GameObject objectRendered3;
    public GameObject objectRendered4;
    public GameObject objectRendered5;
    public GameObject objectRendered6;
    public GameObject objectRendered7;
    public GameObject objectRendered8;
    private bool isHunting;
    private bool isFollowing;
    public float secondsPassedFollowing;
    public Transform lookPlayerBase;
    private readonly float followingDuration = 10f;
    private float wanderingTimer;
    private float startHuntingTime;
    private readonly float startDemonHunt = 10f;
    private readonly float startNormalHunt = 20f;
    private float huntTimer;
    private float huntDuration;
    private readonly float normalHuntDuration = 20f;
    private readonly float thayeHuntDuration = 30f;
    private readonly float wanderingSpeed = 1.0f;
    private readonly float normalHuntingSpeed = 5.0f;
    private float thayeHuntingSpeed;
    private float revenantInSightSpeed;
    private float revenantNotInSightSpeed;
    private float distToDestination;
    public GhostType ghostType;
    public enum GhostType
    {
        Revenant,
        Demon,
        Thaye,
        Phantom
    }
    public Ghosts ChooseGhostType<Ghosts>(Ghosts[] enumValues) where Ghosts : Enum
    {
        int RandomIndex = UnityEngine.Random.Range(0, enumValues.Length);
        return enumValues[RandomIndex];
    }

    public Transform ChooseWanderingLocation(Transform[] enumValues)
    {
        int indexNumber = UnityEngine.Random.Range(0, enumValues.Length);
        Transform location = enumValues[indexNumber];
        return location;

    }

    public void Start()
    {
        ghost.GetComponent<BoxCollider>().enabled = false;
        wanderingTimer = 0f;
        huntDuration = normalHuntDuration;
        huntTimer = 0f;
        isHunting = false;
        secondsPassedFollowing = 0f;
        isFollowing = false;
        ghost = gameObject.GetComponent<NavMeshAgent>();
        ghostType = ChooseGhostType(new GhostType[]{
        GhostType.Revenant, GhostType.Demon,GhostType.Thaye, GhostType.Phantom});
        thayeHuntingSpeed = normalHuntingSpeed * 0.25f;
        revenantInSightSpeed = normalHuntingSpeed * 1.5f;
        revenantNotInSightSpeed = normalHuntingSpeed * 0.5f;
        destination = ChooseWanderingLocation(wanderingLocations);
        startHuntingTime = ghostType == GhostType.Demon ? startDemonHunt : startNormalHunt;
        huntDuration = ghostType == GhostType.Thaye ? thayeHuntDuration : normalHuntDuration;
        
    }
    public void Update()
    {
        if (isHunting)
        {
            ghost.GetComponent<BoxCollider>().enabled = true;
            switch (ghostType)
            {
                case GhostType.Revenant:
                    if (isFollowing)
                    {
                        ghost.speed = revenantInSightSpeed;
                    }
                    else
                    {
                        ghost.speed = revenantNotInSightSpeed;
                    }
                    break;
                case GhostType.Demon:
                    ghost.speed = normalHuntingSpeed;
                    break;
                case GhostType.Thaye:
                    ghost.speed = thayeHuntingSpeed;
                    break;
                case GhostType.Phantom:
                    ghost.speed = normalHuntingSpeed;
                    if (isFollowing)
                    {
                        VisibleGhost(true);
                    }
                    else
                    {
                        VisibleGhost(false);
                    }
                    break;
            }
            huntTimer += Time.deltaTime;
            if (huntTimer <= huntDuration)
            {
                VisibleGhost(true);
                Debug.DrawRay(transform.position + Vector3.up * 1.3f, Vector3.left, Color.blue);
                Debug.DrawRay(transform.position + Vector3.up * 1.3f, Vector3.forward, Color.red);
                bool checkFoward = Physics.SphereCast(ghost.transform.position + Vector3.up * 1.3f, 1.5f, Vector3.forward, out RaycastHit hitInfo2, 20f);
                if (Physics.SphereCast(transform.position + Vector3.up * 1.3f, 1.5f, Vector3.left, out RaycastHit hitInfo, 20f) || checkFoward)
                {
                    GameObject hitObject = hitInfo.transform.gameObject;
                    GameObject hitObject2 = hitInfo2.transform.gameObject;
                    if (hitObject.GetComponent<PlayerController>() || hitObject2.GetComponent<PlayerController>())
                    {
                        transform.LookAt(lookPlayerBase.position);
                        ghost.SetDestination(player.transform.position);
                        secondsPassedFollowing = 0f; //se ti continua a vedere il fantasma resetta il timer
                        if (!isFollowing)
                        {
                            isFollowing = true;
                        }
                        else
                        {
                            secondsPassedFollowing += Time.deltaTime;
                        }
                    }
                    else if (!isFollowing)
                    {
                        Wandering();
                        //Debug.Log("trovato qualcosa ma non il giocatore");
                    }
                    else
                    {
                        secondsPassedFollowing += Time.deltaTime;
                        if (secondsPassedFollowing > followingDuration)
                        {
                            isFollowing = false;
                        }
                        else
                        {
                            transform.LookAt(lookPlayerBase.position);
                            ghost.SetDestination(player.transform.position);
                        }
                    }
                }
                else if (!isFollowing)
                {
                    Wandering();
                    //Debug.Log("non ha trovato nulla");
                }
                else
                {
                    secondsPassedFollowing += Time.deltaTime;
                    if (secondsPassedFollowing > followingDuration)
                    {
                        isFollowing = false;
                    }
                    else
                    {
                        transform.LookAt(lookPlayerBase.position);
                        ghost.SetDestination(player.transform.position);
                    }
                }
            }
            else
            {
                isHunting = false;
                huntTimer = 0;
            }
        }
        else //qui non e' a caccia
        {   
            ghost.GetComponent<BoxCollider>().enabled = false;
            ghost.speed = wanderingSpeed;
            VisibleGhost(false);
            Wandering();
            wanderingTimer += Time.deltaTime;
            if (wanderingTimer > startNormalHunt)
            {
                wanderingTimer = 0;
                isHunting = true;
            }
        }
    }
    public void Wandering()
    {
        distToDestination = ghost.remainingDistance;
            if (distToDestination == 0)
            {
                destination = ChooseWanderingLocation(wanderingLocations);
            }
            ghost.SetDestination(destination.position);
    }
    public void VisibleGhost(bool newState)
    {
        if (newState)
        {
            ghost.GetComponent<MeshRenderer>().enabled = true;
            objectRendered1.GetComponent<SkinnedMeshRenderer>().enabled = true;
            objectRendered2.GetComponent<SkinnedMeshRenderer>().enabled = true;
            objectRendered3.GetComponent<SkinnedMeshRenderer>().enabled = true;
            objectRendered4.GetComponent<SkinnedMeshRenderer>().enabled = true;
            objectRendered5.GetComponent<SkinnedMeshRenderer>().enabled = true;
            objectRendered6.GetComponent<SkinnedMeshRenderer>().enabled = true;
            objectRendered7.GetComponent<SkinnedMeshRenderer>().enabled = true;
            objectRendered8.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        else
        {
            ghost.GetComponent<MeshRenderer>().enabled = false;
            objectRendered1.GetComponent<SkinnedMeshRenderer>().enabled = false;
            objectRendered2.GetComponent<SkinnedMeshRenderer>().enabled = false;
            objectRendered3.GetComponent<SkinnedMeshRenderer>().enabled = false;
            objectRendered4.GetComponent<SkinnedMeshRenderer>().enabled = false;
            objectRendered5.GetComponent<SkinnedMeshRenderer>().enabled = false;
            objectRendered6.GetComponent<SkinnedMeshRenderer>().enabled = false;
            objectRendered7.GetComponent<SkinnedMeshRenderer>().enabled = false;
            objectRendered8.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
    }
}
