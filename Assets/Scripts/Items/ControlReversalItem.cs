using UnityEngine;

namespace BubbleBattle.Items
{
    [CreateAssetMenu(fileName = "Control Reversal Item", menuName = "Bubble Battle/Items/Control Reversal")]
    public class ControlReversalItem : ItemBase
    {
        protected override void ApplyEffect(Component user)
        {
            var targetPlayer = GetOtherPlayer(user);
            if (targetPlayer != null)
            {
                // Reverse the target player's controls using SendMessage
                targetPlayer.SendMessage("SetControlsReversed", true, SendMessageOptions.DontRequireReceiver);
                
                // Start coroutine to reverse back after duration
                StartEffectCoroutine(user, RemoveEffectAfterDelay(targetPlayer, Duration));
                
                Debug.Log($"Controls reversed for {targetPlayer.name} for {Duration} seconds!");
            }
        }
        
        private System.Collections.IEnumerator RemoveEffectAfterDelay(Component targetPlayer, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            if (targetPlayer != null)
            {
                targetPlayer.SendMessage("SetControlsReversed", false, SendMessageOptions.DontRequireReceiver);
                Debug.Log($"Controls restored for {targetPlayer.name}");
            }
        }
    }
}