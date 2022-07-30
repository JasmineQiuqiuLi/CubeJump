using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndPanel : MonoBehaviour
{
    Text text_endScore,text_endDiamondCount, text_bestScore;
    Button btn_tryAgain, btn_mainMenu, btn_rankButton;

    GameObject bestScoreIcon;
    

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        SetDiamondCountText();
        ShowBestScore();
    }

    void SetDiamondCountText()
    {
        text_endScore.text = GamePanel.Score.ToString();
        text_endDiamondCount.text = "+ " + (GamePanel.DiamondCount / 2).ToString();
    }

    private void Init()
    {
        text_endScore = GameObject.Find("EndScore").GetComponent<Text>();
        text_endDiamondCount = GameObject.Find("EndDiamondCount").GetComponent<Text>();
        text_bestScore = GameObject.Find("BestScore").GetComponent<Text>();

        btn_tryAgain = GameObject.Find("TryAgain").GetComponent<Button>();
        btn_mainMenu = GameObject.Find("MainButton").GetComponent<Button>();
        btn_rankButton = GameObject.Find("RankButton").GetComponent<Button>();

        bestScoreIcon = GameObject.Find("BestScoreIcon");
        bestScoreIcon.SetActive(false);


        btn_tryAgain.onClick.AddListener(HandleTryAgain);
        btn_mainMenu.onClick.AddListener(HandleMainMenu);
        btn_rankButton.onClick.AddListener(HandleRankPanel);
    }

    void HandleTryAgain()
    {
        AudioManager.instance.PlayButtonSound();
        SceneManager.LoadScene("Game");
    }

    void HandleMainMenu()
    {
        AudioManager.instance.PlayButtonSound();
        SceneManager.LoadScene("Main");
    }

    void HandleRankPanel()
    {
        AudioManager.instance.PlayButtonSound();
        GameDataController.instance.PrePage = Page.End;
        SceneManager.LoadScene("Rank");
      
    }

    void ShowBestScore()
    {
        if (GameDataController.instance.data != null)
        {
            int bestScore = GameDataController.instance.data.BestScoreArray[0];
            text_bestScore.text = "Best Score: "+bestScore.ToString();

            if (GamePanel.IsNewScore)
            {
                bestScoreIcon.SetActive(true);
            }
        }
    }
}
