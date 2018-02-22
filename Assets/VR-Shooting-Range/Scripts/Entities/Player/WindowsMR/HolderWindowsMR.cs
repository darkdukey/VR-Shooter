using UnityEngine;
using System.Collections;
using HoloToolkit.Unity.InputModule;

namespace ExitGames.SportShooting
{
    public class HolderWindowsMR : Holder
    {
        [SerializeField]
        Transform _holderTarget;

        [SerializeField]
        Transform _lookAtTarget;

        protected override void Awake()
        {
            base.Awake();

            if (_holderTarget == null || _lookAtTarget == null)
            {
                Destroy(this);
            }
        }

        void Update()
        {
            //Syncrhoize transform with target transform
            //Syncrhoize transform with target transform
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
