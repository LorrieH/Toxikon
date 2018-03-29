using UnityEngine;

public class QuitGame : MonoBehaviour {

    /// <summary>
    /// Quits the game and closes the application (Doesn't do anything on web)
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}