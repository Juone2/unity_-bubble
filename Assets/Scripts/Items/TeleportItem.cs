using UnityEngine;
using BubbleBattle.Player;
using BubbleBattle.Core;

namespace BubbleBattle.Items
{
    [CreateAssetMenu(fileName = "Teleport Item", menuName = "Bubble Battle/Items/Teleport")]
    public class TeleportItem : ItemBase
    {
        [Header("Teleport Settings")]
        [SerializeField] private float teleportRadius = 3f;
        [SerializeField] private int maxAttempts = 10;
        
        protected override void ApplyEffect(PlayerController user)
        {
            Vector3 teleportPosition = FindValidTeleportPosition(user.transform.position);
            
            if (teleportPosition != Vector3.zero)
            {
                // Teleport the player
                user.transform.position = teleportPosition;
                
                // Visual effect (if available)
                // PlayTeleportEffect(user.transform.position);
                
                Debug.Log($"Player {user.PlayerData.playerId} teleported to {teleportPosition}");
            }
            else
            {
                Debug.LogWarning($"Could not find valid teleport position for Player {user.PlayerData.playerId}");
            }
        }
        
        private Vector3 FindValidTeleportPosition(Vector3 currentPosition)
        {
            // Get arena bounds from game settings
            Vector2 arenaSize = Vector2.one * 10f; // Default arena size
            
            if (GameManager.Instance != null && GameManager.Instance != null)
            {
                // arenaSize = GameManager.Instance.GameSettings.ArenaSize;
            }
            
            for (int i = 0; i < maxAttempts; i++)
            {
                // Generate random position within arena bounds
                Vector3 randomPosition = new Vector3(
                    Random.Range(-arenaSize.x / 2 + 1, arenaSize.x / 2 - 1),
                    Random.Range(-arenaSize.y / 2 + 1, arenaSize.y / 2 - 1),
                    0f
                );
                
                // Check if position is far enough from current position
                if (Vector3.Distance(currentPosition, randomPosition) >= teleportRadius)
                {
                    // Check if position is not occupied (basic check)
                    Collider2D overlap = Physics2D.OverlapCircle(randomPosition, 0.5f);
                    if (overlap == null || overlap.CompareTag("Player") == false)
                    {
                        return randomPosition;
                    }
                }
            }
            
            return Vector3.zero; // No valid position found
        }
        
        private void PlayTeleportEffect(Vector3 position)
        {
            // Placeholder for teleport visual effect
            // Could spawn particles, play sound, etc.
        }
    }
}