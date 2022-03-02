using System;
using System.Collections.Generic;
using StageSystem.Interfaces;
using Zenject;

namespace StageSystem.Entities.Stages
{
    public class MockSecondLayerStage : BaseStage
    {
        private readonly string _id = "2";

        public override string Id => _id;

        public MockSecondLayerStage([Inject(Id = "Third layer")] List<IStage> thirdLayerStages)
        {
            Init(thirdLayerStages);
        }
    }
}