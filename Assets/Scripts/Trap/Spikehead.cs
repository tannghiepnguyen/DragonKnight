using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("Spikehead attributes")]
    private Vector3 destination;
    private Vector3[] direction = new Vector3[4];
    [SerializeField] private float speed;
    [SerializeField] private float range;
    private bool attacking;
    [SerializeField] private float checkDelay;
    private float checkTimer;
    [SerializeField] private LayerMask playerLayer;
    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;
    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer >= checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirection();
        for (int i = 0; i < direction.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction[i], range, playerLayer);
            if (hit.collider != null && !attacking)
            {
                destination = direction[i];
                attacking = true;
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirection()
    {
        direction[0] = Vector3.right * range;
        direction[1] = Vector3.left * range;
        direction[2] = Vector3.up * range;
        direction[3] = Vector3.down * range;
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        SoundManager.instance.PlaySound(impactSound);
        Stop();
    }

    private void OnEnable()
    {
        Stop();
    }
}
