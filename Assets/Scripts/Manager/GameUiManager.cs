using System;
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
        [SerializeField] private GameObject inGameGroup;
        public Action<int> enemyGetScore;
        public Action<int> playerGetScore;
        public Action<int> nextLevel;
        private int _eScore;
        private int _pScore;
        private int _level;

        private void OnEnable()
        {
            enemyGetScore += OnEnemyGetScore;
            playerGetScore += OnPlayerGetScore;
        }

        private void Start()
        {
            ShowInGame(false);
            enemyScore.text = _eScore.ToString();
            playerScore.text = _pScore.ToString();
        }

        private void OnEnemyGetScore(int value)
        {
            _eScore += value;
            enemyScore.text = _eScore.ToString();
        }

        private void OnPlayerGetScore(int value)
        {
            _pScore += value;
            playerScore.text = _pScore.ToString();
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
            inGameGroup.gameObject.SetActive(show);
        }

        private void OnDisable()
        {
            enemyGetScore -= OnEnemyGetScore;
            playerGetScore -= OnPlayerGetScore;
        }
    }
}