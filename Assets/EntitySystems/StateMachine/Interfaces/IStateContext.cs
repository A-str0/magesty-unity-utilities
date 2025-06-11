using System.Collections.Generic;
using EntitySystems.Interfaces;

namespace EntitySystems.StatesSystem.Interfaces
{
    public interface IStateContext
    {
        IReadOnlyList<IController> Controllers { get; }
        IInputSource InputSource { get; }
    }
}