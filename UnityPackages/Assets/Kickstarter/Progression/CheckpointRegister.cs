using System.Linq;
using Kickstarter.Categorization;
using Kickstarter.Progression;
using UnityEngine;

public class CheckpointRegister : MonoBehaviour
{
    [SerializeField] private CategoryType checkpointType;
    
    public PlayerDataController PlayerDataController { get; set; }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out ObjectCategories categories))
            return;
        if (!categories.Categories.Contains(checkpointType))
            return;
        PlayerDataController.SaveTarget = other.gameObject.transform;
        PlayerDataController.SaveData();
    }
}
