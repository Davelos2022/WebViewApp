using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioClip push;
    [SerializeField] private AudioClip goal;
    [SerializeField] private AudioClip miss;

    private AudioSource audioSource;

    public enum Sounds {Push, Goal, Miss}

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(Sounds sounds)
    {
        switch (sounds)
        {
            case Sounds.Push:
                audioSource.PlayOneShot(push);
                break;

            case Sounds.Goal:
                audioSource.PlayOneShot(goal);
                break;

            case Sounds.Miss:
                audioSource.PlayOneShot(miss);
                break;

            default:
                break;
        }
    }

}
