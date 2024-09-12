using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
