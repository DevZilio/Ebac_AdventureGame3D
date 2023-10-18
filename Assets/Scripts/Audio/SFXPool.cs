using System.Collections;
using System.Collections.Generic;
using DevZilio.Core.Singleton;
using UnityEngine;
using UnityEngine.Audio;


public class SFXPool : Singleton<SFXPool>
{
    private List<AudioSource> _audioSourceList;

    public int poolSize = 10;

    private int _index = 0;

    public AudioMixerGroup sfxAudioMixerGroup;


    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
        }
    }

    private void CreateAudioSourceItem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        var audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = sfxAudioMixerGroup;
        _audioSourceList.Add(audioSource);
    }

    public void Play(SFXType sfxType)
    {
        if (sfxType == SFXType.NONE) return;
        var sfx = SoundManager.Instance.GetSFXByType(sfxType);

        _audioSourceList[_index].clip = sfx.audioClip;
        _audioSourceList[_index].Play();

        _index++;
        if (_index >= _audioSourceList.Count) _index = 0;
    }
}
