using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObjectInventory : MonoBehaviour
{
    [SerializeField] GameObject object01;
    [SerializeField] GameObject object02;
    [SerializeField] GameObject object03;
    [SerializeField] GameObject object04;
    public bool isEquippedObj01;
    public bool isEquippedObj02;
    public bool isEquippedObj03;
    public bool isEquippedObj04;
    void Start()
    {
        isEquippedObj01 = false;
        isEquippedObj02 = false;
        isEquippedObj03 = false;
        isEquippedObj04 = false;
        object01.SetActive(false);
        object02.SetActive(false);
        object03.SetActive(false);
        object04.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("0"))
        {
            object01.SetActive(false);
            object02.SetActive(false);
            object03.SetActive(false);
            object04.SetActive(false);
        }
        else if (Input.GetButtonDown("1") && isEquippedObj01)
        {
            object01.SetActive(true);
            object02.SetActive(false);
            object03.SetActive(false);
            object04.SetActive(false);
        }
        else if (Input.GetButtonDown("2") && isEquippedObj02)
        {
            object01.SetActive(false);
            object02.SetActive(true);
            object03.SetActive(false);
            object04.SetActive(false);
        }
        else if (Input.GetButtonDown("3") && isEquippedObj03)
        {
            object01.SetActive(false);
            object02.SetActive(false);
            object03.SetActive(true);
            object04.SetActive(false);
        }
        else if (Input.GetButtonDown("4") && isEquippedObj04)
        {
            object01.SetActive(false);
            object02.SetActive(false);
            object03.SetActive(false);
            object04.SetActive(true);
        }
    }
    public bool SetIsEquippedObj01(bool newState)
    {
        isEquippedObj01 = newState;
        return newState;
    }
    public bool SetIsEquippedObj02(bool newState)
    {
        isEquippedObj02 = newState;
        return newState;
    }
    public bool SetIsEquippedObj03(bool newState)
    {
        isEquippedObj03 = newState;
        return newState;
    }
    public bool SetIsEquippedObj04(bool newState)
    {
        isEquippedObj04 = newState;
        return newState;
    }
}
