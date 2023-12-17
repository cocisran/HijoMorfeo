using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour {
    public void LoadGameScene() {

        SceneManager.LoadScene("TitleScreen");
    }
}