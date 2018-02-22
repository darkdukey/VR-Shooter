using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ExitGames.SportShooting
{
    public class LaserPointer : MonoBehaviour
    {
        float _maxRayDistance = 100f;

        [SerializeField]
        LayerMask _hitLayer;       

        RaycastHit _hitInfo;
        LineRenderer _lineRenderer;

        protected Vector3 _hitPosition = Vector3.zero;
        protected Quaternion _hitRotation = Quaternion.identity;
        protected bool _raycastHit = false;

        protected virtual void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }
        
        protected virtual void Update()
        {
            // Find collider which was hit by laser aiming
            if (Physics.Raycast(transform.position, transform.forward, out _hitInfo, _maxRayDistance, _hitLayer))
            {
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, _hitInfo.point);

                _hitPosition = transform.position + transform.forward * _hitInfo.distance;
                _hitRotation = transform.rotation;
                _raycastHit = true;
            }
            else
            {
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, transform.position + transform.forward * _maxRayDistance);
                _raycastHit = false;
            }
        }

        protected void ClickOnHitObject()
        {
            Debug.Log("On Click Start");
            if (Physics.Raycast(transform.position, transform.forward, out _hitInfo, _maxRayDistance, _hitLayer))
            {
                Debug.Log("On Click hit test");
                Button button = _hitInfo.collider.GetComponent<Button>();
                if (button != null)
                {
                    Debug.Log("On Click Click");
                    button.onClick.Invoke();
                }
            }
        }
    }
}
