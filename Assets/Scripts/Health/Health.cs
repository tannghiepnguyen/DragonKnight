using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float CurrentHealth { get; private set; }
    private Animator animator;
    private bool dead;

    [Header("IFrame")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        CurrentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable)
        {
            return;
        }
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, startingHealth);
        if (CurrentHealth > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {


                //Player
                if (GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().enabled = false;
                }

                //Enemy
                if (GetComponentInParent<EnemyPatrol>() != null)
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }

                if (GetComponent<MeleeEnemy>() != null)
                {
                    GetComponent<MeleeEnemy>().enabled = false;
                }

                animator.SetTrigger("die");

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }

        }

    }

    private void Update()
    {

    }

    public void AddHealth(float _value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        animator.ResetTrigger("die");
        animator.Play("Idle");
        StartCoroutine(Invulnerability());

        //Player
        if (GetComponent<PlayerMovement>() != null)
        {
            GetComponent<PlayerMovement>().enabled = true;
        }
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        invulnerable = false;
    }
}
