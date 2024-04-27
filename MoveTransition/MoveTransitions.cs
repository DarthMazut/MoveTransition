using Avalonia;
using Avalonia.Animation;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media.Transformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MoveTransition
{
    public class MoveTransitions : AvaloniaObject
    {
        static MoveTransitions()
        {
            TransitionProperty.Changed.AddClassHandler<Layoutable>(HandleTransitionChanged);
        }

        public static readonly AttachedProperty<MoveTransition?> TransitionProperty
            = AvaloniaProperty.RegisterAttached<MoveTransitions, Layoutable, MoveTransition?>(
                "Transition", default, false, BindingMode.OneTime);

        private static void HandleTransitionChanged(Layoutable layoutable, AvaloniaPropertyChangedEventArgs args)
        {
            if (args.NewValue is not null)
            {
                layoutable.PropertyChanged += HostLayoutablePropertyChanged;
            }
            else
            {
                layoutable.PropertyChanged -= HostLayoutablePropertyChanged;
            }
        }

        private static async void HostLayoutablePropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property != Visual.BoundsProperty)
            {
                return;
            }

            // Get host control
            Layoutable host = (sender as Layoutable)!; 
            //Unsubscribe from position changed, otherwise we'll endup with recursive calls
            host.PropertyChanged -= HostLayoutablePropertyChanged;
            // Caluclate move vector
            Vector moveVector = ((Rect)e.NewValue!).Center - ((Rect)e.OldValue!).Center;
            // Move host to original position (before position changed)
            host.RenderTransform = TransformOperations.Parse(GetTranslationExpression(-moveVector));
            // Retrieve transition defined in attached property
            MoveTransition moveTransition = host.GetValue(TransitionProperty)!;
            // If transitions are null create new container
            host.Transitions ??= [];
            // Add MoveTransition defined in attached property
            host.Transitions.Add(moveTransition);
            // Move element from original position to current one but now with transition
            host.RenderTransform = TransformOperations.Parse(GetTranslationExpression(Vector.Zero));
            // Wait until animation finishes
            await Task.Delay(moveTransition.Duration + moveTransition.Delay);
            // Wait some more, as sometime above sometime is not enough
            await Task.Yield();
            // Remove MoveTransition
            host.Transitions.Remove(moveTransition);
            // Subscribe for position changed
            host.PropertyChanged += HostLayoutablePropertyChanged;
        }

        private static string GetTranslationExpression(Vector vector)
        {
            string xStr = ((int)vector.X).ToString();
            string yStr = ((int)vector.Y).ToString();

            return $"translate({xStr}px, {yStr}px)";
        }

        public static void SetTransition(AvaloniaObject element, MoveTransition? transition)
            => element.SetValue(TransitionProperty, transition);

        public static MoveTransition? GetCommand(AvaloniaObject element)
            => element.GetValue(TransitionProperty);
    }
}
