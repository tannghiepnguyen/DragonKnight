using UnityEngine;

public class Arrowtrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    [Header("SFX")]
    [SerializeField] private AudioClip shootSound;
    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;
        SoundManager.instance.PlaySound(shootSound);
        int index = FindArrows();
        arrows[index].transform.position = firePoint.position;
        arrows[index].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }

}
