using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody m_Rigidbody;
    float m_MovementInputValue;
    float m_TurnInputValue;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public Transform m_turretAsset;
    LayerMask m_LayerMask;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_LayerMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        m_MovementInputValue = Input.GetAxis("Vertical");  // Position on the up and down positions
        m_TurnInputValue = Input.GetAxis("Horizontal");  // Position on the left and right positions
        TurnTurret();
        float scroll = Input.GetAxis("Mouse ScrollWheel");
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }

 
 
    void Move()
    {
        Vector3 wantedVelocity = transform.forward * m_MovementInputValue * m_Speed;
        m_Rigidbody.AddForce(wantedVelocity - m_Rigidbody.velocity, ForceMode.VelocityChange);
    }

   
    void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
    void TurnTurret()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_LayerMask))
        {
           //Debug.Log(hit.point)
           m_turretAsset.LookAt(hit.point);
           m_turretAsset.eulerAngles = new Vector3(0, m_turretAsset.eulerAngles.y, m_turretAsset.eulerAngles.z);
        }
    }
}
