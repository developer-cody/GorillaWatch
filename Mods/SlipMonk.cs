using GorillaLocomotion;
using System.Collections.Generic;
using TheGorillaWatch.Models;
using UnityEngine;

namespace TheGorillaWatch.Mods
{
    class SlipMonk : Page
    {
        public override string modName => "SlipMonk";
        private List<MeshCollider> colliders = new List<MeshCollider>();

        public override void Enable()
        {
            base.Enable();

            foreach (MeshCollider meshCollider in colliders)
            {
                meshCollider.GetComponent<GorillaSurfaceOverride>().overrideIndex = 61;
            }

            //Collider[] gameObjects = GameObject.Find("Level").GetComponentsInChildren<Collider>();

            /*for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObject[i].

                int currentOverrideIndex = gameObjects[i].transform.gameObject.GetComponent<GorillaSurfaceOverride>().overrideIndex;
                Destroy(gameObjects[i].transform.gameObject.GetComponent<GorillaSurfaceOverride>());

                GorillaSurfaceOverride newSurfaceOverride = gameObjects[i].transform.gameObject.AddComponent<GorillaSurfaceOverride>();
                newSurfaceOverride.overrideIndex = currentOverrideIndex;
            }*/
        }

        public override void Disable()
        {
            base.Disable();

            foreach (MeshCollider meshCollider in colliders)
            {
                meshCollider.GetComponent<GorillaSurfaceOverride>().overrideIndex = 0;
            }

            /*Collider[] gameObjects = GameObject.Find("Level").GetComponentsInChildren<Collider>();

            foreach (var collider in gameObjects)
            {
                var surfaceOverride = collider.transform.gameObject.GetComponent<GorillaSurfaceOverride>();
                if (surfaceOverride != null)
                {
                    Destroy(surfaceOverride);
                }
            }*/
        }

        public override PageType pageType => PageType.Toggle;
    }
}
