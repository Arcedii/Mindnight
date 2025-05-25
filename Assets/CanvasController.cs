using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public static bool playerIsDead = false;
    public GameObject PlayerCanvas;
    public GameObject DeathCanvas;

    void Start()
    {
        PlayerCanvas.SetActive(true);
        DeathCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsDead == true)
        {
            PlayerCanvas.SetActive(false);
            DeathCanvas.SetActive(true);
        }
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
