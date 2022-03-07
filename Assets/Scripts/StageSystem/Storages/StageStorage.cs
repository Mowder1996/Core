using System.Collections;
using System.Collections.Generic;
using Common.Interfaces;
using StageSystem.Interfaces;

namespace StageSystem.Storages
{
    public class StageStorage : IStorage<IStage>, IEnumerable<IStage>
    {
        private readonly List<IStage> _stages = new List<IStage>();

        public int Count => _stages.Count;

        public void Add(IStage item)
        {
            if (_stages.Contains(item))
            {
                return;
            }
            
            _stages.Add(item);
        }

        public IStage Get(int index)
        {
            return index < _stages.Count ? _stages[index] : null;
        }

        public void Clear()
        {
            _stages.Clear();
        }

        #region Enumerator

        public IEnumerator<IStage> GetEnumerator()
        {
            return _stages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }        

        #endregion
    }
}