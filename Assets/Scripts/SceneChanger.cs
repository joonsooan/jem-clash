using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadOption()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadMapSelect()
    {
        SceneManager.LoadScene(3);
    }
}