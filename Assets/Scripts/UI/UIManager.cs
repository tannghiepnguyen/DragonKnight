using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
