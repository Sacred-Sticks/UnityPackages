using Kickstarter.Events;
using Kickstarter.Identification;
using Kickstarter.Progression.Types;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(SaveMe))]
public class SaveMeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var saveMe = (SaveMe)target;

        switch (Application.isPlaying)
        {
            case true:
                if (GUILayout.Button("Test Save"))
                {
                    saveMe.TriggerSave();
                }
                break;
            case false:
                DrawDefaultInspector();
                break;
        }
    }
}
#endif

[RequireComponent(typeof(Player))]
public class SaveMe : MonoBehaviour
{
    [SerializeField] private string eventKeySpecifier;
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void TriggerSave()
    {
        EventManager.Trigger($"{player.PlayerID}.{eventKeySpecifier}", new QuickSaveRegister.TriggerQuickSaveEvent(transform));
    }
}
