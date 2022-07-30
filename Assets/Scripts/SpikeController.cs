using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    SpriteRenderer spriteRend;
    PlatformSpawner platformSpawner;


    private void Awake()
    {
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        platformSpawner = transform.GetComponentInParent<PlatformSpawner>();
    }

    private void Start()
    {
        if (platformSpawner != null)
        {
            Sprite platformSprite = platformSpawner.CurrPlatformPrefab.GetComponent<SpriteRenderer>().sprite;
            spriteRend.sprite = platformSprite;
        }
    }
}
