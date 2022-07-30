using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

public class VolumeController : MonoBehaviour
{
    public Sprite volumeOnIcon;
    public Sprite volumeOfficon;

    Button btn_volume;

    bool isVolumeOn;

    

    private void Awake()
    {
        btn_volume = GameObject.Find("VolumeButton").GetComponent<Button>();
        btn_volume.onClick.AddListener(ToggleVolume);
    }


    private void Start()
    {
        InitVolumeIcon();
    }

    public void ToggleVolume()
    {
        isVolumeOn = !isVolumeOn;
        
        SetVolumeIcon();

        GameDataController.instance.data.IsMusicOn=isVolumeOn;
        
        GameDataController.instance.Save();

        AudioManager.instance.SetAudioSourceMute(!isVolumeOn);

    }

    void InitVolumeIcon()
    {
        if (GameDataController.instance.data != null)
        {
            isVolumeOn = GameDataController.instance.data.IsMusicOn;
            SetVolumeIcon();
        }
        else
        {
            Debug.Log("the game data is null in gamedatacontroller");
        }
        
    }

    void SetVolumeIcon()
    {
        btn_volume.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = isVolumeOn ? volumeOnIcon : volumeOfficon;
    }
}
