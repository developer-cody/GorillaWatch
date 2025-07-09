using UnityEngine;

namespace TheGorillaWatch.Utilities
{
    internal class GravityUtils
    {
        public static float ogGrav = Physics.gravity.y;

        public static bool isOn()
        {
            if (Physics.gravity.y != ogGrav) return true;
            return false;
        }

        public static void SetGravity(float gravityValue) => Physics.gravity = new Vector3(0, gravityValue, 0);
    }
}