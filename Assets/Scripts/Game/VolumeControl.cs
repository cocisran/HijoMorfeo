using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; 

public class VolumeControl : MonoBehaviour
{

    public void SetVolume(float volume)
    {
                
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in sources)
        {
             source.volume = volume;
        }
    }
}