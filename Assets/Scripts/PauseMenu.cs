using UnityEngine;
using UnityEngine.SceneManagement;

// Pause menu tutorial: https://www.youtube.com/watch?v=JivuXdrIHK0&ab_channel=Brackeys
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject menuUI;
    string menu = "Menu";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menu);
    }
}
