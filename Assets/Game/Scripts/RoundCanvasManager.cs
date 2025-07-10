using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoundCanvasManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private TMP_Text teamContainer;
    [SerializeField] private TMP_Text correctAnswersContainer;
    [SerializeField] private TMP_Text incorrectAnswersContainer;
    [SerializeField] private GameObject startRoundButton;
    [SerializeField] private TMP_Text currentWordCounter;
    [SerializeField] private GameObject nextTeamButton;

    private string[] words;

    private GameObject currentCard = null;
    private int correctAnswers = 0;
    private int incorrectAnswers = 0;
    private int numberOfWords = 0;
    private int numberOfCurrentWord = 0;

    private float minimumSwipeDistance = 10f;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private bool isRoundStarted = false;

    private void Update()
    {
        if (numberOfCurrentWord >= numberOfWords)
        {
            EndRound();
            return;
        }

        if (Touchscreen.current == null) return;

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame && isRoundStarted)
        {
            startTouchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }

        if (Touchscreen.current.primaryTouch.press.wasReleasedThisFrame && isRoundStarted)
        {
            endTouchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            float swipeDistance = Math.Abs(endTouchPosition.x - startTouchPosition.x);

            if (swipeDistance < minimumSwipeDistance)
            {
                return;
            }

            if (endTouchPosition.x < startTouchPosition.x)
            {
                incorrectAnswers++;
            }

            if (endTouchPosition.x > startTouchPosition.x)
            {
                correctAnswers++;
            }

            DeleteCurrentCard();
            numberOfCurrentWord++;
            DrawCard();
        }

        correctAnswersContainer.text = correctAnswers.ToString();
        incorrectAnswersContainer.text = incorrectAnswers.ToString();
        currentWordCounter.text = numberOfCurrentWord.ToString() + " / " + numberOfWords;
    }

    public void InitRound(string[] words, int numberOfWords, int teamNumber)
    {
        startTouchPosition = Vector2.zero;
        endTouchPosition = Vector2.zero;
        this.words = words;
        this.numberOfWords = numberOfWords;
        numberOfCurrentWord = 0;
        correctAnswers = 0;
        incorrectAnswers = 0;
        DeleteCurrentCard();

        teamContainer.text = "Команда №" + teamNumber;
        startRoundButton.SetActive(true);
        nextTeamButton.SetActive(false);
    }

    public void StartRound()
    {
        startRoundButton.SetActive(false);

        DrawCard();
        StartCoroutine(EnableRoundAfterDelay());
    }

    /* Why this is here?
    Cause when i press start button, update method read that i release my touch, but it's not what i expect
    So i enable touch handling after small delay
    */
    private IEnumerator EnableRoundAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        isRoundStarted = true;
    }

    private void DrawCard()
    {
        var newCard = Instantiate(cardPrefab);
        newCard.transform.SetParent(transform, false);
        newCard.GetComponentInChildren<TMP_Text>().text = words[UnityEngine.Random.Range(0, words.Length)];
        currentCard = newCard;
    }

    private void DeleteCurrentCard()
    {
        if (currentCard == null) return;
        Destroy(currentCard);
        currentCard = null;
    }

    private void EndRound()
    {
        DeleteCurrentCard();
        nextTeamButton.SetActive(true);
        isRoundStarted = false;
    }

    public void GoToNextTeam()
    {
        gameManager.StartAnotherRound(correctAnswers);
    }
}
