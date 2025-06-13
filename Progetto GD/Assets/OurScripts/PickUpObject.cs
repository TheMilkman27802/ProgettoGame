using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject objectOnPlayer;
    private SwitchObjectInventory switchObjectInventory;
    public GameObject player;
    void Start()
    {
        switchObjectInventory = player.GetComponent<SwitchObjectInventory>();
        objectOnPlayer.SetActive(false);
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                switch (objectOnPlayer.tag)
                {
                    case "objectInventory1":
                        bool objInventory = switchObjectInventory.isEquippedObj01;
                        objInventory = switchObjectInventory.SetIsEquippedObj01(true);
                        break;
                    case "objectInventory2":
                        objInventory = switchObjectInventory.isEquippedObj02;
                        objInventory = switchObjectInventory.SetIsEquippedObj02(true);
                        break;
                    case "objectInventory3":
                        objInventory = switchObjectInventory.isEquippedObj03;
                        objInventory = switchObjectInventory.SetIsEquippedObj03(true);
                        break;
                    case "objectInventory4":
                        objInventory = switchObjectInventory.isEquippedObj04;
                        objInventory = switchObjectInventory.SetIsEquippedObj04(true);
                        break;
                }
            }
        }
    }
}
