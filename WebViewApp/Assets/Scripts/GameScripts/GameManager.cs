using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI panels")]
    [SerializeField] private GameObject Win;
    [SerializeField] private GameObject Lose;
    [SerializeField] private TextMeshProUGUI playerCountPoints;
    [SerializeField] private TextMeshProUGUI goalkeeperCountPoints;
    [Header("Links to")]
    [SerializeField] private BallMove ballMove;
    [SerializeField] private MoveGoalKeeper goalKeeper;
    private bool isGame; public bool IsGame => isGame;

    private int pointPlayer;
    private int pointGoalkeeper;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        pointGoalkeeper = 0;
        pointPlayer = 0;

        playerCountPoints.text = "0";
        goalkeeperCountPoints.text = "0";

        isGame = true;
    }

    public void WinGame()
    {
        AudioManager.Instance?.PlaySound(AudioManager.Sounds.Goal);

        isGame = false;

        pointPlayer++;
        GetPoint(playerCountPoints, pointPlayer);

        goalKeeper.NextLevel();

        Win.SetActive(true);
    }

    public void LoseGame()
    {
        AudioManager.Instance?.PlaySound(AudioManager.Sounds.Miss);

        isGame = false;

        pointGoalkeeper++;
        GetPoint(goalkeeperCountPoints, pointGoalkeeper);

        Lose.SetActive(true);
    }

    private void GetPoint(TextMeshProUGUI text, int point)
    {
        text.text = $"{point}";
    }

    public void Replay()
    {
        isGame = true;
        ballMove.ResetGame();
    }

    public void Close()
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        ballMove.ResetGame();
        Win.SetActive(false);
        Lose.SetActive(false);
    }
}
