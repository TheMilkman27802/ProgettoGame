using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{

	public class Drawer_Pull_X : MonoBehaviour
	{

		public Animator pull_01;
		public bool open;
		public Transform Player;
		public AudioSource pullSound;

		void Start()
		{
			pullSound.enabled = false;
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
			pull_01.Play("openpull_01");
			open = true;
			pullSound.enabled = true;
			yield return new WaitForSeconds(.5f);
			pullSound.enabled = false;
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			pull_01.Play("closepush_01");
			open = false;
			pullSound.enabled = true;
			yield return new WaitForSeconds(.5f);
			pullSound.enabled = false;
		}


	}
}