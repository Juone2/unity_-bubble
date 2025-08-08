using UnityEngine;
using BubbleBattle.Player;

namespace BubbleBattle.Items
{
    [CreateAssetMenu(fileName = "Speed Boost Item", menuName = "Bubble Battle/Items/Speed Boost")]
    public class SpeedBoostItem : ItemBase
    {
        [Header("Speed Boost Settings")]
        [SerializeField] private float speedMultiplier = 1.5f;
        
        protected override void ApplyEffect(PlayerController user)
        {
            var currentSpeed = user.GetComponent<PlayerController>();
            if (currentSpeed != null)
            {
                // Apply speed boost
                // Note: This would need to be implemented in PlayerController
                user.StartCoroutine(ApplySpeedBoost(user, speedMultiplier, Duration));
                
                Debug.Log($"Speed boost applied to Player {user.PlayerData.playerId} for {Duration} seconds!");
            }
        }
        
        private System.Collections.IEnumerator ApplySpeedBoost(PlayerController player, float multiplier, float duration)
        {
            // Store original speed (this would need to be accessible from PlayerController)
            float originalSpeed = 5f; // Default move speed
            
            // Apply speed boost
            // player.SetMoveSpeed(originalSpeed * multiplier);
            
            yield return new WaitForSeconds(duration);
            
            // Restore original speed
            // player.SetMoveSpeed(originalSpeed);
            
            Debug.Log($"Speed boost ended for Player {player.PlayerData.playerId}");
        }
    }
}