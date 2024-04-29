using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyTankShooting : MonoBehaviour
{
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public Transform m_FireTransform1;
    public Transform m_FireTransform2;
    public float m_LaunchForce = 30f;

    private bool m_CanShoot;
    public float m_ShootDelay = 1f;
    private float m_ShootTimer;

    void Update()
    {
        if (m_CanShoot == true)
        {
            m_ShootTimer -= Time.deltaTime;
            if (m_ShootTimer <= 0)
            {
                m_ShootTimer = m_ShootDelay;
                Fire();
                Fire1();
                Fire2();
            }
        }
    }

    void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_Shell,
                                         m_FireTransform.position,
                                         m_FireTransform.rotation);
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;

    }

    void Fire1()
    {
        Rigidbody shellInstance = Instantiate(m_Shell,
                                        m_FireTransform1.position,
                                        m_FireTransform1.rotation);
        shellInstance.velocity = m_LaunchForce * m_FireTransform1.forward;
    }
    void Fire2()
    {
        Rigidbody shellInstance = Instantiate(m_Shell,
                                       m_FireTransform2.position,
                                       m_FireTransform2.rotation);
        shellInstance.velocity = m_LaunchForce * m_FireTransform2.forward;
    }

    void Awake()
    {
        m_CanShoot = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_CanShoot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_CanShoot = false;
        }
    }
}
