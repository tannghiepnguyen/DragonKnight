using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        ChangeMusicVolumn(0);
        ChangeSoundVolumn(0);

    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }

    public void ChangeSoundVolumn(float _change)
    {
        ChangeSourceVolumn(1, "SoundVolumn", _change, soundSource);
    }

    public void ChangeMusicVolumn(float _change)
    {
        ChangeSourceVolumn(0.3f, "MusicVolumn", _change, musicSource);
    }

    private void ChangeSourceVolumn(float baseVolumn, string volumnName, float change, AudioSource source)
    {
        float currentVolumn = PlayerPrefs.GetFloat(volumnName);
        currentVolumn += change;

        if (currentVolumn < 0)
        {
            currentVolumn = 1;
        }
        else if (currentVolumn > 1)
        {
            currentVolumn = 0;
        }

        float finalVolumn = currentVolumn * baseVolumn;
        source.volume = finalVolumn;

        PlayerPrefs.SetFloat(volumnName, currentVolumn);

    }
}
