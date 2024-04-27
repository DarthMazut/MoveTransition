using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveTransition
{
    public class MoveTransition : TransformOperationsTransition
    {
        public MoveTransition()
        {
            Property = Visual.RenderTransformProperty;
        }
    }
}
