using Unity.VisualScripting;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [SerializeField]
    private float healthValue;
    [Header("SFX")]
    [SerializeField] private AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(collectSound);
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
