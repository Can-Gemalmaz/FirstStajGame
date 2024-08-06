using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameSettings;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI scoreBoard;
    [SerializeField] TextMeshProUGUI candyBoard;
    [SerializeField] Button playAgainButton;
    [SerializeField] Button quitButton;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerSpawnPoint;

    int candyCount;
    PlayerMovement player;

    int bestScore;
    private void Awake()
    {
        playAgainButton.onClick.AddListener(() =>
        {
           gameSettings.SetActive(false);
           GameObject playerGameObject = Instantiate(playerPrefab, playerSpawnPoint);
            player = playerGameObject.GetComponent<PlayerMovement>();
            player.OnPlayerDead += OnPlayerDeadEvent;
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        
    }

    // Start is called before the first frame update
    void Start()
    {
        bestScore = 0;
        if(!player)
        { 
             player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); 
        }

        player.OnPlayerDead += OnPlayerDeadEvent;
        //Load();
        LoadPrefs();
    }

    private void OnPlayerDeadEvent(int obj, int candy)
    {
        candyCount = candy;
        gameSettings.SetActive(true);
        finalScoreText.text = ("Final Score: " + obj);
        candyBoard.text = ("Total Candy: " + candyCount);
        if(bestScore < obj)
        {
            bestScore = obj;
            //Save();
            SavePrefs();
        }
        else
        {
            bestScoreText.text = ("Best Score: " + bestScore);
        }

        player.OnPlayerDead -= OnPlayerDeadEvent;
    }

    // Update is called once per frame
    void Update()
    {
        scoreBoard.text = ("Score: " + player.GetTurnScore().ToString());

        bestScoreText.text = ("Best Score: " + bestScore);

    }

    private void SavePrefs()
    {
        PlayerPrefs.SetInt("int_bestScore", bestScore);
    }

    private void LoadPrefs()
    {
        bestScore = PlayerPrefs.GetInt("int_bestScore", 0);
    }
}
