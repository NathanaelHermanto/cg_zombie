using UnityEngine;
using UnityEngine.SceneManagement;

// menu tutorial: https://www.youtube.com/watch?v=JivuXdrIHK0&ab_channel=Brackeys
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
