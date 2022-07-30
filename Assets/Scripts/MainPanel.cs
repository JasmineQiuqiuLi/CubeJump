using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanel : MonoBehaviour
{
    private Button btn_start,btn_shop, btn_rank, btn_volume, btn_reset,btn_exit;
    
    private void Start()
    {
        Init();
    }

    void Init()
    {
        btn_start = GameObject.Find("StartButton").GetComponent<Button>();
        btn_start.onClick.AddListener(OnStartButtonClick);
        

        btn_shop = GameObject.Find("ShopButton").GetComponent<Button>();
        btn_shop.onClick.AddListener(OnShopButtonClick);
       

        btn_rank = GameObject.Find("RankButton").GetComponent<Button>();
        btn_rank.onClick.AddListener(OnRankButtonClick);

        btn_volume = GameObject.Find("VolumeButton").GetComponent<Button>();
        btn_volume.onClick.AddListener(OnVolumButtonClick);

        btn_reset = GameObject.Find("ResetButton").GetComponent<Button>();
        btn_reset.onClick.AddListener(OnResetButtonClick);

        btn_exit = GameObject.Find("Exit").GetComponent<Button>();
        btn_exit.onClick.AddListener(QuitGame);

        ReadSkinData();

    }

    void ReadSkinData()
    {
        GameData data = GameDataController.instance.data;
        if (data != null)
        {
           Image skinImage= btn_shop.gameObject.transform.GetChild(0).GetComponent<Image>();
           skinImage.sprite = GameDataController.instance.skinSpritesForward[data.SelectedSkinIndex];
        }
        else
        {
            Debug.Log("the gamedata is null");
        }
        
    }
    void OnStartButtonClick()
    {
        AudioManager.instance.PlayButtonSound();
        SceneManager.LoadScene("Game");
    }

 
    void OnShopButtonClick()
    {
        AudioManager.instance.PlayButtonSound();
        SceneManager.LoadScene("Shop");
    }

    void OnRankButtonClick()
    {
        AudioManager.instance.PlayButtonSound();
        GameDataController.instance.PrePage = Page.Main;
        SceneManager.LoadScene("Rank");
    }

    void OnVolumButtonClick()
    {
        AudioManager.instance.PlayButtonSound();

        
    }

    void OnResetButtonClick()
    {
        AudioManager.instance.PlayButtonSound();
        GameDataController.instance.ResetGameData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        GamePanel.DiamondCount = 0;
        GamePanel.Score = 0;

    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
