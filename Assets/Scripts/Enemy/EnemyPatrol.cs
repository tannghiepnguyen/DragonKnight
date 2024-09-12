using System;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement params")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy animator")]
    [SerializeField] private Animator animator;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        animator.SetBool("moving", true);
        //make enemy face direction
        enemy.localScale = new Vector3(Math.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + _direction * Time.deltaTime * speed, enemy.position.y, enemy.position.z);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x <= leftEdge.position.x)
            {
                DirectionChange();
            }
            else
            {
                MoveInDirection(-1);
            }

        }
        else
        {
            if (enemy.position.x >= rightEdge.position.x)
            {
                DirectionChange();
            }
            else
            {
                MoveInDirection(1);
            }
        }
    }

    private void DirectionChange()
    {
        animator.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void OnDisable(){
        animator.SetBool("moving", false);
    }
}
