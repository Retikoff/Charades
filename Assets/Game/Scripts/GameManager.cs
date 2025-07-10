using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RoundCanvasManager roundCanvasManager;
    [SerializeField] private GameObject endCanvas;
    private string[] words;
    private int numOfTeams;
    private int numOfCurrentTeam = 1;
    private int gameRuleAmount;
    private Dictionary<int, int> teamsResults = new();

    public void SetUpGame(float numOfTeams, int wordCategory, float gameRuleAmount, string wordsString)
    {
        this.numOfTeams = (int)numOfTeams;
        this.gameRuleAmount = (int)gameRuleAmount;
        FillWords(wordsString);

        roundCanvasManager.transform.gameObject.SetActive(true);
        roundCanvasManager.InitRound(words, this.gameRuleAmount, numOfCurrentTeam);
    }

    private void FillWords(string wordsString)
    {
        words = wordsString.Split(" ");
    }

    public void StartAnotherRound(int correctAnswers)
    {
        teamsResults[numOfCurrentTeam] = correctAnswers;

        numOfCurrentTeam++;

        if (numOfCurrentTeam > numOfTeams)
        {
            EndGame();
        }
        else
        {
            roundCanvasManager.InitRound(words, gameRuleAmount, numOfCurrentTeam);
        }
    }

    private void EndGame()
    {
        endCanvas.SetActive(true);
        var winnerTeam = FindTeamWithMaxScore();
        endCanvas.GetComponent<EndCanvasManager>().SetUpCanvas(winnerTeam, teamsResults[winnerTeam]);
        Debug.Log("Game Ended");
    }

    private int FindTeamWithMaxScore()
    {
        int max = -10;
        int teamNumber = 0;
        foreach (var team in teamsResults)
        {
            if (team.Value > max)
            {
                max = team.Value;
                teamNumber = team.Key;
            }
        }

        return teamNumber;
    }
}
