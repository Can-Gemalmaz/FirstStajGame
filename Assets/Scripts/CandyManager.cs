using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    [SerializeField] GameObject[] rightCandys;
    [SerializeField] GameObject[] leftCandys;

    GameObject activeRightCandy;
    GameObject activeLeftCandy;

    int candyCount;

    public void SpawnRightCandy()
    {
        activeRightCandy = rightCandys[Random.Range(0,rightCandys.Length)];
        activeRightCandy.SetActive(true);
    }

    public void SpawnLeftCandy() 
    {
        activeLeftCandy = leftCandys[Random.Range(0, leftCandys.Length)];
        activeLeftCandy.SetActive(true);
    }

    public void DeSpawnCandy(GameObject candy) 
    {
        if (candy) 
        {
            candy.SetActive(false);
        }
        activeLeftCandy?.SetActive(false);
        activeRightCandy?.SetActive(false);
    }


    public void SetCandyCount(int candyIncrease)
    { 
        candyCount += candyIncrease; 
    }

    public int GetCandyCount()
    { 
        return candyCount; 
    }
}
