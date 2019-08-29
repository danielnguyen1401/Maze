using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class GameUiManager : SingletonMono<GameUiManager>
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Text playerScore;
        [SerializeField] private Text enemyScore;
        [SerializeField] private Text level;
        [SerializeField] private CanvasGroup inGameGroup;
        [SerializeField] private CanvasGroup winLoseGroup;
        [SerializeField] private Image playerCrown;
        [SerializeField] private Image enemyCrown;
        [SerializeField] private Text playerText;
        [SerializeField] private Text enemyText;

        public Action<int> enemyGetScore;
        public Action<int> playerGetScore;
        public Action nextLevel;
        private int _eScore;
        private int _pScore;
        private int _level;
        private readonly WaitForSeconds _wait = new WaitForSeconds(2f);

        private void OnEnable()
        {
            enemyGetScore += OnEnemyGetScore;
            playerGetScore += OnPlayerGetScore;
            nextLevel += OnLevelChange;
        }

        private void OnLevelChange()
        {
            SetLevelByText();
        }

        private void SetLevelByText()
        {
            var realLevel = GameManager.Instance.LevelId;
            realLevel++;
            level.text = realLevel.ToString();
        }

        private void Start()
        {
            SetLevelByText();
            ShowInGame(false);
            ShowWinLose(false);
            enemyScore.text = _eScore.ToString();
            playerScore.text = _pScore.ToString();
        }

        private void OnEnemyGetScore(int value)
        {
            _eScore += value;
            enemyScore.DOText(_eScore.ToString(), 1f, false, ScrambleMode.Numerals);
        }

        private void OnPlayerGetScore(int value)
        {
            _pScore += value;
            playerScore.DOText(_pScore.ToString(), 1f, false, ScrambleMode.Numerals);
        }

        public void ShowStartBtn(bool show)
        {
            startButton.gameObject.SetActive(show);
        }

        public void ShowNextBtn(bool show)
        {
            nextButton.gameObject.SetActive(show);
        }

        public void ShowInGame(bool show)
        {
            inGameGroup.alpha = show ? 1f : 0f;
            inGameGroup.interactable = show;
            inGameGroup.blocksRaycasts = show;
        }

        public void ShowWinLose(bool show)
        {
            winLoseGroup.alpha = show ? 1 : 0;
            winLoseGroup.interactable = show;
            winLoseGroup.blocksRaycasts = show;
        }

        public void ShowFinishLevel(bool playerIsWinner)
        {
            StartCoroutine(ShowFinishLevelCo(playerIsWinner));
        }

        private IEnumerator ShowFinishLevelCo(bool playerIsWinner)
        {
            yield return _wait;
            nextButton.gameObject.SetActive(true);
            ShowWinLose(true);
            if (playerIsWinner)
                PlayerIsWinner();
            else
                AiIsWinner();
        }

        private void PlayerIsWinner()
        {
            enemyText.text = "AI LOSE";
            enemyCrown.color = Color.black;
            playerText.text = "YOU WIN";
            playerCrown.color = Color.white;
        }

        private void AiIsWinner()
        {
            enemyText.text = "AI WIN";
            enemyCrown.color = Color.white;
            playerText.text = "YOU LOSE";
            playerCrown.color = Color.black;
        }

        private void OnDisable()
        {
            enemyGetScore -= OnEnemyGetScore;
            playerGetScore -= OnPlayerGetScore;
            nextLevel -= OnLevelChange;
        }
    }
}