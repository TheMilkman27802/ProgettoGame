using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;
    public GameObject PauseMenuCanvas;
    public GameObject Player;
    public AudioSource pauseMenubackgroundMusic;
    public AudioSource gameBackgroundMusic;

    
    void Start()
    {
        pauseMenubackgroundMusic.enabled = false;
        gameBackgroundMusic.enabled = true;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.None;
            Player.GetComponent<PlayerController>().enabled = false;
            Cursor.visible = true;
            gameBackgroundMusic.enabled = false;
            pauseMenubackgroundMusic.enabled = true;
            if (paused){
                PlayGame();
                
            }else {
                
                StopGame();
            }
        }
    }
     void StopGame(){
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
     }

    public void PlayGame(){
        PauseMenuCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Player.GetComponent<PlayerController>().enabled = true;
        Cursor.visible = false;
        gameBackgroundMusic.enabled = true;
        pauseMenubackgroundMusic.enabled = false;
        Time.timeScale = 1f;
        paused = false;
        
     }

     public void GoToMainMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        pauseMenubackgroundMusic.enabled = false;
        gameBackgroundMusic.enabled = false;
     }
}
