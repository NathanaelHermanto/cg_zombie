using UnityEngine;
using UnityEngine.SceneManagement;

// start menu tutorial: https://www.youtube.com/watch?v=zc8ac_qUXQY&ab_channel=Brackeys
public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex  + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

}
