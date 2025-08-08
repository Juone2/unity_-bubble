using UnityEngine;
using BubbleBattle.Player;

namespace BubbleBattle.Items
{
    [CreateAssetMenu(fileName = "Shield Item", menuName = "Bubble Battle/Items/Shield")]
    public class ShieldItem : ItemBase
    {
        [Header("Shield Settings")]
        [SerializeField] private int damageReduction = 50; // Percentage
        
        protected override void ApplyEffect(PlayerController user)
        {
            // Apply shield effect
            user.StartCoroutine(ApplyShield(user, damageReduction, Duration));
            
            Debug.Log($"Shield activated for Player {user.PlayerData.playerId} for {Duration} seconds!");
        }
        
        private System.Collections.IEnumerator ApplyShield(PlayerController player, int reduction, float duration)
        {
            // Apply shield effect (would need implementation in PlayerController)
            // player.SetDamageReduction(reduction);
            
            // Visual indication of shield
            // ShowShieldEffect(player);
            
            yield return new WaitForSeconds(duration);
            
            // Remove shield effect
            // player.SetDamageReduction(0);
            // HideShieldEffect(player);
            
            Debug.Log($"Shield effect ended for Player {player.PlayerData.playerId}");
        }
        
        private void ShowShieldEffect(PlayerController player)
        {
            // Placeholder for shield visual effect
            // Could change player color, add particle effect, etc.
        }
        
        private void HideShieldEffect(PlayerController player)
        {
            // Remove shield visual effect
        }
    }
}