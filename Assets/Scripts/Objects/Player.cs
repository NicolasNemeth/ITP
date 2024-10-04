using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Object
{
    [SerializeField] private int maxHp = 3;
    [SerializeField] private Image[] hpImages;
    [SerializeField] private Animator spaceshipAnimator;

    private int hp;
    private float moveSpeed = 20f;
    private float rotationSpeed = 10f;
    private Vector3 targetPos;

    public bool IsMoving { get; private set; } = false;

    public static Player Instance;

    private void Awake()
    {
        Instance = this;
        Heal();
    }

    private void Update()
    {
        if (IsMoving)
            MoveTowards(targetPos);
    }

    public void Heal()
    {
        hp = maxHp;

        foreach (Image img in hpImages)
        {
            img.enabled = true;
        }
    }

    public void TakeDamage()
    {
        if (hp == 0)
            return;

        hp--;
        hpImages[hp].enabled = false;

        if (hp == 0)
            LevelManager.Instance.ShowGameOverScreen();
    }

    public void StopMoving()
    {
        IsMoving = false;
        spaceshipAnimator.SetBool("isMoving", false);
    }

    public Vector3 MultiplyWithScalar(float scalar)
    {
        targetPos = transform.position * scalar;
        UpdateCoordsText(targetPos);
        spaceshipAnimator.SetBool("isMoving", true);
        IsMoving = true;
        return targetPos;
    }
    
    public Vector3 AddVector(Vector3 vector)
    {
        targetPos = transform.position + vector;
        UpdateCoordsText(targetPos);
        spaceshipAnimator.SetBool("isMoving", true);
        IsMoving = true;
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
