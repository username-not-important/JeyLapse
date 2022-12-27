using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using JLEditorLib.Effects.ColorFilters;

namespace JLEditorLib.Effects
{
    public class FXCollection : FX
    {
        protected List<FX> _collection;

        public FXCollection()
        {
            _collection = new List<FX>();
        }

        public FX this[int index]
        {
            get { return _collection[index]; }
        }

        public int Count
        {
            get { return _collection.Count; }
        }

        public void AddEffect(FX fx)
        {
            _collection.Add(fx);
        }

        public void RemoveEffectAt(int index)
        {
            _collection.RemoveAt(index);
        }

        public bool Contains<T>() where T : FX
        {
            return _collection.Any(fx => fx as T != null);
        }

        public void FreeMemory()
        {
            if (!Contains<CobaltFX>())
                CobaltFX.FreeMemory();
            
            if (!Contains<LightLeak>())
                LightLeak.FreeMemory();
        }

        public void Clear()
        {
            _collection = new List<FX>();
        }

        public override Color Apply(Color input)
        {
            foreach (var fx in _collection)
            {
                input = fx.Apply(input);
            }

            return input;
        }

        public override WriteableBitmap Apply(WriteableBitmap input)
        {
            foreach (FX fx in _collection)
            {
                fx.Apply(input);
            }

            return input;
        }
    }
}