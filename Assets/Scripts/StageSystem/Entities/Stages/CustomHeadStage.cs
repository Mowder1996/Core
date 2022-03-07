using System.Collections.Generic;
using StageSystem.Interfaces;

namespace StageSystem.Entities.Stages
{
    public class CustomHeadStage : BaseStage
    {
        public override string Id => "CustomHeadStage";

        public CustomHeadStage(IEnumerable<IStage> subStages)
        {
            Init(subStages);
        }
    }
}