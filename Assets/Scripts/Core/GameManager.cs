using UnityEngine;

namespace BubbleBattle.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        [Header("Game Settings")]
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private float gameDuration = 300f; // 5 minutes
        
        [Header("Game State")]
        private GameState currentGameState = GameState.Menu;
        private float gameTimer;
        private bool isGameRunning = false;
        
        public GameState CurrentGameState => currentGameState;
        public float GameTimer => gameTimer;
        public float GameDuration => gameDuration;
        public bool IsGameRunning => isGameRunning;
        
        public System.Action<GameState> OnGameStateChanged;
        public System.Action<float> OnGameTimerUpdated;
        public System.Action OnGameEnded;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            InitializeGame();
        }
        
        private void Update()
        {
            if (isGameRunning)
            {
                UpdateGameTimer();
            }
        }
        
        private void InitializeGame()
        {
            ChangeGameState(GameState.Menu);
        }
        
        public void StartGame()
        {
            gameTimer = gameDuration;
            isGameRunning = true;
            ChangeGameState(GameState.Playing);
        }
        
        public void PauseGame()
        {
            isGameRunning = false;
            ChangeGameState(GameState.Paused);
        }
        
        public void ResumeGame()
        {
            isGameRunning = true;
            ChangeGameState(GameState.Playing);
        }
        
        public void EndGame()
        {
            isGameRunning = false;
            ChangeGameState(GameState.GameOver);
            OnGameEnded?.Invoke();
        }
        
        private void UpdateGameTimer()
        {
            gameTimer -= Time.deltaTime;
            OnGameTimerUpdated?.Invoke(gameTimer);
            
            if (gameTimer <= 0f)
            {
                gameTimer = 0f;
                EndGame();
            }
        }
        
        private void ChangeGameState(GameState newState)
        {
            if (currentGameState != newState)
            {
                currentGameState = newState;
                OnGameStateChanged?.Invoke(currentGameState);
                Debug.Log($"Game state changed to: {currentGameState}");
            }
        }
    }
}