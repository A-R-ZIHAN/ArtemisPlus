using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviourSingleton<GameFlow>
{
    public StoryManager storyManager;
    
    void Start()
    {
        StoryManager.onAnyStoryStart.AddListener(OnStoryStarted);
        StoryManager.onAnyStoryEnd.AddListener(OnStoryEnded);
    }

    void OnStoryStarted(string storyID)
    {
        switch (storyID)
        {
            case "Intro":
                Debug.Log("Intro story started!");
                break;

            case "chapter1":
                Debug.Log("Chapter 1 started!");
                break;

            case "bossBattle":
                Debug.Log("Boss battle started!");
                break;
        }
    }

    void OnStoryEnded(string storyID)
    {
        switch (storyID)
        {
            case "Intro":
                Debug.Log("Intro story finished!");
                break;

            case "chapter1":
                Debug.Log("Chapter 1 ended!");
                break;

            case "bossBattle":
                Debug.Log("Boss battle ended!");
                break;
        }
    }
}
