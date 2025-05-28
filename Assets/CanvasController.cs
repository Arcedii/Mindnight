using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public static bool playerIsDead = false;
    public GameObject PlayerCanvas;
    public GameObject DeathCanvas;
    public GameObject WinCanvas;
    public static bool playerHasWon = false;

    public MonoBehaviour movementScript;
    public GameObject PlayerCamera, DeathCamera1;



    void Start()
    {
        playerIsDead = false;
        playerHasWon = false;

        PlayerCanvas.SetActive(true);
        DeathCanvas.SetActive(false);
        WinCanvas.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (playerIsDead)
        {
            PlayerCanvas.SetActive(false);
            DeathCanvas.SetActive(true);
        }
        else if (playerHasWon)
        {
            PlayerCanvas.SetActive(false);
            WinCanvas.SetActive(true);
        }
    }


    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры...");
        Application.Quit();
    }


    public void ReloadCurrentScene()
    {
       
        InfimaGames.LowPolyShooterPack.Character.playerAlive = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        PlayerCanvas.SetActive(true);
        DeathCanvas.SetActive(false);
        WinCanvas.SetActive(false);

        playerIsDead = false;
        playerHasWon = false;
    }

    public void TriggerWin()
    {
        playerHasWon = true;

        movementScript.enabled = false;
        PlayerCamera.SetActive(false);
        DeathCamera1.SetActive(true);

        InfimaGames.LowPolyShooterPack.Character.playerAlive = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
