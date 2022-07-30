using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class GamePanel : MonoBehaviour
{
    Button btn_play, btn_pause;
    Text text_score, text_diamondCount;

    public Sprite[] backGroundSprites;

    static int score, diamondCount;
    public static int Score { get { return score; } set { score = value; } }
    public static int DiamondCount { get { return diamondCount; } set { diamondCount = value; } }

    public static GamePanel instance;

    static bool isNewScore;
    public static bool IsNewScore { get { return isNewScore; } }

    private void Awake()
    {
        InitTopBar();
        InitBackGround();
        instance = this;
        score = 0;
        isNewScore = false;
    }

    private void Start()
    {
        InitDiamondCount();

    }



    private void InitTopBar()
    {
        btn_play = GameObject.Find("Play").GetComponent<Button>();
        btn_play.onClick.AddListener(OnPlayButtonClick);

        btn_pause = GameObject.Find("Pause").GetComponent<Button>();
        btn_pause.onClick.AddListener(OnPauseButtonClick);

        text_score = GameObject.Find("Score").GetComponent<Text>();
        text_diamondCount = GameObject.Find("DiamondCount").GetComponent<Text>();

        btn_play.gameObject.SetActive(false);
    }

    void InitBackGround()
    {
        int backgroundIndex = UnityEngine.Random.Range(0, backGroundSprites.Length);
        Image backgroundImage = GameObject.Find("Background").GetComponent<Image>();
        backgroundImage.sprite = backGroundSprites[backgroundIndex];
    }

    void OnPlayButtonClick()
    {
        AudioManager.instance.PlayButtonSound();
        btn_play.gameObject.SetActive(false);
        btn_pause.gameObject.SetActive(true);

        //continue the game
        Time.timeScale = 1;
    }

    void OnPauseButtonClick()
    {
        AudioManager.instance.PlayButtonSound();
        btn_play.gameObject.SetActive(true);
        btn_pause.gameObject.SetActive(false);

        //pause the game
        Time.timeScale = 0;
    }

    public void AddScore()
    {
        score++;
        text_score.text = score.ToString();
    }

    void InitDiamondCount()
    {
        if (GameDataController.instance.data != null)
        {
            diamondCount = GameDataController.instance.data.DiamondCount;
        }
        else
        {
            diamondCount = 0;
        }

        text_diamondCount.text = (diamondCount / 2).ToString();
    }

    public void UpdateDiamonDCount()
    {
        diamondCount++;
        UpdateDiamondCountUI();
    }

    void UpdateDiamondCountUI()
    {
        text_diamondCount.text = (diamondCount/2) .ToString();
    }

    public static void DecreaseDiamondCount(int amount)
    {
        diamondCount = Mathf.Clamp(diamondCount - amount, 0, diamondCount);
    }

    private void OnDestroy()
    {
        SaveDiamondCount();
        SaveBestScore();
    }

    void SaveDiamondCount()
    {
        if (GameDataController.instance.data != null)
        {
            GameDataController.instance.data.DiamondCount = diamondCount;
            GameDataController.instance.Save();
        }
    }

    void SaveBestScore()
    {
        if (GameDataController.instance.data != null) 
        {
            List<int> bestScoreList = GameDataController.instance.data.BestScoreArray.ToList();

            if (score > bestScoreList[0]) //current score is larger than the previous No.1 score
            {   
                
                isNewScore = true;
            }

            if (score > bestScoreList[bestScoreList.Count - 1]) //Excute only if the current score larger than the smallest one in the bestScoreArray
            {  
                bestScoreList.Add(score);
                bestScoreList.Sort();
                bestScoreList.Reverse(); //order the array in descending order.

                bestScoreList.Remove(bestScoreList.Min());

                GameDataController.instance.data.BestScoreArray = bestScoreList.ToArray();

                GameDataController.instance.Save();


            }
            
            
        }
    }

}
