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
        SaveSystem.Init();
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
        Load();
    }

    private void OnPlayerDeadEvent(int obj, int candy)
    {
        candyCount = candy;
        gameSettings.SetActive(true);
        finalScoreText.text = ("Final Score: " + obj);
        candyBoard.text = ("Total Candy: " + candyCount);
        if(bestScore < obj)
        {
            Debug.Log("BestScore before save: " + bestScore);
            bestScore = obj;
            Save();
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

    private void Save()
    {
        SaveObject saveObject = new SaveObject {
            scoreAmount = bestScore,
        };

        string json = JsonUtility.ToJson(saveObject);

        SaveSystem.Save(json);
    }

    private void Load()
    {
        string saveString = SaveSystem.Load();
        if(saveString != null) 
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            bestScore = saveObject.scoreAmount;
            Debug.Log("SavedObjectBestScore"+ bestScore);
            Debug.Log("SavedObjectSavedOne" + saveObject.scoreAmount);

        }
        else 
        {
            Debug.Log("No save");
        }


    }



    private class SaveObject
    {
        public int scoreAmount;

    }
}
