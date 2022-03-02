using System.Collections.Generic;
using Common.Interfaces;
using Cysharp.Threading.Tasks;

namespace StageSystem.Interfaces
{
    public interface IStage : IIdentified
    {
        IEnumerable<IStage> SubStages { get; }
        UniTask Execute();
        void Skip();
    }   
}
