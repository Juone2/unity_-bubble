using UnityEngine;

namespace BubbleBattle.Items
{
    [CreateAssetMenu(fileName = "Speed Boost Item", menuName = "Bubble Battle/Items/Speed Boost")]
    public class SpeedBoostItem : ItemBase
    {
        [Header("Speed Boost Settings")]
        [SerializeField] private float speedMultiplier = 1.5f;
        
        protected override void ApplyEffect(Component user)
        {
            if (user != null)
            {
                // Apply speed boost
                StartEffectCoroutine(user, ApplySpeedBoost(user, speedMultiplier, Duration));
                
                Debug.Log($"Speed boost applied to {user.name} for {Duration} seconds!");
            }
        }
        
        private System.Collections.IEnumerator ApplySpeedBoost(Component player, float multiplier, float duration)
        {
            // Use reflection to get and set speed
            var playerType = player.GetType();
            var getSpeedMethod = playerType.GetMethod("GetOriginalMoveSpeed");
            var setSpeedMethod = playerType.GetMethod("SetMoveSpeed");
            
            // Get original speed
            float originalSpeed = 5f; // Default fallback
            if (getSpeedMethod != null)
            {
                var result = getSpeedMethod.Invoke(player, null);
                if (result is float speed) originalSpeed = speed;
            }
            
            // Apply speed boost
            if (setSpeedMethod != null)
                setSpeedMethod.Invoke(player, new object[] { originalSpeed * multiplier });
            
            yield return new WaitForSeconds(duration);
            
            // Restore original speed
            if (setSpeedMethod != null)
                setSpeedMethod.Invoke(player, new object[] { originalSpeed });
            
            Debug.Log($"Speed boost ended for {player.name}");
        }
    }
}