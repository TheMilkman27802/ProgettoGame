
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public Transform Player;
    public Transform Stairs;
    public NavMeshAgent ghost;
    public float speed;
    public float chaseRange = 1000f;
    public bool playerSeen;
    void Start()
    {
        ghost = GetComponent<NavMeshAgent>();
        playerSeen= false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(ghost.transform.position, ghost.transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 1000f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<MouseLook>()) {
                    playerSeen= true;
                    print("ti vedo");
                    ghost.SetDestination(Player.transform.position);
                    } else {
                        playerSeen= false;
                        print("non ti vedo");
                        ghost.SetDestination(Stairs.transform.position);
                        }
        }else {
        ghost.SetDestination(Stairs.transform.position);


        }
    }


 /* public void SeekPlayer() {
    Ray ray = new Ray(ghost.transform.position, ghost.transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 1000f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<MouseLook>()) {
                    ghost.SetDestination(Player.transform.position);
                    } else {
                        ghost.SetDestination(Stairs.transform.position);
                        }
        }else {
        ghost.SetDestination(Stairs.transform.position);


        }
 } */
}