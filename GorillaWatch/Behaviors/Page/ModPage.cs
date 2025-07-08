using System.Collections.Generic;
using UnityEngine;

namespace TheGorillaWatch.Behaviors.Page
{
    public enum PageType 
    { 
        Toggle, 
        Information
    }

    public abstract class ModPage : MonoBehaviour
    {
        public bool modEnabled { get; set; }

        public virtual string modName { get; }
        public virtual string info { get; set; } = "";

        public virtual List<string> requiredModNames { get; } = new List<string>();
        public virtual List<string> incompatibleModNames { get; } = new List<string>();

        public virtual void Init() { }
        public virtual void Enable() { modEnabled = true; }
        public virtual void Disable() { modEnabled = false; }
        public virtual void OnUpdate() { }
        
        public virtual PageType pageType { get; } = PageType.Toggle;
    }
}