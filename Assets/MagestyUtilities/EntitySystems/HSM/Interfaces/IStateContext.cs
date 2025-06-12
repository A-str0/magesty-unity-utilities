using System.Collections.Generic;
using MagestyUtilities.EntitySystem.Interfaces;

namespace MagestyUtilities.EntitySystem.HSM.Interfaces
{
    public interface IStateContext
    {
        IReadOnlyList<IController> Controllers { get; }
        IInputSource InputSource { get; }
    }
}