using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    // [SerializeField] private AudioClip gameOverSound;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    #region Game Over Functions
    //Game over function
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        // SoundManager.instance.PlaySound(gameOverSound);
    }
    public void GameWin()
    {
        winScreen.SetActive(true);
        // SoundManager.instance.PlaySound(gameOverSound);
    }

    //Restart level
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Activate game over screen
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
        #endif
    }
    #endregion
}
