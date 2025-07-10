using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitCanvasManager : MonoBehaviour
{
    [SerializeField] private TMP_Text curNumTeams;
    [SerializeField] private Slider numTeamsSlider;
    [SerializeField] private TMP_Dropdown categoryContainer;
    [SerializeField] private TMP_Text gameRuleTextContainer;
    [SerializeField] private Slider gameRuleSlider;
    [SerializeField] private TMP_Text curGameRule;
    [SerializeField] private Button wordAmountButton;
    [SerializeField] private Button timeButton;
    private GameRules gameRuleChoice = GameRules.Empty;

    private void Start()
    {
        numTeamsSlider.onValueChanged.AddListener(delegate { UpdateCurrentNumTeams(); });
        gameRuleSlider.onValueChanged.AddListener(delegate { UpdateCurrentGameRule(); });
        SwitchToWordAmount();
    }

    public void SwitchToWordAmount()
    {
        gameRuleChoice = GameRules.WordAmount;
        gameRuleTextContainer.text = "Выберите кол.во слов на раунд";
        wordAmountButton.GetComponentInChildren<TMP_Text>().color = Color.forestGreen;
        timeButton.GetComponentInChildren<TMP_Text>().color = Color.black;
    }

    public void SwitchToTime()
    {
        gameRuleChoice = GameRules.Time;
        gameRuleTextContainer.text = "Выберите время на раунд (мин)";
        timeButton.GetComponentInChildren<TMP_Text>().color = Color.forestGreen;
        wordAmountButton.GetComponentInChildren<TMP_Text>().color = Color.black;
    }

    public void UpdateCurrentNumTeams()
    {
        curNumTeams.text = numTeamsSlider.value.ToString();
    }

    public void UpdateCurrentGameRule()
    {
        curGameRule.text = gameRuleSlider.value.ToString();
    }
}
