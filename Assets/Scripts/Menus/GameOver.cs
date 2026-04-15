using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnRetryButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
