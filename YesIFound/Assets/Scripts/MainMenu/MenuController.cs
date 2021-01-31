using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject cam;
    public GameObject cam2;
    private Animator camAnim;

    public PlayerAtribsSO playerSO;

    public CamAnimEnd camAlert;

    public GameObject gameplayUI;

    private void Awake()
    {
        camAnim = cam.GetComponent<Animator>();
        camAlert = cam.GetComponent<CamAnimEnd>();
    }

    private void Update()
    {
        if (camAlert.endAnim == true)
            ChangeCamera();
    }

    public void PlayGame()
    {
        
        StartCoroutine(CamTrigger());
    }

    public void QuitGame()
    {
        //Debug.Log("Exit");
        Application.Quit();
    }

    private IEnumerator CamTrigger()
    {
        yield return new WaitForSeconds(4f);
        camAnim.SetBool("isStartCamera", true);
        //Debug.Log("Play");

        yield return null;
        camAnim.SetBool("isStartCamera", false);
    }

    public void ChangeMale()
    {
        playerSO.Gender = "Male";
    }

    public void ChangeFemale()
    {
        playerSO.Gender = "Female";
    }

    public void ChangeCamera()
    {
        cam.SetActive(false);
        cam2.SetActive(true);
        gameplayUI.SetActive(true);
    }

}
