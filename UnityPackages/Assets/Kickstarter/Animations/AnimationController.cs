using System;
using Kickstarter.Events;
using UnityEngine;

namespace Kickstarter.Animations
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private string animationEventSpecifier;

        public const string TransitionEventExtension = ".Animations.Transition";
        public const string SetEventExtension = ".Animations.Set";
        public const string ParameterChangeEventExtension = ".Animations.ParameterChange";
        
        public string AnimationEventSpecifier
        {
            get
            {
                return animationEventSpecifier;
            }
            set
            {
                string oldSpecifier = AnimationEventSpecifier;
                animationEventSpecifier = value;
                UpdateSubscriptions(oldSpecifier, value);
                OnAnimationEventSpecifierChange?.Invoke(AnimationEventSpecifier);
            }
        }
        
        public Action<string> OnAnimationEventSpecifierChange { get; set; }

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            EventManager.AddListener<AnimationSetEvent>($"{AnimationEventSpecifier}{SetEventExtension}", SetAnimation);
            EventManager.AddListener<AnimationTransitionEvent>($"{AnimationEventSpecifier}{TransitionEventExtension}", TransitionAnimation);
            EventManager.AddListener<AnimationParameterChangeEvent>($"{AnimationEventSpecifier}{ParameterChangeEventExtension}", ChangeParameter);
        }

        private void UpdateSubscriptions(string oldSpecifier, string newSpecifier)
        {
            EventManager.RemoveListener<AnimationSetEvent>($"{oldSpecifier}{SetEventExtension}", SetAnimation);
            EventManager.RemoveListener<AnimationTransitionEvent>($"{oldSpecifier}{TransitionEventExtension}", TransitionAnimation);
            EventManager.RemoveListener<AnimationParameterChangeEvent>($"{oldSpecifier}{SetEventExtension}", ChangeParameter);
            
            EventManager.AddListener<AnimationSetEvent>($"{newSpecifier}{SetEventExtension}", SetAnimation);
            EventManager.AddListener<AnimationTransitionEvent>($"{newSpecifier}{TransitionEventExtension}", TransitionAnimation);
            EventManager.AddListener<AnimationParameterChangeEvent>($"{newSpecifier}{SetEventExtension}", ChangeParameter);
        }

        private void SetAnimation(AnimationSetEvent parameters)
        {
            animator.Play(parameters.Target, parameters.Layer);
        }

        private void TransitionAnimation(AnimationTransitionEvent parameters)
        {
            animator.CrossFade(parameters.Target, parameters.Duration, parameters.Layer);
        }

        private void ChangeParameter(AnimationParameterChangeEvent parameters)
        {
            switch (parameters)
            {
                case AnimationParameterChangeEvent<float> data:
                    animator.SetFloat(data.Target, data.Value);
                    break;
                case AnimationParameterChangeEvent<int> data:
                    animator.SetInteger(data.Target, data.Value);
                    break;
                case AnimationParameterChangeEvent<bool> data:
                    animator.SetBool(data.Target, data.Value);
                    break;
                default:
                    animator.SetTrigger(parameters.Target);
                    break;
            }
        }

        public abstract class AnimationEvent
        {
            public string Target { get; }

            protected AnimationEvent(string target)
            {
                Target = target;
            }
        }
        
        public class AnimationSetEvent : AnimationEvent
        {
            public int Layer { get; }
            
            public AnimationSetEvent(string target, int layer) : base(target)
            {
                Layer = layer;
            }
        }

        public class AnimationTransitionEvent : AnimationEvent
        {
            public float Duration { get; }
            public int Layer { get; }

            public AnimationTransitionEvent(string target, float duration, int layer) : base(target)
            {
                Duration = duration;
                Layer = layer;
            }
        }

        public class AnimationParameterChangeEvent : AnimationEvent
        {
            public AnimationParameterChangeEvent(string target) : base(target)
            {
            }
        }

        public class AnimationParameterChangeEvent<T> : AnimationParameterChangeEvent
        {
            public T Value { get; }
            
            public AnimationParameterChangeEvent(string target, T value) : base(target)
            {
                Value = value;
            }
        }
    }
}
