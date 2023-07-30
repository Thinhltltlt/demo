using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerByMouse : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected float moveSpeed = 10;
    [SerializeField] ParticleSystem particle;
    [SerializeField] private NavMeshAgent agent;
    
    protected Camera mainCamera;
    protected Vector3 targetPos;

    private void Awake()
    {
        mainCamera = Camera.main;
        targetPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                targetPos = hitInfo.point;
                targetPos.y = transform.position.y;
                particle.transform.position = targetPos;
                particle.Play();
                agent.SetDestination(targetPos);
                animator.SetFloat("Speed", moveSpeed);
            }
        }

        // if (Vector3.Distance(targetPos, transform.position) > 0.1f)
        // {
        //     animator.SetFloat("Speed", moveSpeed);
        //     transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        //     transform.LookAt(targetPos);
        // }
        // else
        // {
        //     animator.SetFloat("Speed", 0);
        // }
    }
}
