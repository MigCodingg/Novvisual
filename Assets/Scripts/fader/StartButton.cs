using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
   

    public void StartGame()
    {
        StartCoroutine(LoadNextSceneWithFade());
    }

     private IEnumerator LoadNextSceneWithFade()
    {
        yield return FadeManager.Instance.FadeOut();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}