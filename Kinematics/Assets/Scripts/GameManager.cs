using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        TargetPlayer.playerDead += ReturnToMainMenu;
        BaseManager.gameOver += ReturnToMainMenu;
    }

    private void ReturnToMainMenu() => SceneManager.LoadScene(0);
    private void PauseGame() => Time.timeScale = 0;
}
