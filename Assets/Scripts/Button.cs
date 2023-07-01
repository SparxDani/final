using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void LoadScene(string sceneLoaded)
    {
        SceneManager.LoadScene(sceneLoaded);
    }
}
