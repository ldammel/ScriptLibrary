using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("There can only be one soundmanager in the scene!");
            Application.Quit();
        }

        Instance = this;
    }

    public AudioClip[] clips;
    public AudioSource source;

    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "sound1":
                source.clip = clips[0];
                source.Play();
                return;
            case "sound2":
                source.clip = clips[1];
                source.Play();
                return;
            case "sound3":
                source.clip = clips[2];
                source.Play();
                return;
            default:
                return;
        }
    }
}

