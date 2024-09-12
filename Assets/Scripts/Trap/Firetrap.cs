using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("Firetrap timer")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isTriggered;
    private bool isActive;

    private Health playerHealth;
    [Header("SFX")]
    [SerializeField] private AudioClip fireSound;

    private void Update()
    {
        if (playerHealth != null && isActive)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = collision.GetComponent<Health>();
            if (!isTriggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (isActive)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        //notify player and trigger trap
        isTriggered = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(activationDelay);

        //activate trap
        SoundManager.instance.PlaySound(fireSound);
        spriteRenderer.color = Color.white;
        isActive = true;
        animator.SetBool("activated", true);

        //deactivate trap
        yield return new WaitForSeconds(activeTime);
        isActive = false;
        isTriggered = false;
        animator.SetBool("activated", false);
    }
}
