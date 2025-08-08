using UnityEngine;
using BubbleBattle.Core;

namespace BubbleBattle.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private PlayerData playerData;
        [SerializeField] private int currentHealth;
        [SerializeField] private int currentScore;
        
        public PlayerData PlayerData => playerData;
        public int CurrentHealth => currentHealth;
        public int CurrentScore => currentScore;
        public int MaxHealth => playerData.health;
        
        public System.Action<int> OnHealthChanged;
        public System.Action<int> OnScoreChanged;
        public System.Action OnPlayerDied;
        
        public void Initialize(PlayerData data)
        {
            playerData = data;
            currentHealth = data.health;
            currentScore = data.score;
        }
        
        public void TakeDamage(int damage)
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);
            playerData.health = currentHealth;
            
            OnHealthChanged?.Invoke(currentHealth);
            
            if (currentHealth <= 0)
            {
                OnPlayerDied?.Invoke();
            }
        }
        
        public void Heal(int healAmount)
        {
            currentHealth = Mathf.Min(MaxHealth, currentHealth + healAmount);
            playerData.health = currentHealth;
            
            OnHealthChanged?.Invoke(currentHealth);
        }
        
        public void AddScore(int points)
        {
            currentScore += points;
            playerData.score = currentScore;
            
            OnScoreChanged?.Invoke(currentScore);
        }
        
        public void ResetStats()
        {
            playerData.ResetStats();
            currentHealth = playerData.health;
            currentScore = playerData.score;
            
            OnHealthChanged?.Invoke(currentHealth);
            OnScoreChanged?.Invoke(currentScore);
        }
        
        public float GetHealthPercentage()
        {
            return MaxHealth > 0 ? (float)currentHealth / MaxHealth : 0f;
        }
        
        public bool IsAlive()
        {
            return currentHealth > 0;
        }
    }
}