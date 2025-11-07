using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    public void OnPressRestart()
    {
        SceneManager.LoadScene("Dungeon1");
    }

    public void OnPressMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
