using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;
    [Header("Win")]
    [SerializeField] private GameObject winScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);

    }

    public void Win()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckIfMenuOpened();
        }
    }
    void CheckIfMenuOpened()
    {
        if (pauseScreen.activeInHierarchy)
        {
            pauseGame(false);
        }
        else
        {
            pauseGame(true);
        }
    }

    #region Game Over
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion

    #region Pause
    public void pauseGame(bool status)
    {
        pauseScreen.SetActive(status);
        Time.timeScale = status ? 0 : 1;
    }

    public void SoundVolumn()
    {
        SoundManager.instance.ChangeSoundVolumn(0.2f);
    }

    public void MusicVolumn()
    {
        SoundManager.instance.ChangeMusicVolumn(0.2f);
    }
    #endregion
}
