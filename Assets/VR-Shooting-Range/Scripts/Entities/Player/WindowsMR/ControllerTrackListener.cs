using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WinMRSnippets;

public class ControllerTrackListener : MonoBehaviour {

    [RequireComponent(typeof(TrackedController))]
    public class TrackerListener : MonoBehaviour
    {
        private TrackedController trackedController;

        // Use this for initialization
        void Start()
        {
            Debug.Log("Started"); 
            trackedController = GetComponent<TrackedController>();

            Debug.Assert(trackedController != null);
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log("Track");
            var state = trackedController.GetState();

            transform.localPosition = state.GripPosition;
            transform.rotation = state.GripRotation;

        }
    }
}
