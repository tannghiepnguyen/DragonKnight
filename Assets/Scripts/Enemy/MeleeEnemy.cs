using System;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack params")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [Header("Collider collider")]
    [SerializeField] private float colliderDistance;

    [Header("Layer player")]
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Health playerHealth;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    private Animator animator;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInsight())
        {
            if (cooldownTimer >= attackCooldown && playerHealth.CurrentHealth > 0)
            {
                cooldownTimer = 0;
                animator.SetTrigger("attack");
                SoundManager.instance.PlaySound(attackSound);
            }
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInsight();
        }
    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            playerHealth = hit.collider.GetComponent<Health>();

        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInsight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
