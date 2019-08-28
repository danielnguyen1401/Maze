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
        public Action<int> enemyGetScore;
        public Action<int> playerGetScore;
        public Action<int> nextLevel;
        private int _eScore;
        private int _pScore;
        private int _level;
        private readonly WaitForSeconds _wait = new WaitForSeconds(2f);

        private void OnEnable()
        {
            enemyGetScore += OnEnemyGetScore;
            playerGetScore += OnPlayerGetScore;
        }

        private void Start()
        {
            ShowInGame(false);
            ShowWinLose(false);
            enemyScore.text = _eScore.ToString();
            playerScore.text = _pScore.ToString();
        }

        private void OnEnemyGetScore(int value)
        {
            _eScore += value;
//            enemyScore.text = _eScore.ToString();
            enemyScore.DOText(_eScore.ToString(), 1f, false, ScrambleMode.Numerals);
        }

        private void OnPlayerGetScore(int value)
        {
            _pScore += value;
            playerScore.DOText(_pScore.ToString(), 1f, false, ScrambleMode.Numerals);
        }

        private void Update()
        {
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

        public void ShowFinishLevel()
        {
            StartCoroutine(ShowFinishLevelCo());
        }

        private IEnumerator ShowFinishLevelCo()
        {
            yield return _wait;
            ShowInGame(false);
            ShowWinLose(true);
        }

        private void OnDisable()
        {
            enemyGetScore -= OnEnemyGetScore;
            playerGetScore -= OnPlayerGetScore;
        }
    }
}