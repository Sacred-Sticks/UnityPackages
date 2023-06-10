using System;
using UnityEngine;

namespace Kickstarter.Animations
{
    [RequireComponent(typeof(Animator))]
    public abstract class AnimationController : MonoBehaviour
    {
        private Animator animator;

        protected void Awake()
        {
            animator = GetComponent<Animator>();
        }

        protected void SetAnimation(AnimationSetData parameters)
        {
            animator.Play(parameters.Target, parameters.Layer);
        }

        protected void TransitionAnimation(AnimationTransitionData parameters)
        {
            animator.CrossFade(parameters.Target, parameters.Duration, parameters.Layer);
        }

        protected void ChangeParameter<TValueType>(AnimationParameterChangeData parameters, TValueType value)
        {
            switch (value)
            {
                case float data:
                    animator.SetFloat(parameters.Target, data);
                    break;
                case int data:
                    animator.SetInteger(parameters.Target, data);
                    break;
                case bool data:
                    animator.SetBool(parameters.Target, data);
                    break;
                default:
                    animator.SetTrigger(parameters.Target);
                    break;
            }
        }

        [Serializable]
        protected abstract class AnimationData
        {
            [SerializeField] private string target;
            
            public string Target
            {
                get
                {
                    return target;
                }
            }
        }

        [Serializable]
        protected class AnimationSetData : AnimationData
        {
            [SerializeField] private int layer;

            public int Layer
            {
                get
                {
                    return layer;
                }
            }
        }

        [Serializable]
        protected class AnimationTransitionData : AnimationData
        {
            [SerializeField] private float duration;
            [SerializeField] private int layer;

            public float Duration
            {
                get
                {
                    return duration;
                }
            }
            public int Layer
            {
                get
                {
                    return layer;
                }
            }
        }

        [Serializable]
        protected class AnimationParameterChangeData : AnimationData
        {
            
        }
    }
}
