using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineChain : MonoBehaviour
{
    public PlayableDirector _nextTimeline;
    
    void Start()
    {
        PlayableDirector pd = GetComponent<PlayableDirector>();
        pd.stopped += OnCutSceneFinished;
    }

    void OnCutSceneFinished(PlayableDirector pd)
    {
        if (_nextTimeline != null)
        {
            _nextTimeline.Play();
        }
    }
}
