using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    GameObject player;

    private void Awake()
    {
        player = Instantiate(playerPrefab);
        player.transform.position = new Vector3(0.0f, -2.3f, 90);

    }

    private void Start()
    {
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();

        if (GameDataController.instance.data != null)
        {
            spriteRenderer.sprite = GameDataController.instance.skinSpritesBack[GameDataController.instance.data.SelectedSkinIndex];
        }
        else
        {
            Debug.Log("cannot read GameData");
        }
        

    }
}
