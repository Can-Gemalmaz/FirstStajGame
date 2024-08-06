using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] GameObject[] rightObstacles;
    [SerializeField] GameObject[] leftObstacles;
    [SerializeField] int[] obstacleNumbers;
    [SerializeField] int[] obstacleChangeInTurn;

    private List<int> randomPlaces;



    public void ShuffleRightObstacles(int turn)
    {
        ShuffleObstaclesBySides(turn, rightObstacles);

    }



    public void ShuffleLeftObstacles(int turn)
    {
        ShuffleObstaclesBySides(turn, leftObstacles);
    }

    private void ShuffleObstaclesBySides(int turn, GameObject[] side)
    {
        for (int i = 0; i < obstacleChangeInTurn.Length; i++)
        {
            if (turn >= obstacleChangeInTurn[obstacleChangeInTurn.Length - 1])
            {
                RandomNumberGenerator(side.Length, obstacleNumbers[obstacleNumbers.Length - 1]);
                foreach (int number in randomPlaces)
                {
                    SetObstaclesActive(side[number]);
                }
                return;
            }
            else if (turn >= obstacleChangeInTurn[i] && turn < obstacleChangeInTurn[i + 1])
            {
                RandomNumberGenerator(rightObstacles.Length, obstacleNumbers[i]);
                foreach (int number in randomPlaces)
                {
                    SetObstaclesActive(side[number]);
                }
                return;
            }
        }
    }

    public void DeactivateRightObstacles()
    {
        foreach (GameObject rightObject in rightObstacles)
        {
            SetObstacleNonActive(rightObject);
        }
    }
    public void DeactivateLeftObstacles()
    {
        foreach (GameObject leftObject in leftObstacles)
        {
            SetObstacleNonActive(leftObject);
        }
    }

    //Do a non-reapeter random number generator
    private void RandomNumberGenerator(int range,int length)
    {
        //randomizing the list with non repatitive numbers
        int counter = 0;

        randomPlaces = new List<int>();
        randomPlaces.Clear();
        while (counter < length) 
        {
            int randomInt = Random.Range(0, range);
            if (!randomPlaces.Contains(randomInt))
            {
                randomPlaces.Add(randomInt);
                counter++;
            }
        }
    }

    private void SetObstaclesActive(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    private void SetObstacleNonActive(GameObject gameObject)
    { 
        gameObject.SetActive(false);
    }

}
