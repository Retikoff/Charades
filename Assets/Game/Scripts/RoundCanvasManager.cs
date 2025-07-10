using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoundCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private TMP_Text correctAnswersContainer;
    [SerializeField] private TMP_Text incorrectAnswersContainer;

    private string[] words;

    private GameObject currentCard = null;
    private int correctAnswers = 0;
    private int incorrectAnswers = 0;


    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private void Update()
    {

        if (Touchscreen.current == null) return;

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            startTouchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }

        if (Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
        {
            endTouchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            if (endTouchPosition.x < startTouchPosition.x)
            {
                incorrectAnswers++;
            }
            else if (endTouchPosition.x > startTouchPosition.x)
            {
                correctAnswers++;
            }

            DeleteCurrentCard();
            DrawCard();
        }

        correctAnswersContainer.text = correctAnswers.ToString();
        incorrectAnswersContainer.text = incorrectAnswers.ToString();
    }

    public void StartRound(string[] words)
    {
        correctAnswers = 0;
        incorrectAnswers = 0;
        this.words = words;
        DrawCard();
    }

    private void DrawCard()
    {
        var newCard = Instantiate(cardPrefab);
        newCard.transform.SetParent(transform, false);
        newCard.GetComponentInChildren<TMP_Text>().text = words[Random.Range(0, words.Length)];
        currentCard = newCard;
    }

    private void DeleteCurrentCard()
    {
        if (currentCard == null) return;
        Destroy(currentCard);
        currentCard = null;
    }
}
