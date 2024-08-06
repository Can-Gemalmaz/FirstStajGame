using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event Action<int, int> OnPlayerDead;

    [SerializeField] float velocity = 2f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] LayerMask obstacle;
    ObstacleManager obstacleManager;
    CandyManager candyManager;

    Rigidbody2D playerRigidBody;
    BoxCollider2D boxCollider2D;

    Vector2 linearVelocity;
    int turns = 0;

    private void Awake()
    {
        obstacleManager = GameObject.FindWithTag("ObstacleManager").GetComponent<ObstacleManager>();
        candyManager = obstacleManager.GetComponent<CandyManager>();

    }

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        boxCollider2D = GetComponent<BoxCollider2D>();

        linearVelocity = new Vector2(velocity, 0);
        playerRigidBody.AddForce(linearVelocity);
        
        obstacleManager.ShuffleRightObstacles(0);
        candyManager.SpawnRightCandy();
    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Vector2 jumpDirection = new Vector2(0, jumpForce);
            playerRigidBody.AddForce(jumpDirection);
        }
        else
        {            
            //playerRigidBody.velocity = new Vector2(velocity, 0);
        }
        
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Obstacle")
        {
            OnPlayerDead?.Invoke(turns, candyManager.GetCandyCount());
            obstacleManager.DeactivateRightObstacles();
            obstacleManager.DeactivateLeftObstacles();
            candyManager.DeSpawnCandy(null);
            Destroy(gameObject);

        }
        if (collision.gameObject.tag == "Wall")
        {
            turns++;
            if (turns % 2 == 1)
            {
                playerRigidBody.AddForce(-linearVelocity);
                obstacleManager.ShuffleLeftObstacles(turns);
                obstacleManager.DeactivateRightObstacles();
            }
            else 
            {
                playerRigidBody.AddForce(linearVelocity);
                obstacleManager.ShuffleRightObstacles(turns);
                obstacleManager.DeactivateLeftObstacles();
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (turns % 2 == 0)
        {
            //Left turn
            candyManager.DeSpawnCandy(collision.gameObject);
            candyManager.SpawnLeftCandy();
            candyManager.SetCandyCount(1);

        }
        else
        {
            //Right turn
            candyManager.DeSpawnCandy(collision.gameObject);
            candyManager.SpawnRightCandy();
            candyManager.SetCandyCount(1);

        }
    }

    public int GetTurnScore()
    { 
        return turns; 
    }

}
