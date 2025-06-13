using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class GhostBehaviour : MonoBehaviour
{
    public bool[] collectProbability = { false, true, false, false };
    public Transform[] wanderingLocations;
    private Transform destination;
    public GameObject cursedToy;
    public GameObject redCursedToy;
    public GameObject blueCursedToy;
    public GameObject purpleCursedToy;
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
    
    private readonly float optimalDistance = 2.0f; // distanza ottimale per il fantasma per raccogliere l'oggetto
    private bool isHunting;
    private bool firstTimeHunting;
    private bool playerSeen;
    private float startHuntingTimer;
    private readonly float startDemonHuntFirstTime = 1;
    private readonly float startNormalHuntFirstTime = 2f;
    private readonly float startDemonHunt = 1f;
    private readonly float startNormalHunt = 2f;
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
    private readonly float sightRange = 20f;
    private float sightDuration = 0; // the distance at which the ghost starts chasing the player
    public float deathRange = .75f; // the distance at which the ghost kills the player

    private bool cursedToyTaken;
    private bool redCursedToyTaken;
    private bool blueCursedToyTaken;
    private bool purpleCursedToyTaken;
    int toysDestroyed = 0;

    public enum GhostType
    {
        Revenant,
        Demon,
        Thaye,
        Phantom
    }
    public GhostType ghostType;


    void Start()
    {
        startHuntingTimer = 0;
        isHunting = false;
        firstTimeHunting = true;
        playerSeen = false;
        ghost = GetComponent<NavMeshAgent>();
        ghostType = ChooseGhostType(new GhostType[]{
                GhostType.Revenant, GhostType.Demon,GhostType.Thaye, GhostType.Phantom
            });
        thayeHuntingSpeed = normalHuntingSpeed * 0.25f;
        revenantInSightSpeed = normalHuntingSpeed * 1.5f;
        revenantNotInSightSpeed = normalHuntingSpeed * 0.5f;
        cursedToyTaken = false;
        redCursedToyTaken = false;
        blueCursedToyTaken = false;
        purpleCursedToyTaken = false;
    }


    void Update()
    {
        Debug.DrawRay(ghost.transform.position + Vector3.up * 1.3f, Vector3.forward*sightRange, Color.blue); //la direzione in cui spara il raggio è a sinistra a causa della rotazione del modello importato
        if (!isHunting)
        {
            startHuntingTimer += Time.deltaTime;
        }
        if (ghostType == GhostType.Demon && firstTimeHunting)
        {
            if (startHuntingTimer <= startDemonHuntFirstTime)
            {
                Wandering();
            }
            else
            {
                firstTimeHunting = false;
                Hunting();
            }
        }
        else if (ghostType == GhostType.Demon && !firstTimeHunting)
        {
            if (startHuntingTimer <= startDemonHunt)
            {
                Wandering();
            }
            else
            {
                Hunting();
            }
        }
        else if (ghostType != GhostType.Demon && firstTimeHunting)
        {
            if (startHuntingTimer <= startNormalHuntFirstTime)
            {
                Wandering();
            }
            else
            {
                firstTimeHunting = false;
                Hunting();
            }
        }
        else
        {
            if (startHuntingTimer <= startNormalHunt)
            {
                Wandering();
            }
            else
            {
                Hunting();
            }
        }
    }


    public Ghosts ChooseGhostType<Ghosts>(Ghosts[] enumValues) where Ghosts : Enum
    {
        int RandomIndex = UnityEngine.Random.Range(0, enumValues.Length);
        return enumValues[RandomIndex];
    }

    public Transform ChooseWanderingLocation(Transform[] enumValues, Transform location) {
        int indexNumber = UnityEngine.Random.Range(0, enumValues.Length);
        location = enumValues[indexNumber];
        return location;

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
    public void Wandering()
    {
        if (!isHunting)
        {
            VisibleGhost(false);
            ghost.speed = wanderingSpeed;
        }
        distToDestination = ghost.remainingDistance;
        if (distToDestination == 0)
        {
            destination = ChooseWanderingLocation(wanderingLocations, destination);
        }
        //ghost.transform.LookAt(destination);
        ghost.SetDestination(destination.position);
        SearchForCursedToys();
    }

    public void Hunting()
    {
        VisibleGhost(true);
        isHunting = true;
        startHuntingTimer = 0f;
        huntTimer += Time.deltaTime;
        huntDuration = ghostType == GhostType.Thaye ? huntDuration = thayeHuntDuration : normalHuntDuration;
        SeekPlayer();
        if (huntTimer <= huntDuration)
        {
            switch (ghostType)
            {
                case GhostType.Revenant:
                    if (playerSeen)
                    {
                        ghost.speed = revenantInSightSpeed;

                    }
                    else
                    {
                        ghost.speed = revenantNotInSightSpeed;
                        Wandering();
                    }
                    break;
                case GhostType.Demon:
                    ghost.speed = normalHuntingSpeed;
                    if (playerSeen)
                    {

                    }
                    else
                    {
                        Wandering();
                    }
                    break;
                case GhostType.Thaye:
                    ghost.speed = thayeHuntingSpeed;
                    if (playerSeen)
                    {

                    }
                    else
                    {
                        Wandering();
                    }
                    break;
                case GhostType.Phantom:
                    ghost.speed = normalHuntingSpeed;
                    if (playerSeen)
                    {
                        ghost.GetComponent<MeshRenderer>().enabled = true;

                    }
                    else
                    {
                        //VisibleGhost(false);
                        Wandering();
                    }
                    break;
            }
        }
        else
        {
            isHunting = false;
            huntTimer = 0f;
        }

    }
    public void SeekPlayer() {
        if (Physics.SphereCast(ghost.transform.position, 1.5f, Vector3.left, out RaycastHit hitInfo, sightRange))
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject.GetComponent<PlayerController>())
            {
                transform.LookAt(player.transform);
                ghost.SetDestination(player.transform.position);
                if (sightDuration == 0)
                {
                    playerSeen = true;
                    float followDuration = 5f;
                    sightDuration += 1f;
                    StartCoroutine(Follow(followDuration));
                }
                else
                {
                    playerSeen = false;
                    sightDuration = 0 + Time.deltaTime;
                }

            }
            else if (sightDuration == 0)
            {
                playerSeen = false;
                Wandering();
            }
            else
            {
                playerSeen = true;
                transform.LookAt(player.transform);
                sightDuration += Time.deltaTime;
                ghost.SetDestination(player.transform.position);
            }
        }
        else if (sightDuration == 0)
        {
            playerSeen = false;
            Wandering();
        }
        else
        {
            playerSeen = true;
            transform.LookAt(player.transform);
            sightDuration += Time.deltaTime;
            ghost.SetDestination(player.transform.position);
        }
    }

    private IEnumerator Follow(float duration)
    {
        yield return new WaitUntil(() => sightDuration > duration);
        sightDuration = 0;
    }
    
    public void SearchForCursedToys()
    {
        if (!cursedToyTaken)
        {
            float distToCursedToy = Vector3.Distance(a: transform.position, b: cursedToy.transform.position);
            if (distToCursedToy <= optimalDistance)
            {
                int indexNumber = UnityEngine.Random.Range(0, collectProbability.Length);
                if (collectProbability[indexNumber])
                {
                    cursedToy.SetActive(false);
                    toysDestroyed++;
                    cursedToyTaken = true;
                }
            }
        }
        if (!redCursedToyTaken)
        {
            float distToRed = Vector3.Distance(a: transform.position, b: redCursedToy.transform.position);
            if (distToRed <= optimalDistance)
            {
                int indexNumber = UnityEngine.Random.Range(0, collectProbability.Length);
                if (collectProbability[indexNumber])
                {
                    redCursedToy.SetActive(false);
                    toysDestroyed++;
                    redCursedToyTaken = true;
                }
            }
        }
        if (!blueCursedToyTaken)
        {
            float distToBlue = Vector3.Distance(a: transform.position, b: blueCursedToy.transform.position);
            if (distToBlue <= optimalDistance)
            {
                int indexNumber = UnityEngine.Random.Range(0, collectProbability.Length);
                if (collectProbability[indexNumber])
                {
                    blueCursedToy.SetActive(false);
                    toysDestroyed++;
                    blueCursedToyTaken = true;
                }
            }
        }
        if (!purpleCursedToyTaken)
        {
            float distToPurple = Vector3.Distance(a: transform.position, b: purpleCursedToy.transform.position);
            if (distToPurple <= optimalDistance)
            {
                int indexNumber = UnityEngine.Random.Range(0, collectProbability.Length);
                if (collectProbability[indexNumber])
                {
                    purpleCursedToy.SetActive(false);
                    toysDestroyed++;
                    purpleCursedToyTaken = true;
                }
            }
        }
    }
}
