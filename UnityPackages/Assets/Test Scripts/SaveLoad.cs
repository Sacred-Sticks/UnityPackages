using Kickstarter.Progression;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private string[] dataFileLocations;

    private void OnDestroy()
    {
        if (dataFileLocations.Length > 0)
            SaveManager.SaveData(transform.position, dataFileLocations[0]);
        if (dataFileLocations.Length > 1)
            SaveManager.SaveData(transform.rotation, dataFileLocations[1]);
    }

    private void Awake()
    {
        if (dataFileLocations.Length > 0)
            if (SaveManager.LoadData(dataFileLocations[0], out Vector3 loadedPosition))
                transform.position = loadedPosition;
        if (dataFileLocations.Length > 1)
            if (SaveManager.LoadData(dataFileLocations[1], out Quaternion loadedRotation))
                transform.rotation = loadedRotation;
    }
}
