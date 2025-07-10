using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitCanvasManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text curNumTeams;
    [SerializeField] private Slider numTeamsSlider;
    [SerializeField] private TMP_Dropdown categoryContainer;
    [SerializeField] private TMP_Text gameRuleTextContainer;
    [SerializeField] private Slider gameRuleSlider;
    [SerializeField] private TMP_Text curGameRule;

    private void Start()
    {
        numTeamsSlider.onValueChanged.AddListener(delegate { UpdateCurrentNumTeams(); });
        gameRuleSlider.onValueChanged.AddListener(delegate { UpdateCurrentGameRule(); });
    }

    public void UpdateCurrentNumTeams()
    {
        curNumTeams.text = numTeamsSlider.value.ToString();
    }

    public void UpdateCurrentGameRule()
    {
        curGameRule.text = gameRuleSlider.value.ToString();
    }

    public void StartGame()
    {
        //clean up this call 
        gameManager.SetUpGame(numTeamsSlider.value, categoryContainer.value, gameRuleSlider.value, categoryContainer.value switch
        {
            0 => Words.Animals,
            1 => Words.Professions,
            2 => Words.Sport,
            3 => Words.Fables,
            _ => ""
        }
         );
        gameObject.SetActive(false);
    }
}
