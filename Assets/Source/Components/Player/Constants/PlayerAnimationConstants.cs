using UnityEngine;

namespace Source.Components.Player.Constants
{
    public static class PlayerAnimationConstants
    {
        public static readonly int IdleStateHash = Animator.StringToHash("Idle");
        public static readonly int SitDownStateHash = Animator.StringToHash("SitDown");
        public static readonly int SittingStateHash = Animator.StringToHash("Sitting");
        public static readonly int StandUpStateHash = Animator.StringToHash("StandUp");
        public static readonly int IsSittingParamHash = Animator.StringToHash("IsSitting");
    }
}
