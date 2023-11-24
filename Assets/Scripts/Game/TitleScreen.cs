using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen: MonoBehaviour
{
    public void LoadTitleScreen()
    {

        SceneManager.LoadScene("TitleScreen");
    }
}