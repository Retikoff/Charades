using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCanvasManager : MonoBehaviour
{
    [SerializeField] private TMP_Text winnerContainer;
    [SerializeField] private TMP_Text scoreContainer;

    public void SetUpCanvas(int teamNumber, int teamScore)
    {
        winnerContainer.text = "Команда №" + teamNumber;
        scoreContainer.text = "набрав " + teamScore + " очков";
    }
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
