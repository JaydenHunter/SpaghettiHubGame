using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Coder of code: Keira

public class MainMenuScript : MonoBehaviour
{

    public void GoToMainScene(float joyDistance)
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame(float joyDistance)
    {
        Application.Quit();
    }

}

