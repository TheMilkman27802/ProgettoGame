using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GhostBehaviour : MonoBehaviour
{
public bool[] collectProbability= {false,true,false,false}; 
public Transform[] wanderingLocations;
public Transform player;
private Transform destination;
public Transform cursedToy;
public Transform redCursedToy;
public Transform blueCursedToy;
public Transform purpleCursedToy;
public NavMeshAgent ghost;

int currentLocation;
int toysDestroyed = 0;
public float optimalDistance=5.0f; // distanza ottimale per il fantasma per raccogliere l'oggetto
public bool isHunting;
private bool firstTimeHunting;
private float ghostSpeed;
public float wanderingSpeed=1.0f;
public float huntingSpeed=5.0f;
private float distToDestination;
 public float chaseRange = 5.0f; // the distance at which the ghost starts chasing the player
 public float deathRange = .75f; // the distance at which the ghost kills the player



public enum GhostType{
        Revenant,
        Demon,
        Thaye,
        Phantom
        }
        GhostType ghostType;

        public Ghosts ChooseGhostType<Ghosts> (Ghosts[] enumValues) where Ghosts : Enum {
           int RandomIndex = UnityEngine.Random.Range(0,enumValues.Length);
            return enumValues [RandomIndex];
        }

        public void ChooseWanderingLocation(Transform[] enumValues, Transform location){
               int indexNumber = UnityEngine.Random.Range(0, enumValues.Length);
               location.position = enumValues[indexNumber].position;
             //  return location;

        }
       
    public void Wandering(){
        if (!isHunting){
            ghostSpeed = ghostType == GhostType.Revenant ? ghostSpeed = (wanderingSpeed*0.5f) : wanderingSpeed;
            ghostSpeed=wanderingSpeed;
            ghost.speed = wanderingSpeed;
            ChooseWanderingLocation(wanderingLocations, destination);
            ghost.transform.LookAt(destination);
            distToDestination = ghost.remainingDistance;
            Vector3 moveTowards = Vector3.MoveTowards(current: ghost.transform.position, destination.position, 100f);
            float distToCursedObj = Vector3.Distance(a: transform.position,b: cursedToy.position);
            float distToRed = Vector3.Distance(a: transform.position,b: redCursedToy.position);
            float distToBlue = Vector3.Distance(a: transform.position,b: blueCursedToy.position);
            float distToPurple = Vector3.Distance(a: transform.position,b: purpleCursedToy.position);
            if (distToCursedObj<=optimalDistance){
                int indexNumber = UnityEngine.Random.Range(0, collectProbability.Length);
                 if (collectProbability[indexNumber]){
                     Destroy(cursedToy.gameObject);
                      toysDestroyed++;
          }
         }
         if (distToRed<=optimalDistance){
            int indexNumber = UnityEngine.Random.Range(0, collectProbability.Length);
             if (collectProbability[indexNumber]){
                Destroy(redCursedToy.gameObject);
                toysDestroyed++;
           }
         }
         if (distToBlue<=optimalDistance){
            int indexNumber = UnityEngine.Random.Range(0, collectProbability.Length);
             if (collectProbability[indexNumber]){
                Destroy(blueCursedToy.gameObject);
                toysDestroyed++;
            }
         }
         if (distToPurple<=optimalDistance){
            int indexNumber = UnityEngine.Random.Range(0, collectProbability.Length);
             if (collectProbability[indexNumber]){
                Destroy(purpleCursedToy.gameObject);
                toysDestroyed++;
            }
         }
         if (toysDestroyed==4){
            
         }
         FollowingTarget();
    }
 }

bool CheckDistToTargetZero(){
            if (distToDestination==0) {
                return true;
            }
            else {
                return false;
            }
        }
        IEnumerator FollowingTarget (){
                yield return new WaitUntil(CheckDistToTargetZero);
            }
 public void PlayerSeen() {
     Ray ray = new(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<CharacterController>()){
                    ghostSpeed = ghostType == GhostType.Revenant ? ghostSpeed=(huntingSpeed*1.5f) : ghostSpeed=huntingSpeed;
               ghost.SetDestination(hit.point);
                }
 }
 }



    void Start()
    {
        isHunting=false;
        firstTimeHunting=true;
        ghost = GetComponent<NavMeshAgent>();
        ghostType=ChooseGhostType(new GhostType[]{
                GhostType.Revenant, GhostType.Demon,GhostType.Thaye, GhostType.Phantom
            }); 
        // destination = ChooseWanderingLocation(wanderingLocations, destination);
        
    }

   
    void Update()
    {
       Wandering();
        
        isHunting=true;
        ghostSpeed=huntingSpeed;
        
        switch (ghostType){
                case GhostType.Revenant :
                
                {
                    huntingSpeed *=1.5f;
                }
                Wandering();
                    break;
                case GhostType.Demon:
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.position, ghostSpeed * Time.deltaTime);
                }
                Wandering();
                break;
                case GhostType.Thaye:
                huntingSpeed*=0.25f;
               {
                    transform.position = Vector3.MoveTowards(transform.position, player.position, ghostSpeed * Time.deltaTime);
                }
                Wandering();
                break;
                case GhostType.Phantom:
                ghost.GetComponent<MeshRenderer>().enabled =false;
                break;
                
            }

        }

    }