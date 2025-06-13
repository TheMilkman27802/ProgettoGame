using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public AudioSource mainMenuTheme;

   public void Start()
   {
      mainMenuTheme.enabled = true;

   }


   public void Play()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      mainMenuTheme.enabled = false;

   }
   public void Quit()
   {
      Application.Quit();
      mainMenuTheme.enabled = false;
      Debug.Log("Player Has Quit The Game");

   }

}

