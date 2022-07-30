using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{

    bool isVolumeOn;
    int[] bestScoreArray;
    int selectedSkinIndex;
    bool[] skinUnlocked;
    int diamondCount;

    public GameData(bool isMusicOn, int[] bestScoreArray, int selectedSkinIndex, bool[] skinUnlocked, int diamondCount)
    {
        this.isVolumeOn = isMusicOn;
        this.bestScoreArray = bestScoreArray;
        this.selectedSkinIndex = selectedSkinIndex;
        this.skinUnlocked = skinUnlocked;
        this.diamondCount = diamondCount;
    }

    public bool IsMusicOn { get { return isVolumeOn; } set { isVolumeOn=value; } }
    public int[] BestScoreArray { get { return bestScoreArray; } set { bestScoreArray = value; } }

    public int SelectedSkinIndex { get { return selectedSkinIndex; } set { selectedSkinIndex = value; } }

    public bool[] SkinUnlocked { get { return skinUnlocked; } set { skinUnlocked = value; } }

    public int DiamondCount { get { return diamondCount; } set { diamondCount = value; } }



}
