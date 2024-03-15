using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float m_DampTime = 0.2f;
    private Vector3 m_MoveVelocity;

    public Transform m_target;
    private Vector3 m_DesiredPosition;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    private void Move()
    {
        m_DesiredPosition = m_target.position;
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            m_DesiredPosition, 
            ref m_MoveVelocity, 
            m_DampTime);
        transform.position = m_DesiredPosition; 
    }
}
