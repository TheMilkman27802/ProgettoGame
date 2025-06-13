using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class Drawer_Pull_Z : MonoBehaviour
	{

		public Animator pull;
		public bool open;
		public Transform Player;
		public AudioSource pullZSound;

		void Start()
		{
			pullZSound.enabled = false;
			open = false;
		}

		void OnMouseOver()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 10)
					{
						print("object name");
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
			pull.Play("openpull");
			open = true;
			pullZSound.enabled = true;
			yield return new WaitForSeconds(.5f);
			pullZSound.enabled = false;
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			pull.Play("closepush");
			open = false;
			pullZSound.enabled = true;
			yield return new WaitForSeconds(.5f);
			pullZSound.enabled = false;
		}


	}
}