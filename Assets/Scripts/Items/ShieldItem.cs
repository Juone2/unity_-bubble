using UnityEngine;

namespace BubbleBattle.Items
{
    [CreateAssetMenu(fileName = "Shield Item", menuName = "Bubble Battle/Items/Shield")]
    public class ShieldItem : ItemBase
    {
        [Header("Shield Settings")]
        [SerializeField] private int damageReduction = 50; // Percentage
        
        protected override void ApplyEffect(Component user)
        {
            // Apply shield effect
            StartEffectCoroutine(user, ApplyShield(user, damageReduction, Duration));
            
            Debug.Log($"Shield activated for {user.name} for {Duration} seconds!");
        }
        
        private System.Collections.IEnumerator ApplyShield(Component player, int reduction, float duration)
        {
            // Apply shield effect using SendMessage
            player.SendMessage("SetDamageReduction", reduction, SendMessageOptions.DontRequireReceiver);
            
            // Visual indication of shield
            ShowShieldEffect(player);
            
            yield return new WaitForSeconds(duration);
            
            // Remove shield effect
            player.SendMessage("SetDamageReduction", 0, SendMessageOptions.DontRequireReceiver);
            HideShieldEffect(player);
            
            Debug.Log($"Shield effect ended for {player.name}");
        }
        
        private void ShowShieldEffect(Component player)
        {
            // Placeholder for shield visual effect
            // Could change player color, add particle effect, etc.
        }
        
        private void HideShieldEffect(Component player)
        {
            // Remove shield visual effect
        }
    }
}