using UnityEngine;
using System.Collections;

namespace CovertPath.Utilities {   
    public class Billboard : MonoBehaviour {
        [SerializeField] private bool minimap = false;
        void LateUpdate() {
            if (minimap == false)
                transform.LookAt(
                    transform.position + Camera.main.transform.rotation * Vector3.forward,
                    Camera.main.transform.rotation * Vector3.up
                );
            else
                transform.LookAt(
                    transform.position + GameObject.FindWithTag("MinimapCamera").transform.rotation * Vector3.forward,
                    GameObject.FindWithTag("MinimapCamera").transform.rotation * Vector3.up
                );
        }
    }
}