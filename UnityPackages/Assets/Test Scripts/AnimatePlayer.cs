using System;
using Kickstarter.Animations;
using Kickstarter.Events;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AnimatePlayer : MonoBehaviour
{
    [SerializeField] private FloatInput jumpInput;
    [SerializeField] private Vector2Input movementInput; 
    [SerializeField] private Player.PlayerIdentifier player;
    [SerializeField] private string XMoveParameter;
    [SerializeField] private string YMoveParameter;

    private const float TOLERANCE = 0.5f;

    private string eventKey;

    private void Start()
    {
        ConnectToAnimationController();
        jumpInput.SubscribeToInputAction(OnJumpInputChange, player);
        movementInput.SubscribeToInputAction(OnMovementInputChange, player);
        EventManager.AddListener<Movement.GroundedEvent>(OnGrounded);
    }

    private void ConnectToAnimationController()
    {
        var animationController = GetComponentInChildren<AnimationController>();
        if (animationController == null)
        {
            Debug.LogWarning("No Animation Controller Found");
            return;
        }
        eventKey = animationController.AnimationEventSpecifier;
        animationController.OnAnimationEventSpecifierChange += s => eventKey = s;
    }

    private void OnJumpInputChange(float input)
    {
        if (input < TOLERANCE)
            return;
        EventManager.Trigger($"{eventKey}{AnimationController.TransitionEventExtension}", 
            new AnimationController.AnimationTransitionEvent("Jump", 0.25f, 0));
    }

    private void OnMovementInputChange(Vector2 input)
    {
        EventManager.Trigger($"{eventKey}{AnimationController.ParameterChangeEventExtension}", 
            new AnimationController.AnimationParameterChangeEvent<float>(XMoveParameter, input.x));
        EventManager.Trigger($"{eventKey}{AnimationController.ParameterChangeEventExtension}", 
            new AnimationController.AnimationParameterChangeEvent<float>(YMoveParameter, input.y));
    }

    private void OnGrounded(Movement.GroundedEvent parameters)
    {
        EventManager.Trigger($"{eventKey}{AnimationController.TransitionEventExtension}", 
            new AnimationController.AnimationTransitionEvent("Base Tree", 0.5f, 0));
    }
}
