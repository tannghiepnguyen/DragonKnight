using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float attackCooldown;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject[] fireballs;
    private Animator animator;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private AudioClip fireballSound;
    private PlayerControl control;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        control = new PlayerControl();
        control.Gameplay.Attack.performed += x => ControllerAttack();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }
    private void ControllerAttack() 
    {
        if (playerMovement.canAttack()) Attack();
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        animator.SetTrigger("attack");
        cooldownTimer = 0;

        //pooling fireballs
        int index = FindFireball();
        fireballs[index].transform.position = firePoint.position;
        fireballs[index].GetComponent<Projectile>().SetDirection(Math.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private void OnEnable()
    {
        control.Gameplay.Enable();
    }
    private void OnDisable()
    {
        control.Gameplay.Disable();
    }
}
