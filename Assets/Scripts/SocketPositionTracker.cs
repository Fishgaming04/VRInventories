using Unity.Mathematics;
using Unity.VRTemplate;
using UnityEngine;

public class SocketPositionTracker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 m_PositionOffset;
    private Quaternion m_rotationOffset;

    [SerializeField]
    private bool3 m_TrackRotation;

    [SerializeField]
    private bool3 m_TrackPosition;

    [SerializeField]
    private Camera m_Camera;

    private void Start()
    {
        m_PositionOffset = transform.localPosition;
        m_rotationOffset = transform.localRotation;
    }

    private void LateUpdate()
    {
        if (m_Camera == null)
        {
            return;
        }
        Transform cameraTransform = m_Camera.transform;
        Vector3 newPosition = transform.position;
        Quaternion newRotation = transform.rotation;
        if (m_TrackPosition.x) newPosition.x = cameraTransform.localPosition.x + m_PositionOffset.x;
        if (m_TrackPosition.y) newPosition.y = cameraTransform.localPosition.y + m_PositionOffset.y;
        if (m_TrackPosition.z) newPosition.z = cameraTransform.localPosition.z + m_PositionOffset.z;

        if (m_TrackRotation.x) newRotation.x = cameraTransform.localRotation.x + m_rotationOffset.x;
        if (m_TrackRotation.y) newRotation.y = cameraTransform.localRotation.y + m_rotationOffset.y;
        if (m_TrackRotation.z) newRotation.z = cameraTransform.localRotation.z + m_rotationOffset.z;

        transform.localPosition = newPosition;
        transform.localRotation = newRotation;

    }
}
