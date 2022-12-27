using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JLEditorLib.Effects;

namespace JeyLapse.Editing
{
    public class ImageEditor
    {
        private static FXCollection _fxCollection;

        public static FXCollection FxCollection
        {
            get { return _fxCollection ?? (_fxCollection = new FXCollection()); }
            set { _fxCollection = value; }
        }

    }
}
