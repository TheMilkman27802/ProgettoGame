using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
  public AudioSource winSound;
    void Start()
    {
        winSound.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            winSound.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        }
    }

}

