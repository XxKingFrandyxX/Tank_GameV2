using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemytankshooting : MonoBehaviour
{
    public ParticleSystem m_ExplosionParticles;
    public float m_MaxLifeTime = 2f;
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public float m_LaunchForce = 30f;

    private bool m_CanShoot;
    public float m_ShootDelay = 1f;
    private float m_ShootTimer;
    
    
    void start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }
    void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation); shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
    }
    void Update()
    {
        if(m_CanShoot == true)
        {
            m_ShootTimer -= m_ShootTimer.deltaTime;
            if(m_ShootTimer <= 0)
            {
                Fire();
            }
            
        }

    }

    void Awake()
    {
        m_CanShoot = false;
    }



    private void OnTriggerEnter(collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_CanShoot = true;
        }
    }
    
   private void OnTriggerExit(collider other)
   {
      if (other.CompareTag("Player"))
      {
            m_CanShoot = false;
      }
   }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
