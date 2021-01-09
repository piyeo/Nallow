using UnityEngine;

namespace Note
{
    public abstract class NoteControllerBase : MonoBehaviour
    {
        public NoteProperty noteProperty;
        public virtual void OnTap(JudgementType judgementType) { }
    }
}
