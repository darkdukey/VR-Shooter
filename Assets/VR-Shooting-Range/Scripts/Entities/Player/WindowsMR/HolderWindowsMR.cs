using UnityEngine;
using System.Collections;
using HoloToolkit.Unity.InputModule;

namespace ExitGames.SportShooting
{
    public class HolderWindowsMR : Holder
    {
        public Transform motionControllers;
        Transform _holderTarget;
        Transform _lookAtTarget;

        protected override void Awake()
        {
            base.Awake();
        }

        void Update()
        {
            //Syncrhoize transform with target transform
            if (_holderTarget == null || _lookAtTarget == null)
            {
                _holderTarget = motionControllers.Find("RightController");
                _lookAtTarget = motionControllers.Find("LeftController");
            }

            if (_holderTarget == null || _lookAtTarget == null)
            {
                return;
            }


            transform.position = _holderTarget.position;
            transform.rotation = _holderTarget.rotation;
            transform.LookAt(_lookAtTarget.position);
        }
    }
}
