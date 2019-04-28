using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenySceneManager : MonoBehaviour
{

    public void StartGameButton()
    {
        SceneManager.LoadScene("ToebsLuksusScene");
        SceneManager.UnloadSceneAsync("StartMenu");

    }

}
