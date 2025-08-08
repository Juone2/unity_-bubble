using UnityEngine;

namespace BubbleBattle.Core
{
    [System.Serializable]
    public struct PlayerData
    {
        public int playerId;
        public string playerName;
        public int score;
        public int health;
        public Vector2 spawnPosition;
        public Color playerColor;
        
        public PlayerData(int id, string name, Vector2 spawn, Color color)
        {
            playerId = id;
            playerName = name;
            score = 0;
            health = 100;
            spawnPosition = spawn;
            playerColor = color;
        }
        
        public void ResetStats()
        {
            score = 0;
            health = 100;
        }
        
        public void TakeDamage(int damage)
        {
            health = Mathf.Max(0, health - damage);
        }
        
        public void AddScore(int points)
        {
            score += points;
        }
    }
}