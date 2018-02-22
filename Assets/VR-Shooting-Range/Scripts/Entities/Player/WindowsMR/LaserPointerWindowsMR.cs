using UnityEngine;
using System.Collections;
using UnityEngine.XR.WSA.Input;

namespace ExitGames.SportShooting
{
    public class LaserPointerWindowsMR : LaserPointer
    {
        [SerializeField]
        GameObject _cursorPrefab;

        private GameObject _cursor;
        //private Material _material;
        private MotionControllerState _currentState;

        private void StartListeners()
        {
            InteractionManager.InteractionSourceUpdated += InteractionManager_InteractionSourceUpdated;
            InteractionManager.InteractionSourcePressed += InteractionManager_InteractionSourcePressed;
            InteractionManager.InteractionSourceReleased += InteractionManager_InteractionSourceReleased;
        }

        private void StopListeners()
        {
            InteractionManager.InteractionSourceUpdated -= InteractionManager_InteractionSourceUpdated;
            InteractionManager.InteractionSourcePressed -= InteractionManager_InteractionSourcePressed;
            InteractionManager.InteractionSourceReleased -= InteractionManager_InteractionSourceReleased;
        }

        protected override void Awake()
        {
            base.Awake();
            if (_cursor == null)
            {
                _cursor = Instantiate<GameObject>(_cursorPrefab);
                //_material = _cursor.GetComponent<MeshRenderer>().material;
            }
        }

        protected override void Update()
        {
            base.Update();

            _cursor.transform.position = _hitPosition;
            _cursor.transform.rotation = _hitRotation;
            _cursor.SetActive(_raycastHit);
        }

        private void Start()
        {
        }

        void OnEnable()
        {
            StartListeners();
            if (_cursor != null)
            {
                _cursor.SetActive(true);
            }
        }

        void OnDisable()
        {
            StopListeners();
            if (_cursor != null)
            {
                _cursor.SetActive(false);
            }
        }

        void OnDestroy()
        {
            if (_cursor != null)
            {
                Destroy(_cursor);
                _cursor = null;
            }
        }

        private void InteractionManager_InteractionSourceUpdated(InteractionSourceUpdatedEventArgs args)
        {
            if (isRightController(args.state))
            {
                updatePose(args.state.sourcePose);

                transform.localPosition = _currentState.PointerPosition;
                transform.forward = _currentState.PointerForward;

                if (_currentState.SelectPressed) { }
            }
        }

        private void InteractionManager_InteractionSourcePressed(InteractionSourcePressedEventArgs args)
        {

            if (isRightController(args.state))
            {
                UpdatePressed(args.pressType, args.state);
                //if (_currentState.SelectPressed)
                if(args.pressType == InteractionSourcePressType.Select)
                {
                    Debug.Log("Select pressed");
                    ClickOnHitObject();
                }
                //_material.SetColor("_Color", Color.red);
            }
        }

        private void InteractionManager_InteractionSourceReleased(InteractionSourceReleasedEventArgs args)
        {
            if (isRightController(args.state))
            {
                UpdatePressed(args.pressType, args.state);
                //_material.SetColor("_Color", Color.white);
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

        void updatePose(InteractionSourcePose pose)
        {

            Vector3 angularVelocity, gripPosition, pointerPosition, pointerForward, gripForward;
            Quaternion gripRotation, pointerRotation;

            if (pose.TryGetPosition(out gripPosition, InteractionSourceNode.Grip))
            {
                _currentState.GripPosition = gripPosition;
            }

            if (pose.TryGetPosition(out pointerPosition, InteractionSourceNode.Pointer))
            {
                _currentState.PointerPosition = pointerPosition;
            }

            if (pose.TryGetRotation(out pointerRotation, InteractionSourceNode.Pointer))
            {
                _currentState.PointerRotation = pointerRotation;
            }


            if (pose.TryGetRotation(out gripRotation, InteractionSourceNode.Grip))
            {
                _currentState.GripRotation = gripRotation;
            }

            if (pose.TryGetForward(out pointerForward, InteractionSourceNode.Pointer))
            {
                _currentState.PointerForward = pointerForward;
            }

            if (pose.TryGetAngularVelocity(out angularVelocity))
            {
                _currentState.AngularVelocity = angularVelocity;
            }
        }

        void UpdatePressed(InteractionSourcePressType pressType, InteractionSourceState state)
        {
            switch (pressType)
            {
                case InteractionSourcePressType.Select:
                    _currentState.SelectPressed = state.selectPressed;
                    Debug.Log("Select state:" + state.selectPressed);
                    break;
                case InteractionSourcePressType.Grasp:
                    _currentState.GraspPressed = state.grasped;
                    break;
                case InteractionSourcePressType.Menu:
                    _currentState.MenuPressed = state.menuPressed;
                    break;
                case InteractionSourcePressType.Touchpad:
                    _currentState.TouchPadPressed = state.touchpadPressed;
                    break;
                case InteractionSourcePressType.Thumbstick:
                    _currentState.ThumbstickPressed = state.thumbstickPressed;
                    break;
            }
        }

    }
}
