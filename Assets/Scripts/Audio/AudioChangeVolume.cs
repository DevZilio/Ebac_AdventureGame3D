using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioChangeVolume : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer group;
    public string floatParam = "MyEsposedParam";

    public void ChangeValue(float f)
    {
        group.SetFloat(floatParam, f);
    }

}
