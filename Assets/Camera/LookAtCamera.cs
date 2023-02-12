using System;
using System.Drawing;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField]
    private Mode mode;

    private void LateUpdate()
    {
        if (mode is Mode.LookAt)
        {
            transform.LookAt(Camera.main.transform.position);
        }
        else if (mode is Mode.LookAtInverted)
        {
            transform.LookAt(
                transform.position + (transform.position - Camera.main.transform.position)
            );
        }
        else if (mode is Mode.CameraForward)
        {
            transform.forward = Camera.main.transform.forward;
        }
        else if (mode is Mode.CameraForwardInverted)
        {
            transform.forward = -Camera.main.transform.forward;
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted
    }
}
