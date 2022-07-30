using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public float falltime = 2.0f;
    

    private void Awake()
    {
        Invoke("Fall", falltime);
    }

    void Fall()
    {
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        
        //the gravityScale of player is set to 1, so the platform falls faster, which call the ontriggerexit2D on playerController script
        // to trigger the event: playerLose.
        rb2D.gravityScale = 1.1f;
        DestroyPlatform();

    }

    void DestroyPlatform()
    {
        Destroy(gameObject,1.5f);
    }

}
