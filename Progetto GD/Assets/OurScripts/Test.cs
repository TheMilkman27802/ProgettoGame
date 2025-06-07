
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform Player;
    public Transform ghost;
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ghost.GetComponent<MeshRenderer>().enabled =false;
         transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
    }
}
