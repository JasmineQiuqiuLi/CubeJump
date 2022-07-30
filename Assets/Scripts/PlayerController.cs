using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    bool isMoveLeft = false;
    private Vector3 nextPlatformPosLeft;
    private Vector3 nextPlatformPosRight;
    bool isJump = false;

    public static PlayerController instance;

    public Transform rayDown;
    public LayerMask platformLayer;

    public static event Action OnPlayerLose;

    public ParticleSystem DeathEffect;

    bool lose = false;//make sure the OnPlayerLose only called once.

    GameObject lastPlatform;

    
    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        OnPlayerLose += HandlePlayerLose;
    }

    private void OnDisable()
    {
        OnPlayerLose -= HandlePlayerLose;
    }

    private void Update()
    {
        if (IsPointerOverGameObject(Input.mousePosition)) return;
        if (GameManager.instance.IsGameOver) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0) && !isJump)
        {
            isJump = true;
            Vector3 mousePos = Input.mousePosition;

            isMoveLeft = (mousePos.x <= Screen.width/2) ? true : false;

            Jump();

            
            PlatformSpawner.instance.DecidePath();
            
        }

        if (isJump)
        {
            //if the player cannot detect a regular platform underneath when the player is jumping, then the player loses the game.
            //it includes two scenerios: (1) when the player Step on the air; or (2) the player step on obstacle platforms.
            if (!IsRayDownPlatform() && !lose)
            {
                lose = true;
                OnPlayerLose.Invoke();
            }
        }

        if(transform.position.y-Camera.main.transform.position.y<-5 && !lose)
        {
            lose = true;
            AudioManager.instance.PlayFallSound();
            OnPlayerLose.Invoke();
            
        }

    }


    void Jump()
    {
        //change the charater orientation of the avatar;
        transform.localScale = isMoveLeft ? new Vector3(-1, 1, 1) : Vector3.one;

        Vector3 nextPlatformPos = isMoveLeft ?  nextPlatformPosLeft: nextPlatformPosRight;

        transform.DOMoveX(nextPlatformPos.x, 0.2f);
        transform.DOMoveY(nextPlatformPos.y+0.8f, 0.15f);

       AudioManager.instance.PlayJumpSound();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            isJump = false;
            Vector3 currentPlatformPos = collision.gameObject.transform.position;
            
            nextPlatformPosLeft = new Vector3(currentPlatformPos.x -PlatformSpawner.instance.NextXPos, currentPlatformPos.y + PlatformSpawner.instance.NextYPos, currentPlatformPos.z);
            nextPlatformPosRight = new Vector3(currentPlatformPos.x + PlatformSpawner.instance.NextXPos, currentPlatformPos.y + PlatformSpawner.instance.NextYPos, currentPlatformPos.z);

            //check if this is a new platform
            if (collision.gameObject == lastPlatform) return;
            else
            {
                if (lastPlatform != null && !GameManager.instance.IsGameOver)
                {
                    GamePanel.instance.AddScore();
                }
                lastPlatform = collision.gameObject;
            }

        }
    }

    bool IsRayDownPlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayDown.position,Vector2.down,1f,platformLayer);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Platform") return true;

            if(hit.collider.tag=="Obstacle")
            {
                
                AudioManager.instance.PlayHitSound();
                Instantiate(DeathEffect, new Vector3(transform.position.x, transform.position.y, DeathEffect.transform.position.z), DeathEffect.transform.rotation);
                return false;
            }
        }
        
        AudioManager.instance.PlayFallSound();
        return false;

    }

    void HandlePlayerLose()
    {
        
        SpriteRenderer spriteRender = gameObject.GetComponent<SpriteRenderer>();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        Invoke("LoadEndScene",1.0f);

        
    }

    void LoadEndScene()
    {
        SceneManager.LoadScene("End");
    }


    private bool IsPointerOverGameObject(Vector2 mousePosition)
    {
        
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
       
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults.Count > 0;
    }
}
