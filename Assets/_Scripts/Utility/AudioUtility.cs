using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioUtility
{
    static AudioManager s_AudioManager;

    static AudioUtility()
    {
        s_AudioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    public enum AudioGroups
    {
        Master,
        Music,
        Ambience,
        SFX,
        UI
    }

    public static void ChangeAmbience(AudioClip clip)
    {
        if (s_AudioManager == null) s_AudioManager = GameObject.FindObjectOfType<AudioManager>();

        s_AudioManager.SwapTrack(clip);
    }
    public static void CreateSFX(AudioClip clip, Vector3 position, AudioGroups audioGroup, float spatialBlend,
        float rolloffDistanceMin = 10f, float rollofDistanceMax = 15f)
    {
        GameObject impactSfxInstance = new GameObject();
        impactSfxInstance.transform.position = position;
        AudioSource source = impactSfxInstance.AddComponent<AudioSource>();
        source.clip = clip;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.spatialBlend = spatialBlend;
        source.minDistance = rolloffDistanceMin;
        source.maxDistance = rollofDistanceMax;
        source.Play();

        source.outputAudioMixerGroup = GetAudioGroup(audioGroup);

        TimedSelfDestruct timedSelfDestruct = impactSfxInstance.AddComponent<TimedSelfDestruct>();
        timedSelfDestruct.LifeTime = clip.length;
    }
    public static void CreateSFXLoop(AudioClip clip, Vector3 position, AudioGroups audioGroup, float spatialBlend, float period, ref float lastSFXTime,
        float rolloffDistanceMin = 1f)
    {
        if(Time.time - lastSFXTime > period)
        {
            lastSFXTime = Time.time;

            GameObject impactSfxInstance = new GameObject();
            impactSfxInstance.transform.position = position;
            AudioSource source = impactSfxInstance.AddComponent<AudioSource>();
            source.clip = clip;
            source.spatialBlend = spatialBlend;
            source.minDistance = rolloffDistanceMin;
            source.Play();

            source.outputAudioMixerGroup = GetAudioGroup(audioGroup);
            source.maxDistance = 10f;

            TimedSelfDestruct timedSelfDestruct = impactSfxInstance.AddComponent<TimedSelfDestruct>();
            timedSelfDestruct.LifeTime = clip.length;
        }
       
    }
    public static void CreateSFXLoop(List<AudioClip> clips, Vector3 position, AudioGroups audioGroup, float spatialBlend, float period, ref float lastSFXTime,
        float rolloffDistanceMin = 1f)
    {
        if (Time.time - lastSFXTime > period)
        {
            lastSFXTime = Time.time;

            GameObject impactSfxInstance = new GameObject();
            impactSfxInstance.transform.position = position;
            AudioSource source = impactSfxInstance.AddComponent<AudioSource>();

            
            source.clip = clips[UnityEngine.Random.Range(0, clips.Count)];
            source.spatialBlend = spatialBlend;
            source.minDistance = rolloffDistanceMin;
            source.Play();

            source.outputAudioMixerGroup = GetAudioGroup(audioGroup);
            source.maxDistance = 10f;

            TimedSelfDestruct timedSelfDestruct = impactSfxInstance.AddComponent<TimedSelfDestruct>();
            timedSelfDestruct.LifeTime = source.clip.length;
        }

    }
    public static AudioMixerGroup GetAudioGroup(AudioGroups group)
    {
        if(s_AudioManager == null) s_AudioManager = GameObject.FindObjectOfType<AudioManager>();

        var groups = s_AudioManager.FindMatchingGroups(group.ToString());

        if (groups.Length > 0)
            return groups[0];

        Debug.LogWarning("Didn't find audio group for " + group.ToString());
        return null;
    }

    public static void SetMasterVolume(float value)
    {
        if (s_AudioManager == null) s_AudioManager = GameObject.FindObjectOfType<AudioManager>();

        if (value <= 0)
            value = 0.001f;
        float valueInDb = Mathf.Log10(value) * 20;

        s_AudioManager.SetFloat("MasterVolume", valueInDb);
    }

    public static float GetMasterVolume()
    {
        if (s_AudioManager == null) s_AudioManager = GameObject.FindObjectOfType<AudioManager>();

        s_AudioManager.GetFloat("MasterVolume", out var valueInDb);
        return Mathf.Pow(10f, valueInDb / 20.0f);
    }
}
