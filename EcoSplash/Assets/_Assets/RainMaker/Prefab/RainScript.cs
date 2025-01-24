//
// Rain Maker (c) 2015 Digital Ruby, LLC
// http://www.digitalruby.com
//

using UnityEngine;
using System.Collections;

namespace DigitalRuby.RainMaker
{
    public class RainScript : BaseRainScript
    {
        [Tooltip("The height above the camera that the rain will start falling from")]
        public float RainHeight = 25.0f;

        [Tooltip("How far the rain particle system is ahead of the player")]
        public float RainForwardOffset = -7.0f;

        [Tooltip("The top y value of the mist particles")]
        public float RainMistHeight = 3.0f;

        private void UpdateRain()
        {
            if (Camera == null)
            {
                Camera = Camera.main;
            }

            if (Camera == null)
            {
                Debug.LogError("No camera available for rain script");
                enabled = false;
                return;
            }

            if (RainFallParticleSystem != null)
            {
                var s = RainFallParticleSystem.shape;
                s.shapeType = ParticleSystemShapeType.ConeVolume;

                if (FollowCamera)
                {
                    RainFallParticleSystem.transform.position = Camera.transform.position;
                    RainFallParticleSystem.transform.Translate(0.0f, RainHeight, RainForwardOffset);
                    RainFallParticleSystem.transform.rotation = Quaternion.Euler(0.0f, Camera.transform.rotation.eulerAngles.y, 0.0f);
                }

                if (RainMistParticleSystem != null)
                {
                    var s2 = RainMistParticleSystem.shape;
                    s2.shapeType = FollowCamera ? ParticleSystemShapeType.Hemisphere : ParticleSystemShapeType.Box;

                    Vector3 pos = FollowCamera 
                        ? Camera.transform.position 
                        : RainFallParticleSystem.transform.position;

                    pos.y += RainMistHeight;
                    RainMistParticleSystem.transform.position = pos;
                }
            }
        }
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            UpdateRain();
        }
    }
}