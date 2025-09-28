using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    public Camera surfaceCamera;
    public Camera habitatViewCamera;

    public List<HabitatManager> habitatManagers;
    
    public int selectedHabitatNo;
    
    public List<GameObject> habitatsPrefabs;
    public Colony colonyTemplate;

    [Header("Mission Data")] 
    public int missionCrewAmount = 4;
    public string missionDuration = "Short - 60 days";
    public string missionLocation = "Moon";

    public Camera uiCam;

    public override void Awake()
    {
        base.Awake();
        ToggleSurfaceCamera(true);
        ToggleHabitatViewCamera(false);
    }

    public void ToggleSurfaceCamera(bool flag)
    {
        surfaceCamera.gameObject.SetActive(flag);
    }

    public void ToggleHabitatViewCamera(bool flag)
    {
        habitatViewCamera.gameObject.SetActive(flag);
    }

    public void TriggerColony()
    {
        GameObject habitatPrefab = habitatsPrefabs[selectedHabitatNo-1];
        colonyTemplate.ReInitializeColonyhabitat(habitatPrefab);
    }
}
