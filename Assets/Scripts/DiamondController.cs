using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GamePanel.instance.UpdateDiamonDCount();
            AudioManager.instance.PlayDiamondSound();
            Destroy(gameObject);


        }
    }

}



