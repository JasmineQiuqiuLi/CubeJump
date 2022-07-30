using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ShopPanel : MonoBehaviour
{
    RectTransform scrollContentParent; //the parent that host all the skin elements
    
    public GameObject skinSprite;
    GameObject selectSkin, buySkin; //button gameobjects
    GameObject message;

    public SkinSprite[] skinInfo;

    Text text_skinName;
    Text text_diamondPrice;
    Text text_diamondCount;

    Button btn_back;

    int currentSkinIndex;

    GameData data;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        InitSkinInfoArray();

        InitScrollRect();

        InitSelectBuyButton();

        InitSKinInfo();

        InitDiamondCount();

        SetSkinTone();

    }

    private void Init()
    {
        scrollContentParent = GameObject.Find("Parent").GetComponent<RectTransform>();

        text_skinName = GameObject.Find("SkinName").GetComponent<Text>();
        text_diamondPrice = GameObject.Find("DiamondPrice").GetComponent<Text>();
        text_diamondCount = GameObject.Find("EndDiamondCount").GetComponent<Text>();

        selectSkin = GameObject.Find("SelectSkin");
        buySkin = GameObject.Find("BuySkin");
        btn_back = GameObject.Find("BackButton").GetComponent<Button>();
        message = GameObject.Find("Message");
        message.SetActive(false);

        selectSkin.GetComponent<Button>().onClick.AddListener(HandleSelect);
        buySkin.GetComponent<Button>().onClick.AddListener(HandleBuy);

        btn_back.onClick.AddListener(HandleBackButtonClick);

    }
    private void InitSkinInfoArray()
    {
        //access GameData
        data = GameDataController.instance.data;
        for (int i = 0; i < skinInfo.Length; i++)
        {
            skinInfo[i].isBought = data.SkinUnlocked[i];
        }
    }
    private void InitScrollRect()
    {
        scrollContentParent.sizeDelta = new Vector2((skinInfo.Length + 2) * 160, 302);
        for(int i=0; i < skinInfo.Length; i++)
        {
            GameObject newSkinSprite = Instantiate(skinSprite, scrollContentParent);
            newSkinSprite.GetComponentInChildren<Image>().sprite= skinInfo[i].sprite;
            newSkinSprite.transform.localPosition = new Vector3((i + 1) * 160, 0, 0);
        }
    }

    void InitSelectBuyButton()
    {
        selectSkin.SetActive(true);
        buySkin.SetActive(false);
    }

    void InitSKinInfo()
    {
        if (skinInfo.Length != 0)
        {
            skinInfo[0].isBought = true;
            SetItemSize(0);
        }
    }

    void InitDiamondCount()
    {
        GamePanel.DiamondCount = GameDataController.instance.data.DiamondCount;
        UpdateDiamondCount();

    }

    void UpdateDiamondCount()
    {
        text_diamondCount.text = (GamePanel.DiamondCount/2).ToString();
       
    }

    private void Update()
    {
        
         currentSkinIndex = (int)Mathf.Round(scrollContentParent.transform.localPosition.x / -160.0f);

        if (Input.GetMouseButtonUp(0))
        {
            scrollContentParent.transform.DOLocalMoveX(currentSkinIndex * -160, 0.2f);
            
            SetItemSize(currentSkinIndex);
            UpdateName(currentSkinIndex);
            UpdateSelectBuyButton(currentSkinIndex);
        }
        

    }

    void SetItemSize(int index)
    {
        for (int i=0; i < scrollContentParent.childCount; i++)
        {
            if (index == i)
            {
                scrollContentParent.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
            }
            else
            {
                scrollContentParent.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
            }
        }
    }

    void UpdateName(int index)
    {
        text_skinName.text = skinInfo[index].name;
    }

    void UpdateCost(int index)
    {
        text_diamondPrice.text = skinInfo[index].cost.ToString();
    }

    void UpdateSelectBuyButton(int index)
    {
        if (skinInfo[index].isBought)
        {
            selectSkin.SetActive(true);
            buySkin.SetActive(false);
        }
        else
        {
            selectSkin.SetActive(false);
            buySkin.SetActive(true);

            UpdateCost(index);
        }
    }

    void HandleBackButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    void HandleSelect()
    {
        //set the player sprite as the newly select one
        //1. save the selected sprite
        SaveSelectedSprite();
        
        //go back to the menu page
        HandleBackButtonClick();
    }

    void HandleBuy()
    {
        int index=(int)Mathf.Round(scrollContentParent.transform.localPosition.x / -160.0f);

        if (GamePanel.DiamondCount >= skinInfo[index].cost)
        {
            skinInfo[index].isBought = true;
            GamePanel.DecreaseDiamondCount(skinInfo[index].cost*2);

            SetSpriteFliter(index,Color.white);

            UpdateDiamondCount();
            SaveDiamondCount();

            SaveSkinUnlocked(index);

            
        }
        else
        {
            message.SetActive(true);
            message.GetComponent<Image>().DOFade(255, 3.0f);
            StartCoroutine(SetMessageInactive());
 
        }
        
    }

    void SetSpriteFliter(int index,Color color)
    {
        scrollContentParent.GetChild(index).GetChild(0).GetComponent<Image>().color = color;
    }

    void SaveSelectedSprite()
    {
        GameData data = GameDataController.instance.data;
        data.SelectedSkinIndex = currentSkinIndex;
        GameDataController.instance.Save();
    }

    void SaveSkinUnlocked(int index)
    {
        GameData data = GameDataController.instance.data;
        data.SkinUnlocked[index] = true;
        GameDataController.instance.Save();
    }

    void SaveDiamondCount()
    {
        GameData data = GameDataController.instance.data;
        data.DiamondCount = GamePanel.DiamondCount;
        GameDataController.instance.Save();
    }

    void SetSkinTone()
    {
        //set the filter as gray;
        for (int i = 1; i < skinInfo.Length; i++)
        {
            if (skinInfo[i].isBought)
            {
                SetSpriteFliter(i, Color.white);
            }
            else
            {
                SetSpriteFliter(i, Color.gray);
            }
            
        }
    }

    IEnumerator SetMessageInactive()
    {
        yield return new WaitForSeconds(1.5f);
        message.GetComponent<Image>().DOFade(0, 1.5f);
        message.SetActive(false);
    }
}
