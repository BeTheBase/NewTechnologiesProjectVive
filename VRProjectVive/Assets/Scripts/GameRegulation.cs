using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRegulation : MonoBehaviour
{

    private void OnEnable()
    {
        EventManager.OnGameEndedCaller += PlayerDetected;
    }

    private void OnDisable()
    {
        EventManager.OnGameEndedCaller -= PlayerDetected;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayerDetected()
    {
        SceneManager.LoadScene(1);
    }
}
