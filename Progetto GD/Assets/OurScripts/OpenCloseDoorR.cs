using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseDoorR : MonoBehaviour
{
    public Animator DoorClosetHingeR;
	public bool open;
	public Transform Player;
	public AudioSource openCloseDoor;

	void Start()
		{
			openCloseDoor.enabled = false;
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
			DoorClosetHingeR.Play("OpenDoorClosetR");
			open = true;
			openCloseDoor.enabled = true;
			yield return new WaitForSeconds(.5f);
			openCloseDoor.enabled = false;
		}

	IEnumerator closing()
	{
		print("you are closing the door");
		DoorClosetHingeR.Play("CloseDoorClosetR");
		open = false;
		openCloseDoor.enabled = true;
		yield return new WaitForSeconds(.5f);
		openCloseDoor.enabled = false;
		}


	}


