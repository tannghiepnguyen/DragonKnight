using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    [SerializeField] private RectTransform[] options;
    private int currentPosition;
    private PlayerControl playerControl;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        playerControl = new PlayerControl();
        playerControl.Menu.Up_D.performed += x => ChangePosition(-1);
        playerControl.Menu.Down_D.performed += x => ChangePosition(1);
        playerControl.Menu.Confirm.performed += x => Interact();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        //Interact
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;
        if (_change != 0)
        {
            SoundManager.instance.PlaySound(changeSound);
        }
        if (currentPosition < 0)
        {
            currentPosition = options.Length - 1;
        }
        else if (currentPosition >= options.Length)
        {
            currentPosition = 0;
        }
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, rect.position.z);
    }
}
