using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewStory", menuName = "Story/Story Data")]
public class StoryData : ScriptableObject
{
    public string storyID;
    public StoryLine[] lines;
    
    [Header("Events")]
    public UnityEvent onStoryStart;
    public UnityEvent onStoryEnd;
}