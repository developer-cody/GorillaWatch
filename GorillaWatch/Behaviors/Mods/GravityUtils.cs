using UnityEngine;

namespace TheGorillaWatch.Behaviors.Mods
{
    internal class GravityUtils
    {
        public static bool isOn;
        public static float ogGrav = Physics.gravity.y;

        public static void SetGravity(float gravityValue) => Physics.gravity = new Vector3(0, gravityValue, 0);
    }
}