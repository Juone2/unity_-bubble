using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BubbleBattle.Core;
using BubbleBattle.Player;

namespace BubbleBattle.UI
{
    public class GameHUD : MonoBehaviour
    {
        [Header("Timer")]
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Slider timerSlider;
        
        [Header("Player 1 UI")]
        [SerializeField] private TextMeshProUGUI player1ScoreText;
        [SerializeField] private Slider player1HealthBar;
        [SerializeField] private Image[] player1ItemSlots = new Image[3];
        [SerializeField] private Image player1ActiveItemFrame;
        
        [Header("Player 2 UI")]
        [SerializeField] private TextMeshProUGUI player2ScoreText;
        [SerializeField] private Slider player2HealthBar;
        [SerializeField] private Image[] player2ItemSlots = new Image[3];
        [SerializeField] private Image player2ActiveItemFrame;
        
        [Header("Game Status")]
        [SerializeField] private TextMeshProUGUI statusText;
        [SerializeField] private GameObject controlReversalWarning;
        
        private PlayerController[] players = new PlayerController[2];
        
        public void Show()
        {
            gameObject.SetActive(true);
            InitializeHUD();
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        private void Start()
        {
            InitializeHUD();
        }
        
        private void InitializeHUD()
        {
            // Find players
            var allPlayers = FindObjectsOfType<PlayerController>();
            for (int i = 0; i < allPlayers.Length && i < 2; i++)
            {
                players[i] = allPlayers[i];
                SubscribeToPlayerEvents(players[i]);
            }
            
            // Subscribe to game events
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnGameTimerUpdated += UpdateTimer;
            }
            
            // Initialize UI elements
            UpdateAllUI();
        }
        
        private void OnDestroy()
        {
            // Unsubscribe from events
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                    UnsubscribeFromPlayerEvents(players[i]);
            }
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnGameTimerUpdated -= UpdateTimer;
            }
        }
        
        private void SubscribeToPlayerEvents(PlayerController player)
        {
            if (player == null) return;
            
            var playerStats = player.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.OnHealthChanged += (health) => UpdatePlayerHealth(player);
                playerStats.OnScoreChanged += (score) => UpdatePlayerScore(player);
            }
            
            player.OnItemSwitched += (index) => UpdatePlayerItems(player);
            player.OnItemUsed += (item) => UpdatePlayerItems(player);
        }
        
        private void UnsubscribeFromPlayerEvents(PlayerController player)
        {
            if (player == null) return;
            
            var playerStats = player.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.OnHealthChanged -= (health) => UpdatePlayerHealth(player);
                playerStats.OnScoreChanged -= (score) => UpdatePlayerScore(player);
            }
            
            player.OnItemSwitched -= (index) => UpdatePlayerItems(player);
            player.OnItemUsed -= (item) => UpdatePlayerItems(player);
        }
        
        private void UpdateAllUI()
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    UpdatePlayerScore(players[i]);
                    UpdatePlayerHealth(players[i]);
                    UpdatePlayerItems(players[i]);
                }
            }
        }
        
        private void UpdateTimer(float timeRemaining)
        {
            if (timerText != null)
            {
                int minutes = Mathf.FloorToInt(timeRemaining / 60);
                int seconds = Mathf.FloorToInt(timeRemaining % 60);
                timerText.text = $"{minutes:00}:{seconds:00}";
            }
            
            if (timerSlider != null && GameManager.Instance != null)
            {
                timerSlider.value = timeRemaining / GameManager.Instance.GameDuration;
            }
        }
        
        private void UpdatePlayerScore(PlayerController player)
        {
            var playerStats = player.GetComponent<PlayerStats>();
            if (playerStats == null) return;
            
            int playerId = player.PlayerData.playerId;
            TextMeshProUGUI scoreText = (playerId == 1) ? player1ScoreText : player2ScoreText;
            
            if (scoreText != null)
            {
                scoreText.text = $"Score: {playerStats.CurrentScore}";
            }
        }
        
        private void UpdatePlayerHealth(PlayerController player)
        {
            var playerStats = player.GetComponent<PlayerStats>();
            if (playerStats == null) return;
            
            int playerId = player.PlayerData.playerId;
            Slider healthBar = (playerId == 1) ? player1HealthBar : player2HealthBar;
            
            if (healthBar != null)
            {
                healthBar.value = playerStats.GetHealthPercentage();
            }
        }
        
        private void UpdatePlayerItems(PlayerController player)
        {
            int playerId = player.PlayerData.playerId;
            Image[] itemSlots = (playerId == 1) ? player1ItemSlots : player2ItemSlots;
            Image activeFrame = (playerId == 1) ? player1ActiveItemFrame : player2ActiveItemFrame;
            
            // Update item slots
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i] != null)
                {
                    var item = player.Inventory[i];
                    if (item != null && item.ItemIcon != null)
                    {
                        itemSlots[i].sprite = item.ItemIcon;
                        itemSlots[i].color = Color.white;
                    }
                    else
                    {
                        itemSlots[i].sprite = null;
                        itemSlots[i].color = Color.clear;
                    }
                }
            }
            
            // Update active item frame
            if (activeFrame != null)
            {
                activeFrame.transform.position = itemSlots[player.CurrentItemIndex].transform.position;
            }
        }
        
        public void ShowControlReversalWarning(bool show)
        {
            if (controlReversalWarning != null)
            {
                controlReversalWarning.SetActive(show);
            }
        }
        
        public void ShowStatusMessage(string message, float duration = 3f)
        {
            if (statusText != null)
            {
                statusText.text = message;
                statusText.gameObject.SetActive(true);
                
                // Hide after duration
                Invoke(nameof(HideStatusMessage), duration);
            }
        }
        
        private void HideStatusMessage()
        {
            if (statusText != null)
            {
                statusText.gameObject.SetActive(false);
            }
        }
    }
}