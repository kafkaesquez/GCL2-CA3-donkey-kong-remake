using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void OnCreditsClick()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnBackClick()
    {
        SceneManager.LoadScene("StartScene");
    }

}
