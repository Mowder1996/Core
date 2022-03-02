using System;
using System.Collections.Generic;
using StageSystem.Interfaces;
using Zenject;

namespace StageSystem.Entities.Stages
{
    public class MockThirdLayerStage : BaseStage
    {
        private readonly string _id = "3";

        public override string Id => _id;

        public MockThirdLayerStage([Inject(Id = "Fourth layer")] List<IStage> fourthLayerStages)
        { 
            Init(fourthLayerStages);
        }
    }
}