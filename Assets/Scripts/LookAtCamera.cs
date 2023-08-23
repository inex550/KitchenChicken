using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    private enum Mode {
        LookAt,
        LookAtInversed,
        CameraForward,
        CameraBackward,
    }

    [SerializeField] private Mode mode;

    private void Update() {
        switch (mode) {
        case Mode.LookAt:
            transform.LookAt(Camera.main.transform);
            break;

        case Mode.LookAtInversed:
            Vector3 inverseDir = transform.position - Camera.main.transform.position;
            transform.LookAt(inverseDir);
            break;

        case Mode.CameraForward:
            transform.forward = Camera.main.transform.forward;
            break;

        case Mode.CameraBackward:
            transform.forward = -Camera.main.transform.forward;
            break;
        }
    }
}