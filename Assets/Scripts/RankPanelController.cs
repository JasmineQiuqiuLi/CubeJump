using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankPanelController : MonoBehaviour
{
    GameObject rankPanel;
    

    private void Awake()
    {
        rankPanel = gameObject.GetComponentInChildren<Image>().gameObject;
        
    }

     void Start()
    { 
        
        //fetch the rank data;
        if (GameDataController.instance.data != null)
        {
            int[] bestScoreArray = GameDataController.instance.data.BestScoreArray;
            GameObject rankGroup = GameObject.Find("RankGroup");

            int childCount=rankGroup.GetComponent<RectTransform>().childCount;


            for (int i = 0; i < childCount; i++)
            {
                rankGroup.GetComponent<RectTransform>().GetChild(i).GetComponentInChildren<Text>().text=bestScoreArray[i].ToString();

                
            }
        }

        RegisterEvent();
    }

    void RegisterEvent()
    {
        Button btn_Back = GameObject.Find("BackButton").GetComponent<Button>();
        btn_Back.onClick.AddListener(GoBackToPrePage);
    }
    
    

    public void GoBackToPrePage()
    {
        if (GameDataController.instance.PrePage == Page.End)
        {
            SceneManager.LoadScene("End");
        }
        else if (GameDataController.instance.PrePage == Page.Main)
        {
            SceneManager.LoadScene("Main");
        }
    }

}
