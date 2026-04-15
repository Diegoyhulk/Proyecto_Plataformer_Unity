using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnRetryButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnMenuButtonClicked()
    {
        Debug.Log("OnQuitButtonClicked");
    }
}
