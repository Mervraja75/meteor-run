using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // needed for Image

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //---UI---
    public GameObject startPanel;
    public GameObject gameOverPanel;

    //---AUDIO---
    public AudioSource musicSource;
    public AudioClip gameOverSfx;

    //--TOGGLES--
    public Image pauseButtonImage;   // this is for PauseButton's Image
    public Sprite pauseIcon;         // icon shown when game is running
    public Sprite resumeIcon;        // icon shown when game is paused
    public Image muteButtonImage;    // this is for MuteButton's Image
    public Sprite muteIcon;          // icon shown when game is unmuted
    public Sprite unmuteIcon;        // icon shown when game is muted

    // States to keep track of the game
    public bool gameIsRunning { get; private set; } = false;
    public bool IsPaused { get; private set; } = false;
    public bool IsMuted  { get; private set; } = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Show the intro menu, hide game over screen
        if (startPanel) startPanel.SetActive(true);
        if (gameOverPanel) gameOverPanel.SetActive(false);

        // Pause the game at the start
        Time.timeScale      = 0f;
        AudioListener.pause = true;
        gameIsRunning       = false;
        IsPaused            = true;

        UpdatePauseIcon();
        UpdateMuteIcon();
    }

    // === START ===
    public void StartGame()
    {
        if (startPanel) startPanel.SetActive(false);

        // play background music
        if (musicSource && !musicSource.isPlaying)
            musicSource.Play();

        // unpause the game
        Time.timeScale      = 1f;
        AudioListener.pause = false;
        gameIsRunning       = true;
        IsPaused            = false;

        UpdatePauseIcon();
    }

    // === GAME OVER ===
    public void GameOver()
    {
        if (ScoreManager.instance) ScoreManager.instance.OnGameOver();

        // stop music and play game over sound
        if (musicSource) musicSource.Stop();
        if (gameOverSfx && Camera.main)
            AudioSource.PlayClipAtPoint(gameOverSfx, Camera.main.transform.position, 1f);

        // show Game Over screen
        if (gameOverPanel) gameOverPanel.SetActive(true);

        // pause everything
        Time.timeScale      = 0f;
        AudioListener.pause = false;
        gameIsRunning       = false;
        IsPaused            = true;

        UpdatePauseIcon();
    }

    public void Restart()
    {
        // reset audio + time before restarting game
        AudioListener.pause  = false;
        AudioListener.volume = IsMuted ? 0f : 1f;
        if (musicSource) musicSource.mute = IsMuted;

        Time.timeScale = 1f;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
    
    void Update()
    {
        // 🔹 TEMP: For debugging, press G to trigger Game Over
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("DEBUG: G pressed → Game Over triggered");
            GameOver();
        }
    }

    // === PAUSE ===
    public void TogglePause()
    {
        IsPaused = !IsPaused;

        Time.timeScale = IsPaused ? 0f : 1f; //pause or resume game
        AudioListener.pause = IsPaused;           //pause or resume audio

        UpdatePauseIcon();
        Debug.Log($"TogglePause -> {(IsPaused ? "PAUSED" : "RESUMED")}");
    }

    void UpdatePauseIcon()
    {
        if (pauseButtonImage)
            pauseButtonImage.sprite = IsPaused ? resumeIcon : pauseIcon;
    }

    // === MUTE ===
    public void ToggleMute()
    {
        IsMuted = !IsMuted;

        AudioListener.volume = IsMuted ? 0f : 1f;       // mute or unmute all audio
        if (musicSource) musicSource.mute = IsMuted;    // also mute music

        UpdateMuteIcon(); // change button icon
        Debug.Log($"ToggleMute -> {(IsMuted ? "MUTED" : "UNMUTED")}");
    }

    void UpdateMuteIcon() 
    {
        // change mute button picture
        if (muteButtonImage)
            muteButtonImage.sprite = IsMuted ? unmuteIcon : muteIcon;
    }
}