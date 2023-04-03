using UnityEngine;

namespace OceanToolkit
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class OceanCamera : MonoBehaviour
    {
        protected Camera cam;

        public void Start()
        {
            cam = GetComponent<Camera>();
        }

        public void Update()
        {
            if (cam.depthTextureMode != DepthTextureMode.DepthNormals)
            {
                cam.depthTextureMode = DepthTextureMode.DepthNormals;
            }
        }
    }
}