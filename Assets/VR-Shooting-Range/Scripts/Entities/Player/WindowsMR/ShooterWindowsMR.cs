using UnityEngine;
using System.Collections;
using UnityEngine.XR.WSA.Input;

namespace ExitGames.SportShooting
{
    public class ShooterWindowsMR : Shooter
    {

        void OnEnable()
        {
            InteractionManager.InteractionSourcePressed += ShootAttempt;
        }

        void OnDisable()
        {
            InteractionManager.InteractionSourcePressed -= ShootAttempt;
        }

        protected void ShootAttempt(InteractionSourcePressedEventArgs args)
        {
            if (isRightController(args.state) && args.pressType == InteractionSourcePressType.Select)
            {
                if (_photonView != null && _photonView.isMine)
                {
                    ShootAttempt();
                }
            }
        }


        bool isRightController(InteractionSourceState state)
        {
            if (state.source.kind == InteractionSourceKind.Controller && state.source.handedness == InteractionSourceHandedness.Right)
            {
                return true;
            }

            return false;
        }
    }
}
