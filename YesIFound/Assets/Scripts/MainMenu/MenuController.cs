using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject cam;
    public GameObject cam2;
    public GameObject nextWaveText;

    public GameObject train;
    private Animator camAnim;

    public PlayerAtribsSO playerSO;

    public CamAnimEnd camAlert;

    public GameObject gameplayCanvas;
    public GameplayUI gameplayUI;

    public PlayerMovement playerMoviment;

    public WaveManager waveManager;

    private void Awake()
    {
        camAnim = cam.GetComponent<Animator>();
        camAlert = cam.GetComponent<CamAnimEnd>();
        gameplayUI = GetComponent<GameplayUI>();
    }

    private void Update()
    {
        if (camAlert.endAnim == true)
        {
            ChangeCamera();
            
            camAlert.endAnim = false;
        }
    }

    public void PlayGame()
    {
        
        StartCoroutine(CamTrigger());
        
    }

    public void PlayGame2()
    {

        StartCoroutine(CamTrigger2());

    }

    public void QuitGame()
    {
        //Debug.Log("Exit");
        Application.Quit();
    }

    private IEnumerator CamTrigger()
    {
        yield return new WaitForSeconds(6f);
        camAnim.SetBool("isStartCamera", true);
        waveManager.WaveStart();
        yield return null;
        camAnim.SetBool("isStartCamera", false);
    }

    private IEnumerator CamTrigger2()
    {
        nextWaveText.SetActive(true);
        waveManager.m_StartGame = false;
        waveManager.DestroyAllNpcs();
        yield return new WaitForSeconds(6f);
        camAnim.SetBool("isStartCamera", true);
        waveManager.m_StartGame = true;
        yield return null;
        camAnim.SetBool("isStartCamera", false);
        nextWaveText.SetActive(false);
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
        gameplayCanvas.SetActive(true);
        gameplayUI.timerIsRunning = true;
        playerMoviment.canMove = true;
        train.SetActive(false);
    }

}
