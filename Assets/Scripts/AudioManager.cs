using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------------- Audio Source ---------------")]
    [SerializeField] AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("--------------- Audio Clip -----------------")]
    public AudioClip background;
    public AudioClip collision;
    public AudioClip jump;
    public AudioClip fuel;
    public AudioClip goal;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
