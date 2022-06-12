using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume volume;

    private DepthOfField depthOfField;
    private void Awake()
    {
        EventManager.GamePaused.AddListener(Blur);
        EventManager.GameResumed.AddListener(SetDefault);
    }

    private void Start()
    {
        depthOfField = volume.profile.GetSetting<DepthOfField>();
    }

    private void Blur()
    {
        depthOfField.enabled.Override(true);
    }

    private void SetDefault()
    {
        depthOfField.enabled.Override(false);
    }
}
