using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 offset;
    Vector2 velocity;
    

    private void Start()
    {
        offset =  PlayerController.instance.transform.position- transform.position;
    }

    private void FixedUpdate()
    {
        if (PlayerController.instance != null)
        {
            Vector3 PlayerPos = PlayerController.instance.transform.position;
            float posX = Mathf.SmoothDamp(transform.position.x, PlayerPos.x - offset.x, ref velocity.x, 0.05f);
            float posY = Mathf.SmoothDamp(transform.position.y, PlayerPos.y - offset.y, ref velocity.y, 0.05f);

            if (posY > transform.position.y)
                transform.position = new Vector3(posX, posY, transform.position.z);
        }
        

    }
}
