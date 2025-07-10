using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RoundCanvasManager roundCanvasManager;
    private string[] words;
    private int numOfTeams;

    public void SetUpGame(float numOfTeams, int wordCategory, float gameRuleAmount, string wordsString)
    {
        this.numOfTeams = (int)numOfTeams;
        Debug.Log(numOfTeams.ToString() + wordCategory.ToString()  + gameRuleAmount.ToString());
        FillWords(wordsString);

        roundCanvasManager.transform.gameObject.SetActive(true);
        //change somehow words array and start round through roundCanvasManager
        roundCanvasManager.StartRound(words);
    }

    private void FillWords(string wordsString)
    {
        words = wordsString.Split(" ");
    }
}
