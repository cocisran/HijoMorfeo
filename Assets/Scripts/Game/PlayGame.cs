using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void LoadGameScene()
    {
        
        SceneManager.LoadScene("SampleScene");
    }
}