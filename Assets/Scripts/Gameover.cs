using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public GameObject gameOverPanel; // reference to the UI panel
    public void GameOver()
    {
        if (ScoreManager.instance != null)
            ScoreManager.instance.OnGameOver(); // must happen before showing UI or when pausing

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }
    private void OnTriggerEnter2D(Collider2D other)

    //OnTrigger2D is called when another collider enters the trigger zone
    //Collder2D refers to the object that entered
    {
        if (other.CompareTag("Player")) //CompareTag is to check if the object entered is "Player"
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Restart the scene or GameOver
        }
    }
}
