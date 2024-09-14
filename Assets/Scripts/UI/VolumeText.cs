using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumnName;
    [SerializeField] private string textInfo;
    private Text txt;
    private void Awake()
    {
        txt = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        float volumnValue = PlayerPrefs.GetFloat(volumnName) * 100;
        txt.text = textInfo + volumnValue.ToString();
    }
}
