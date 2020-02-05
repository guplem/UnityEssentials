using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    public class WorldAnimationsManager : MonoBehaviour
    {
        [SerializeField] private List<WorldAnimation> animations;



        public void Play(WorldAnimation animation)
        {
            if (animations == null)
                animations = new List<WorldAnimation>();
                
            animations.Add(animation);
        }

        private void Update()
        {
            if (animations != null)
            {
                List<WorldAnimation> animationsToRemove = new List<WorldAnimation>();
                foreach (WorldAnimation animation in animations)
                {
                    if (animation.Step(Time.deltaTime))
                        animationsToRemove.Add(animation);
                }

                foreach (WorldAnimation animation in animationsToRemove)
                {
                    animations.Remove(animation);
                }
            }

        }
    }
}