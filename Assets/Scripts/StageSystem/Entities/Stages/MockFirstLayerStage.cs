using System;
using System.Collections.Generic;
using StageSystem.Interfaces;
using Zenject;

namespace StageSystem.Entities.Stages
{
    public class MockFirstLayerStage : BaseStage
    {
        private readonly string _id = "1";

        public override string Id => _id;

        public MockFirstLayerStage([Inject(Id = "Second layer")] List<IStage> secondLayerStages)
        {
            Init(secondLayerStages);
        }
    }
}