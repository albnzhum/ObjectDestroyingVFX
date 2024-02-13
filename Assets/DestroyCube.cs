using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private Rigidbody _rb;

    public Transform target;
    public float moveSpeed;

    private Vector3 direction;

    private void OnEnable()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == target.gameObject)
        {
            DestroyWithVFX();
        }
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        direction = (target.position - transform.position).normalized;
        _rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
    }

    private void DestroyWithVFX()
    {
        GetComponent<Renderer>().enabled = false;

        _particleSystem.simulationSpace = ParticleSystemSimulationSpace.World;
        _particleSystem.transform.forward = direction;

        _particleSystem.Play();

        Destroy(gameObject, _particleSystem.main.duration);
    }
}