using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
  public AudioSource deathSound;
    void Start()
    {
        deathSound.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Wait());
        if (Input.GetKeyDown(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        }
    }

    private IEnumerator Wait(){
        yield return new WaitForSeconds(2);
        deathSound.enabled = false;
    }
}
