using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenySceneManager : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;

    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            SceneManager.LoadScene("ToebsLuksusScene");
            SceneManager.UnloadSceneAsync("ArtScene_Niclas003");

        }
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene("ToebsLuksusScene");
        SceneManager.UnloadSceneAsync("ArtScene_Niclas003");

    }

}
