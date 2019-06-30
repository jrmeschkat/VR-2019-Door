using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;

using Leap.Unity.Examples;

[RequireComponent(typeof(InteractionBehaviour))]
public class GraspHandle : MonoBehaviour {

    private InteractionBehaviour _interactiveObject;

    public int maxRotation;
    public int minRotation;

    void Start() {
        _interactiveObject = GetComponent<InteractionBehaviour>();
    }

    void Update() {

        if (_interactiveObject.isGrasped) {
            // object grasped

            // get hand that grasped object
            var sourceHand = Hands.Get(Chirality.Right) != null ? Hands.Get(Chirality.Right) : Hands.Get(Chirality.Left);

            if (sourceHand != null) {
                // get rotation of hand
                Vector3 handRotation = sourceHand.Rotation.ToQuaternion().eulerAngles;

                // calculate new rotation for handle
                float newRotationHandle = -1 * handRotation.z + 360;

                if (this.minRotation <= newRotationHandle && newRotationHandle <= this.maxRotation) {
                    // apply rotation when it is in the limits
                    transform.localRotation = Quaternion.Euler(0, 0, newRotationHandle);
                }
            }

        }
        else {
            // object not grasped

            // reset rotation to 0
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnEnable() {
        _interactiveObject = GetComponent<InteractionBehaviour>();

        _interactiveObject.OnGraspStay = () => { };  
        _interactiveObject.manager.OnPostPhysicalUpdate = () => { };
    }

}
