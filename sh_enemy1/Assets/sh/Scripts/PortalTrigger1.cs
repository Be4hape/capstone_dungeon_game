using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger1 : MonoBehaviour
{
    public string nextSceneName = "Dungeon3";

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        SceneManager.LoadScene(nextSceneName);
    }
}
