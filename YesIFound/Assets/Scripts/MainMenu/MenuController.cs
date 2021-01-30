using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject cam; 
    private Animator camAnim;

    private void Awake()
    {
        camAnim = cam.GetComponent<Animator>();
    }

    public void PlayGame()
    {
        
        StartCoroutine(CamTrigger());
    }

    public void QuitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    private IEnumerator CamTrigger()
    {
        yield return new WaitForSeconds(4f);
        camAnim.SetBool("isStartCamera", true);
        Debug.Log("Play");

        yield return null;
        camAnim.SetBool("isStartCamera", false);
    }

}
