using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWave : MonoBehaviour
{
    public MenuController menuController;
    public GameObject camInitial;
    public GameObject camPlayer;
    public GameObject train;
    public GameplayUI gameplayUI;
    public PlayerMovement playerMovement;
    public GameObject player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ResetWaveMethod();
        }
    }

    public void ResetWaveMethod()
    {
        menuController.PlayGame();
        camInitial.SetActive(true);
        camPlayer.SetActive(false);
        train.SetActive(true);
        gameplayUI.timeRemaining = 120;
        gameplayUI.timerIsRunning = false;
        playerMovement.canMove = false;
        player.transform.position = new Vector3(0, 20.7f, 0);
        //reiniciar npcs e itens
    }

}
