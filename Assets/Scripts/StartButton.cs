using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void LoadScene(string sceneLoaded)
    {
        SceneManager.LoadScene(sceneLoaded);
    }
}
