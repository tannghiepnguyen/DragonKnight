
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkPointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void CheckRespawn()
    {
        //Check if checkpoint is available
        if (currentCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }
        transform.position = currentCheckpoint.position; //Move player to checkPoint
        //Restore health
        playerHealth.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            currentCheckpoint = other.transform;
            SoundManager.instance.PlaySound(checkPointSound);
            other.GetComponent<Collider2D>().enabled = false; //Deactivate checkpoint collider
            other.GetComponent<Animator>().SetTrigger("appear"); // Trigger checkpoint animation;
        }
    }
}
