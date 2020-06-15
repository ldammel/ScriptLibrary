using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider music;
    public Slider sfx;
    public AudioMixer mixer;

    private void Update()
    {
        mixer.SetFloat("Music", music.value);
        mixer.SetFloat("SFX", sfx.value);
    }
}
