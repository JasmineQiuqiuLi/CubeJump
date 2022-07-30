using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip button,diamond,fall,hit,jump;

    //static bool isLaunched = false;

    public static AudioManager instance;

    

    private void Awake()
    {
        //if (!isLaunched)
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //    isLaunched = true;
            
        //}

        //instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayButtonSound()
    {  
            audioSource.PlayOneShot(button);
    }

    public void PlayDiamondSound()
    {
        
        audioSource.PlayOneShot(diamond);

    }

    public void PlayFallSound()
    {
        
        audioSource.PlayOneShot(fall);
           
    }

    public void PlayHitSound()
    {

       audioSource.PlayOneShot(hit);

    }

    public void PlayJumpSound()
    {

        audioSource.PlayOneShot(jump);

    }

    public void SetAudioSourceMute(bool status)
    {
        audioSource.mute = status;
    }
}
