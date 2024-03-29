using Kickstarter.Animations;
using Kickstarter.Events;
using Kickstarter.Identification;
using Kickstarter.Inputs;
using UnityEngine;

public sealed class AnimatePlayer : AnimationController
{
    [SerializeField] private FloatInput jumpInput;
    [SerializeField] private Vector2Input movementInput;
    [Space(20)]
    [SerializeField] private AnimationTransitionData jumpAnimationData;
    [SerializeField] private AnimationTransitionData groundedAnimationData;
    [SerializeField] private AnimationParameterChangeData xMovementData;
    [SerializeField] private AnimationParameterChangeData yMovementData;

    private Player player;

    private const float TOLERANCE = 0.5f;

    private new void Awake()
    {
        base.Awake();
        player = GetComponentInParent<Player>();
        EventManager.AddListener<Movement.GroundedEvent>(OnGrounded);
    }
    
    private void Start()
    {
        jumpInput.SubscribeToInputAction(OnJumpInputChange, player.PlayerID);
        movementInput.SubscribeToInputAction(OnMovementInputChange, player.PlayerID);
    }

    private void OnJumpInputChange(float input)
    {
        if (input < TOLERANCE)
            return;
        TransitionAnimation(jumpAnimationData);
    }

    private void OnMovementInputChange(Vector2 input)
    {
        ChangeParameter(xMovementData, input.x);
        ChangeParameter(yMovementData, input.y);
    }

    private void OnGrounded(Movement.GroundedEvent parameters)
    {
        TransitionAnimation(groundedAnimationData);
    }
}
