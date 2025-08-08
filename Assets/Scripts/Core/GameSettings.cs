using UnityEngine;

namespace BubbleBattle.Core
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Bubble Battle/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Game Rules")]
        [SerializeField] private float gameDuration = 300f;
        [SerializeField] private int maxItemsPerPlayer = 3;
        [SerializeField] private float itemSpawnInterval = 10f;
        
        [Header("Player Settings")]
        [SerializeField] private float playerMoveSpeed = 5f;
        [SerializeField] private int playerMaxHealth = 100;
        
        [Header("Arena Settings")]
        [SerializeField] private Vector2 arenaSize = new Vector2(20f, 12f);
        [SerializeField] private Vector2 playerSpawnDistance = new Vector2(8f, 0f);
        
        public float GameDuration => gameDuration;
        public int MaxItemsPerPlayer => maxItemsPerPlayer;
        public float ItemSpawnInterval => itemSpawnInterval;
        public float PlayerMoveSpeed => playerMoveSpeed;
        public int PlayerMaxHealth => playerMaxHealth;
        public Vector2 ArenaSize => arenaSize;
        public Vector2 PlayerSpawnDistance => playerSpawnDistance;
    }
}