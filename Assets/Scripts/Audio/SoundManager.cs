using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DevZilio.Core.Singleton;

public class SoundManager : Singleton<SoundManager>
{
 public List<MusicSetup> musicSetups;
 public List<SFXSetup> sfxSetups;

public AudioSource musicSource;

public void PlayMusicByType(MusicType musicType)
{
    var music = GetMusicByType(musicType);
    musicSource.clip = music.audioClip;
    musicSource.Play();
}

public MusicSetup GetMusicByType(MusicType musicType)
{
    return musicSetups.Find(i => i.musicType == musicType);
}


public SFXSetup GetSFXByType(SFXType sfxType)
{
    return sfxSetups.Find(i => i.sfxType == sfxType);
}




}

public enum MusicType
{
    NONE,
    TYPE_01,
    TYPE_02,
    TYPE_03,

}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;

}

public enum SFXType
{
    NONE,
    TYPE_COIN,
    TYPE_LIFE,
    TYPE_CHECKPOINT,
    TYPE_RECOVERYLIFE,
    TYPE_JUMP,
    TYPE_GUN1,
    TYPE_GUN2,
    TYPE_GUN3,
    TYPE_BODYSHOOTPLAYER,
    TYPE_BODYSHOOTENEMY,
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip audioClip;

}