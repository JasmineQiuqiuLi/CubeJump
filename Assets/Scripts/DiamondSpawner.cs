using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    public GameObject diamondPrefab;

    
    public void SpawnDiamond(Transform transform,Vector3 platformPos)
    {
        int index = Random.Range(0, 10);
        if (index == 1)
        {
            GameObject newDiamond = Instantiate(diamondPrefab, transform);
            newDiamond.transform.localPosition = new Vector3(platformPos.x, platformPos.y + 0.5f, platformPos.z);
        }
    }
}
