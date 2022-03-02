using System.Collections.Generic;
using StageSystem.Entities.Stages.Mock.SelectMap.SelectEnvironment;

namespace StageSystem.Entities.Stages.Mock.SelectMap
{
    public class MockSelectEnvironmentStage : MockSelectMapSubStage
    {
        public override string Id => "SelectEnvironment";

        public MockSelectEnvironmentStage(List<MockSelectEnvironmentSubStage> selectEnvironmentSubStages)
        {
            Init(selectEnvironmentSubStages);
        }
    }
}