using UnityEngine;

namespace Note
{
    public abstract class NoteControllerBase : MonoBehaviour
    {
        public NoteProperty noteProperty;
        public bool isProcessed = false;

        public virtual void OnTapDown(JudgementType judgementType) { }
        public virtual void OnTapUp(JudgementType judgementType) { }
        public virtual void OnFlick(bool isFlicked) { }
    }
}
