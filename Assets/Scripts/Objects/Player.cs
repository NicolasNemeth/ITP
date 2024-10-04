using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Object
{
    [SerializeField] private Animator spaceshipAnimator;

    private float moveSpeed = 20f;
    private float rotationSpeed = 10f;
    private Vector3 targetPos;
    private bool isMoving = false;

    public static Player Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isMoving)
            MoveTowards(targetPos);
    }

    public void StopMoving()
    {
        isMoving = false;
        spaceshipAnimator.SetBool("isMoving", false);
    }

    public Vector3 MultiplyWithScalar(float scalar)
    {
        targetPos = transform.position * scalar;
        UpdateCoordsText(targetPos);
        spaceshipAnimator.SetBool("isMoving", true);
        isMoving = true;
        return targetPos;
    }
    
    public Vector3 AddVector(Vector3 vector)
    {
        targetPos = transform.position + vector;
        UpdateCoordsText(targetPos);
        spaceshipAnimator.SetBool("isMoving", true);
        isMoving = true;
        return targetPos;
    }

    private void MoveTowards(Vector3 pos)
    {
        transform.position = Vector3.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);

        Vector3 direction = (targetPos - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        if (Vector3.Distance(transform.position, pos) <= 0.01f)
        {
            StopMoving();
            transform.position = pos;
        }
    }
}
