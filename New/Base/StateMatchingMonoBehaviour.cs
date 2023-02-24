
using Sirenix.OdinInspector;

namespace Nino.NewStateMatching
{
    public abstract class StateMatchingMonoBehaviour : SerializedMonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Remove();
    }
}

