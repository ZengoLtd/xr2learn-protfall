using UnityEngine;

namespace BNG {
    public class SetGroundPosition : MonoBehaviour {

        public Transform FollowTarget;
        public bool MatchRotation = true;

        public float YOffset = 0;
        public bool SetOnlyInYAxis = false;

        private void Start()
        {
            if (FollowTarget == null)
            {
                FollowTarget = GameObject.Find("XR Rig Advanced/PlayerController")?.transform;
            }
        }

        void Update() {
            if(FollowTarget) {
                transform.position = FollowTarget.position;

                if(YOffset != 0) {
                    if (SetOnlyInYAxis)
                    {
                        float currentYValue = transform.position.y;
                        transform.position = new Vector3(0, currentYValue + YOffset, 0);
                    }
                    else 
                    {
                        transform.position += new Vector3(0, YOffset, 0);
                    }
                }

                if(MatchRotation) {
                    transform.rotation = FollowTarget.rotation;
                }
            }
        }
    }
}