using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public float m_startingHealth = 100f;
    public ParticleSystem m_ExplosionPrefab;
    private ParticleSystem m_ExplosionParticles;

    public float m_CurrentHealth;
    private bool m_Dead;

    private void OnEnable()
    {
        m_CurrentHealth = m_startingHealth;
        m_Dead = false;
    }

    public void TakeDamage(float amount)
    {
        m_CurrentHealth -= amount;

        if (m_CurrentHealth <= 0 && m_Dead == false)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();

        gameObject.SetActive(false);
    }

    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab);
        m_ExplosionParticles.gameObject.SetActive(false);
    }


}
