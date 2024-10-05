using UnityEngine;

namespace TheGorillaWatch.Models
{
    public abstract class Page : MonoBehaviour
    {
        public virtual bool modEnabled {  get; set; }

        public abstract string modName { get; }

        public virtual string info { get; set; } = "";

        public virtual PageType pageType { get; } = PageType.Toggle;

        public virtual void Enable() { modEnabled = true; }
               
        public virtual void Disable() { modEnabled = false; }

        public virtual void OnUpdate() {  }

        public virtual void Init() { }
    }
}
