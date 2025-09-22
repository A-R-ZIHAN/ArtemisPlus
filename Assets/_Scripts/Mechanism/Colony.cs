using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : MonoBehaviour
{
    [SerializeField] private List<Transform> habitatSpawnPoints;
    [SerializeField] private GameObject habitatParent;
    
    [Header("Colony Data")]
    private int habitatQuantity;
    private GameObject habitatPrefab;

    public void Initialize(int habitatQuantity, GameObject habitatPrefab)
    {
        this.habitatQuantity = habitatQuantity;
        this.habitatPrefab = habitatPrefab;
        
        PopulateColonyHabitat();
    }

    private void PopulateColonyHabitat()
    {
        for (int i = 0; i < habitatQuantity; i++)
        {
            GameObject habitat = Instantiate(habitatPrefab, habitatSpawnPoints[i].position, habitatSpawnPoints[i].rotation, transform);
            habitat.transform.SetParent(habitatParent.transform);
        }
    }

    public void PopulateColonyPopulation()
    {
        
    }
}
