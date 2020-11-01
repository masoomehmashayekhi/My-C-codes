using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TinyGeekGame
{
    static class Animations
    {
        public static Storyboard MoveDown( Rectangle MyImage,double to)
        {
            Storyboard story = new Storyboard();
           


            DoubleAnimation dbCanvasY = new DoubleAnimation();
            dbCanvasY.From = Canvas.GetTop(MyImage);
            dbCanvasY.To = to;
            dbCanvasY.Duration = new Duration(TimeSpan.FromSeconds(10));

            story.Children.Add(dbCanvasY);
            Storyboard.SetTargetName(dbCanvasY, MyImage.Name);
            Storyboard.SetTargetProperty(dbCanvasY, new PropertyPath(Canvas.TopProperty));
            return story;
        }

        public static Storyboard TowardBoxAndFadeOut(Rectangle MyImage,double toX,double toY)
        {
            Storyboard story = new Storyboard();
            var Fadeout = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };


            DoubleAnimation dbCanvasY = new DoubleAnimation();
            dbCanvasY.From = Canvas.GetTop(MyImage);
            dbCanvasY.To =toY;
            dbCanvasY.Duration = new Duration(TimeSpan.FromSeconds(1));
            DoubleAnimation dbCanvasX = new DoubleAnimation();
            dbCanvasX.From = Canvas.GetLeft(MyImage);
            dbCanvasX.To =toX;
            dbCanvasX.Duration = new Duration(TimeSpan.FromSeconds(1));
            story.Children.Add(dbCanvasY);
            story.Children.Add(dbCanvasX);
            story.Children.Add(Fadeout);
            Storyboard.SetTargetName(dbCanvasY, MyImage.Name);
            Storyboard.SetTargetProperty(dbCanvasY, new PropertyPath(Canvas.TopProperty));
            Storyboard.SetTargetName(dbCanvasX, MyImage.Name);
            Storyboard.SetTargetProperty(dbCanvasX, new PropertyPath(Canvas.LeftProperty));
            Storyboard.SetTargetName(Fadeout, MyImage.Name);
            Storyboard.SetTargetProperty(Fadeout, new PropertyPath(Rectangle.OpacityProperty));
            return story;
        }

        internal static void SetAnimationToNull(Rectangle MyImage)
        {
            MyImage.BeginAnimation(Rectangle.OpacityProperty, null);
            MyImage.BeginAnimation(Canvas.TopProperty, null);
            MyImage.BeginAnimation(Canvas.LeftProperty, null);
        }
    }
}
