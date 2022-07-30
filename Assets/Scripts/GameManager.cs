using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    bool isGameOver = false;
    public bool IsGameOver { get { return isGameOver; } }

    public static GameManager instance;


    private void OnEnable()
    {
        PlayerController.OnPlayerLose += SetGameOver;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerLose -= SetGameOver;
    }

    private void Awake()
    {
        instance = this;
        
    }

    public void SetGameOver()
    {
        isGameOver = true;
    }
}
