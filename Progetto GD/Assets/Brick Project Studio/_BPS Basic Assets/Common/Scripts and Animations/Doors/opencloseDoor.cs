using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;
		public AudioSource openCloseSound;

		void Start()
		{
			openCloseSound.enabled = false;
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
			openandclose.Play("Opening");
			open = true;
			openCloseSound.enabled = true;
			yield return new WaitForSeconds(.5f);
			openCloseSound.enabled = false;
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			openandclose.Play("Closing");
			open = false;
			openCloseSound.enabled = true;
			yield return new WaitForSeconds(.5f);
			openCloseSound.enabled = false;
		}


	}
}