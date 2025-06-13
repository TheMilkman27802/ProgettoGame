using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;
    public GameObject PauseMenuCanvas;
    public GameObject Player;
    
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.None;
            Player.GetComponent<PlayerController>().enabled = false;
            Cursor.visible = true;
            if (paused){
                Play();

            }else {
                Stop();
            }
        }
    }
     void Stop(){
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
     }

    public void Play(){
        PauseMenuCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Player.GetComponent<PlayerController>().enabled = true;
        Cursor.visible = false;
        Time.timeScale = 1f;
        paused = false;
        
     }

     public void GoToMainMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
     }
}
