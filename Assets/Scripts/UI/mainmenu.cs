using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class mainmenu : MonoBehaviour
{
    public float delay = 0f; 

  

    public void ExitGame()
    {
        Debug.Log("Ha salido del juego");
        Application.Quit();
    }
}