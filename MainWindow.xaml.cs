using System.Windows;
using System.Windows.Media.Animation;

namespace SpinAnimation;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        StartAnimation();
    }

    private void StartAnimation()
    {
        // Create a storyboard
        var storyboard = new Storyboard();

        // 1. Spin animation
        var rotateAnimation = new DoubleAnimation(0, 360, TimeSpan.FromSeconds(2))
        {
            RepeatBehavior = new RepeatBehavior(1)
        };
        Storyboard.SetTarget(rotateAnimation, AnimatedImage);
        Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("RenderTransform.Children[1].Angle"));
        storyboard.Children.Add(rotateAnimation);

        // 2. Scale animation
        var scaleXAnimation = new DoubleAnimation(1, 1.5, TimeSpan.FromSeconds(1))
        {
            AutoReverse = true,
            BeginTime = TimeSpan.Zero
        };
        var scaleYAnimation = new DoubleAnimation(1, 1.5, TimeSpan.FromSeconds(1))
        {
            AutoReverse = true,
            BeginTime = TimeSpan.Zero
        };
        Storyboard.SetTarget(scaleXAnimation, AnimatedImage);
        Storyboard.SetTarget(scaleYAnimation, AnimatedImage);
        Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.Children[0].ScaleX"));
        Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.Children[0].ScaleY"));
        storyboard.Children.Add(scaleXAnimation);
        storyboard.Children.Add(scaleYAnimation);

        // 3. Fade-out animation
        var fadeAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1))
        {
            BeginTime = TimeSpan.FromSeconds(1),
            FillBehavior = FillBehavior.HoldEnd
        };
        Storyboard.SetTarget(fadeAnimation, AnimatedImage);
        Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("Opacity"));
        storyboard.Children.Add(fadeAnimation);

        storyboard.Completed += (s, e) => Application.Current.Shutdown();

        // Start the sound and animation
        BatmanSound.Play();
        storyboard.Begin(this);
    }
}