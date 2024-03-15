using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody m_Rigidbody;
    float m_MovementInputValue;
    float m_TurnInputValue;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        m_MovementInputValue = Input.GetAxis("Vertical");  // Position on the up and down positions
        m_TurnInputValue = Input.GetAxis("Horizontal");  // Position on the left and right positions
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }

    /// <summary>
    /// Applies velocity on the tank's Vector.
    /// Todo: Write a proper comment
    /// </summary>
    void Move()
    {
        Vector3 wantedVelocity = transform.forward * m_MovementInputValue * m_Speed;
        m_Rigidbody.AddForce(wantedVelocity - m_Rigidbody.velocity, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Todo: Comment this
    /// </summary>
    void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}
