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

    private void Start()
    {
        numTeamsSlider.onValueChanged.AddListener(delegate { UpdateCurrentNumTeams(); });   
    }

    public void UpdateCurrentNumTeams()
    {
        curNumTeams.text = numTeamsSlider.value.ToString();
    } 
}
