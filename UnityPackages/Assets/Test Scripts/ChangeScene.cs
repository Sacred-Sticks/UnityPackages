using Kickstarter.Events;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using Kickstarter.Stages;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private FloatInput changeLevelInput;

    private void Start()
    {
        changeLevelInput.SubscribeToInputAction(OnInput, Player.PlayerIdentifier.KeyboardAndMouse);
    }

    private void OnInput(float input)
    {
        if (input == 0)
            return;
        EventManager.Trigger("Scene.Load", new SceneController.SceneChangeEvent("New Scene"));
    }
}
