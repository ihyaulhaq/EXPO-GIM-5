using UnityEngine;
using UnityEngine.SceneManagement; // Tambahkan namespace ini untuk menggunakan SceneManager

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Fungsi untuk memulai game dari tombol "Play"
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // Ganti "SampleScene" dengan nama scene gameplay Anda
    }

    // Fungsi untuk keluar dari game
    public void QuitGame()
    {
        Debug.Log("Game Quit!");
        Application.Quit();
    }
}
