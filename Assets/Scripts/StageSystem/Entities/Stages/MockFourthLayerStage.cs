using System;

namespace StageSystem.Entities.Stages
{
    public class MockFourthLayerStage : BaseStage
    {
        private readonly string _id = "4";

        public override string Id => _id;
    }
}