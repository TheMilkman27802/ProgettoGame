using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoorL : MonoBehaviour
{
    public Animator DoorClosetHingeL;
	public bool open;
	public Transform Player;
	public AudioSource openCloseDoorSound;

	void Start()
		{
			openCloseDoorSound.enabled = false;
			open = false;
		}
    
   		void OnMouseOver()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 15)
					{
						if (open == false)
						{
							if (Input.GetMouseButtonDown(0))
							{
								StartCoroutine(opening());
							}
						}
						else
						{
							if (open == true)
							{
								if (Input.GetMouseButtonDown(0))
								{
									StartCoroutine(closing());
								}
							}

						}

					}
				}

			}

		}

	IEnumerator opening()
	{
		print("you are opening the door");
		DoorClosetHingeL.Play("OpenDoorClosetL");
		open = true;
		openCloseDoorSound.enabled = true;
		yield return new WaitForSeconds(.5f);
		openCloseDoorSound.enabled = false;
		}

	IEnumerator closing()
	{
		print("you are closing the door");
		DoorClosetHingeL.Play("CloseDoorClosetL");
		open = false;
		openCloseDoorSound.enabled = true;
		yield return new WaitForSeconds(.5f);
		openCloseDoorSound.enabled = false;
		}


	}