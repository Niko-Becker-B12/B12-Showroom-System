using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Showroom.UI;
using Sirenix.OdinInspector;

namespace Showroom.WorldSpace
{

    public class ShowroomWorldSpaceBillboard : MonoBehaviour
    {

        public Camera camera;

        public bool invertFace;

        public Vector3 upDirection;

        public bool rotateOnAllAxis;

        public bool resizeByDistance;

        [EnableIf("resizeByDistance")]
        public AnimationCurve resizeCurve = AnimationCurve.Linear(0f, 0.0005f, 2f, 0.0025f);

        private void Awake()
        {
            if (camera == null) camera = Camera.main;
        }
        void Update()
        {
            if (camera == null) return;
            if (rotateOnAllAxis)
            {
                transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward,
                    camera.transform.rotation * Vector3.up);
            }
            else
            {

                transform.rotation = Quaternion.LookRotation(Vector3.Cross(upDirection, Vector3.Cross(camera.transform.forward * (invertFace ? +1 : -1), upDirection)), upDirection);

            }

            if(resizeByDistance)
            {

                float size = resizeCurve.Evaluate(Vector3.Distance(ShowroomNavigation.Instance.playerCamera.transform.position, this.transform.position));

                this.transform.localScale = new Vector3(size, size, size);

            }

        }

    }

}