using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    public void OnPressGameStart()
    {
        SceneManager.LoadScene("Dungeon1");
    }

    public void OnPressExit()
    {
        Application.Quit();
    }

    
}
