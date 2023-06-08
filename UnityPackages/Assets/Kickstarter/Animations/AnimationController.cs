using System.Linq;
using Kickstarter.Events;
using UnityEngine;

namespace Kickstarter.Animations
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private AnimationTransition[] animationTransitions;

        private Animator animator;

        private void Awake()
        {
            if (animationTransitions.Length == 0)
                Destroy(this);
            animator = GetComponent<Animator>();
        }

        private System.Collections.IEnumerator Start()
        {
            animationTransitions[0].PlayAnimation(animator);
            EventManager.AddListener<AnimationEvent>(TriggerAnimation);
            yield return new WaitForSeconds(5);
            EventManager.Trigger(new AnimationEvent("New State 0"));
        }

        private void TriggerAnimation(AnimationEvent parameters)
        {
            var animationState = animationTransitions.FirstOrDefault(s => s.StateName == parameters.StateName);
            if (animationState == default)
            {
                Debug.LogWarning($"No Animate State is named {parameters.StateName}");
                return;
            }
            animationState.CrossFadeAnimation(animator);
        }

        [System.Serializable]
        private class AnimationTransition
        {
            [SerializeField] private string stateName;
            [SerializeField] private string stateLayer;
            [Space]
            [SerializeField] private float transitionDuration;
            [SerializeField] private float transitionTimeOffset;

            public string StateName
            {
                get
                {
                    return stateName;
                }
            }

            public void PlayAnimation(Animator animator)
            {
                if (GetLayerIndex(animator, out int layerIndex))
                    return;
                if (animator.HasState(layerIndex, Animator.StringToHash(StateName)))
                    animator.Play(StateName);
                else 
                    Debug.LogWarning($"No Animation State Found Titled '{StateName}'");
            }

            private bool GetLayerIndex(Animator animator, out int layerIndex)
            {
                layerIndex = animator.GetLayerIndex(stateLayer);
                if (layerIndex != -1)
                    return false;
                Debug.LogWarning($"{stateLayer} Layer Does Not Exist in the Animation Controller");
                return true;
            }

            public void CrossFadeAnimation(Animator animator)
            {
                if (GetLayerIndex(animator, out int layerIndex))
                    return;
                animator.CrossFade(StateName, transitionDuration, layerIndex, transitionTimeOffset);
            }
        }

        public class AnimationEvent
        {
            public string StateName { get; }

            public AnimationEvent(string stateName)
            {
                StateName = stateName;
            }
        }
    }
}
