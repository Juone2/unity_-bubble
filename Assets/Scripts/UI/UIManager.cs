using UnityEngine;
using BubbleBattle.Core;

namespace BubbleBattle.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        
        [Header("UI Panels")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject gameHUDPanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject gameOverPanel;
        
        [Header("UI Components")]
        [SerializeField] private GameHUD gameHUD;
        [SerializeField] private MenuManager menuManager;
        
        private GameState currentUIState;
        
        public GameHUD GameHUD => gameHUD;
        public MenuManager MenuManager => menuManager;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeUI();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            SubscribeToEvents();
            ShowMainMenu();
        }
        
        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }
        
        private void InitializeUI()
        {
            // Initialize UI components
            if (gameHUD == null)
                gameHUD = GetComponentInChildren<GameHUD>();
            if (menuManager == null)
                menuManager = GetComponentInChildren<MenuManager>();
        }
        
        private void SubscribeToEvents()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
            }
        }
        
        private void UnsubscribeFromEvents()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
            }
        }
        
        private void OnGameStateChanged(GameState newState)
        {
            UpdateUIForGameState(newState);
        }
        
        private void UpdateUIForGameState(GameState gameState)
        {
            // Hide all panels first
            HideAllPanels();
            
            // Show appropriate panel based on game state
            switch (gameState)
            {
                case GameState.Menu:
                    ShowMainMenu();
                    break;
                case GameState.Playing:
                    ShowGameHUD();
                    break;
                case GameState.Paused:
                    ShowPauseMenu();
                    break;
                case GameState.GameOver:
                    ShowGameOverScreen();
                    break;
            }
            
            currentUIState = gameState;
        }
        
        private void HideAllPanels()
        {
            if (mainMenuPanel) mainMenuPanel.SetActive(false);
            if (gameHUDPanel) gameHUDPanel.SetActive(false);
            if (pausePanel) pausePanel.SetActive(false);
            if (gameOverPanel) gameOverPanel.SetActive(false);
        }
        
        public void ShowMainMenu()
        {
            if (mainMenuPanel)
            {
                mainMenuPanel.SetActive(true);
                if (menuManager != null)
                    menuManager.ShowMainMenu();
            }
        }
        
        public void ShowGameHUD()
        {
            if (gameHUDPanel)
            {
                gameHUDPanel.SetActive(true);
                if (gameHUD != null)
                    gameHUD.Show();
            }
        }
        
        public void ShowPauseMenu()
        {
            if (pausePanel)
            {
                pausePanel.SetActive(true);
                if (menuManager != null)
                    menuManager.ShowPauseMenu();
            }
        }
        
        public void ShowGameOverScreen()
        {
            if (gameOverPanel)
            {
                gameOverPanel.SetActive(true);
                if (menuManager != null)
                    menuManager.ShowGameOverScreen();
            }
        }
        
        public void TogglePause()
        {
            if (GameManager.Instance == null) return;
            
            if (GameManager.Instance.CurrentGameState == GameState.Playing)
            {
                GameManager.Instance.PauseGame();
            }
            else if (GameManager.Instance.CurrentGameState == GameState.Paused)
            {
                GameManager.Instance.ResumeGame();
            }
        }
        
        private void Update()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            // Handle pause input
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }
}