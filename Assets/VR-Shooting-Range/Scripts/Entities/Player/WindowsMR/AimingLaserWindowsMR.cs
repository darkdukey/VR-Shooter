using UnityEngine;
using System.Collections;
namespace ExitGames.SportShooting
{
    public class AimingLaserWindowsMR : AimingLaser
    {

        protected override void Awake()
        {
            base.Awake();
            
        }

        protected void OnDestroy()
        {
            
        }

        private void ToggleLaser(object sender, ClickedEventArgs e)
        {
            ToggleLaser();
        }
    }
}