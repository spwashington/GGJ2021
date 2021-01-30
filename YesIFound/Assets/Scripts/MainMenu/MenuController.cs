using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    
    public void PlayGame()
    {
        Debug.Log("Play");
    }

    public void QuitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

}
