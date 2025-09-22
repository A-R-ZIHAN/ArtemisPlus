using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    public int selectedHabitatNo;
    public int habitatQuantity;
    
    public List<GameObject> habitatsPrefabs;
    public Colony colonyTemplate;

    public void TriggerColony()
    {
        GameObject habitatPrefab = habitatsPrefabs[selectedHabitatNo-1];
        colonyTemplate.Initialize(habitatQuantity,habitatPrefab);
    }
}
