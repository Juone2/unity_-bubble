using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BubbleBattle.Core;
using BubbleBattle.Items;

namespace BubbleBattle.UI
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Menu Buttons")]
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button quitButton;
        
        [Header("Game Over")]
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private TextMeshProUGUI winnerText;
        [SerializeField] private TextMeshProUGUI finalScoreText;
        
        private void Start()
        {
            InitializeButtons();
        }
        
        private void InitializeButtons()
        {
            // Main Menu buttons
            if (startGameButton != null)
                startGameButton.onClick.AddListener(StartGame);
            if (quitButton != null)
                quitButton.onClick.AddListener(QuitGame);
                
            // Pause Menu buttons
            if (resumeButton != null)
                resumeButton.onClick.AddListener(ResumeGame);
            if (restartButton != null)
                restartButton.onClick.AddListener(RestartGame);
            if (mainMenuButton != null)
                mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
        
        public void ShowMainMenu()
        {
            // Main menu is already active, just ensure buttons are interactable
            SetButtonsInteractable(true);
        }
        
        public void ShowPauseMenu()
        {
            SetButtonsInteractable(true);
        }
        
        public void ShowGameOverScreen()
        {
            UpdateGameOverUI();
            SetButtonsInteractable(true);
        }
        
        private void UpdateGameOverUI()
        {
            if (GameManager.Instance == null) return;
            
            // Find players and determine winner
            var players = FindObjectsOfType<BubbleBattle.Player.PlayerController>();
            if (players.Length >= 2)
            {
                var player1Stats = players[0].GetComponent<BubbleBattle.Player.PlayerStats>();
                var player2Stats = players[1].GetComponent<BubbleBattle.Player.PlayerStats>();
                
                if (player1Stats != null && player2Stats != null)
                {
                    // Determine winner based on score
                    string winner;
                    if (player1Stats.CurrentScore > player2Stats.CurrentScore)
                    {
                        winner = "Player 1 Wins!";
                    }
                    else if (player2Stats.CurrentScore > player1Stats.CurrentScore)
                    {
                        winner = "Player 2 Wins!";
                    }
                    else
                    {
                        winner = "It's a Tie!";
                    }
                    
                    // Update UI text
                    if (gameOverText != null)
                        gameOverText.text = "Game Over";
                    if (winnerText != null)
                        winnerText.text = winner;
                    if (finalScoreText != null)
                        finalScoreText.text = $"Player 1: {player1Stats.CurrentScore}  |  Player 2: {player2Stats.CurrentScore}";
                }
            }
        }
        
        private void SetButtonsInteractable(bool interactable)
        {
            if (startGameButton != null)
                startGameButton.interactable = interactable;
            if (resumeButton != null)
                resumeButton.interactable = interactable;
            if (restartButton != null)
                restartButton.interactable = interactable;
            if (mainMenuButton != null)
                mainMenuButton.interactable = interactable;
            if (quitButton != null)
                quitButton.interactable = interactable;
        }
        
        public void StartGame()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StartGame();
            }
        }
        
        public void ResumeGame()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ResumeGame();
            }
        }
        
        public void RestartGame()
        {
            // Reset players and restart game
            var players = FindObjectsOfType<BubbleBattle.Player.PlayerController>();
            foreach (var player in players)
            {
                player.ResetPlayer();
            }
            
            // Clear all items
            if (ItemManager.Instance != null)
            {
                ItemManager.Instance.ClearAllItems();
            }
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StartGame();
            }
        }
        
        public void ReturnToMainMenu()
        {
            // Reset game state
            if (GameManager.Instance != null)
            {
                GameManager.Instance.EndGame();
            }
            
            // Show main menu UI
            if (UIManager.Instance != null)
            {
                UIManager.Instance.ShowMainMenu();
            }
        }
        
        public void QuitGame()
        {
            Debug.Log("Quitting game...");
            
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}