using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour 

{
    public int score = 0;                   //stores the players score
    public static ScoreManager instance;    //Singleton: lets other scripts access this easily without needing inference
    public TMP_Text scoreText;              // UI Element to display the score
    public TMP_Text gameOverScoreText;      // FinalScoreText in Inspector
    public TMP_Text gameOverHighScoreText;  // HighScoreText in Inspector
    public int highScore = 0;               // stored best score

    void Awake() 
    {
        if (instance == null)                           //ensure only one ScoreManager exist
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0); // load saved best

    }

    public void AddScore(int amount)
    {
        score += amount;                                //increase the score by given amount
        Debug.Log("Score: " + score);                   //for more infomation in the console 

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString(); //Update the UI
        }
    }
    
    public void OnGameOver()
    {
        // save new high score if defeated
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        // update the game over UI
        if (gameOverScoreText != null)
            gameOverScoreText.text = "Score: " + score;

        if (gameOverHighScoreText != null)
            gameOverHighScoreText.text = "High Score: " + highScore;
    }
}