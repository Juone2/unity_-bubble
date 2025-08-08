using UnityEngine;
using BubbleBattle.Player;

namespace BubbleBattle.Items
{
    [CreateAssetMenu(fileName = "Control Reversal Item", menuName = "Bubble Battle/Items/Control Reversal")]
    public class ControlReversalItem : ItemBase
    {
        protected override void ApplyEffect(PlayerController user)
        {
            var targetPlayer = GetOtherPlayer(user);
            if (targetPlayer != null)
            {
                // Reverse the target player's controls
                targetPlayer.ControlsReversed = true;
                
                // Start coroutine to reverse back after duration
                user.StartCoroutine(RemoveEffectAfterDelay(targetPlayer, Duration));
                
                Debug.Log($"Controls reversed for Player {targetPlayer.PlayerData.playerId} for {Duration} seconds!");
            }
        }
        
        private System.Collections.IEnumerator RemoveEffectAfterDelay(PlayerController targetPlayer, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            if (targetPlayer != null)
            {
                targetPlayer.ControlsReversed = false;
                Debug.Log($"Controls restored for Player {targetPlayer.PlayerData.playerId}");
            }
        }
    }
}