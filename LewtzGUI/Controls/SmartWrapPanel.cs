using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LewtzGUI.Controls
{
    // Original code : Abe Heidebrecht
    public class SmartWrapPanel : WrapPanel
    {
        /// <summary>
        /// Identifies the DesiredHeight dependency property
        /// </summary>
        public static readonly DependencyProperty DesiredHeightProperty = DependencyProperty.Register(
          "DesiredHeight",
          typeof(double),
          typeof(SmartWrapPanel),
          new FrameworkPropertyMetadata(Double.NaN,
                  FrameworkPropertyMetadataOptions.AffectsArrange |
                  FrameworkPropertyMetadataOptions.AffectsMeasure |
                  FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                  FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Gets or sets the height to attempt to be.  If any child is taller than this, will use the child's height.
        /// </summary>
        public double DesiredHeight
        {
            get { return (double)GetValue(DesiredHeightProperty); }
            set { SetValue(DesiredHeightProperty, value); }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size ret = base.MeasureOverride(constraint);
            double h = ret.Height;

            if (!Double.IsNaN(DesiredHeight))
            {
                h = DesiredHeight;
                foreach (UIElement child in Children)
                {
                    if (child.DesiredSize.Height > h)
                        h = child.DesiredSize.Height;
                }
            }

            return base.MeasureOverride(new Size(ret.Width, h));
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double h = finalSize.Height;

            if (!Double.IsNaN(DesiredHeight))
            {
                h = DesiredHeight;
                foreach (UIElement child in Children)
                {
                    if (child.DesiredSize.Height > h)
                        h = child.DesiredSize.Height;
                }
            }

            return base.ArrangeOverride(new Size(finalSize.Width, h));
        }
    }
}
