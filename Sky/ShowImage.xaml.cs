using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Sky
{
    /// <summary>
    /// Показ изображения в отдельном окне.
    /// </summary>
    public partial class ShowImage : Window
    {
        public bool ZoomModeEnabled { get; set; }
        public ShowImage(Image image)
        {
            InitializeComponent();
            Image.Source = image.Source;
        }
        /// <summary>
        /// Приближение и отдаление изображения
        /// </summary>
        private void Grid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(ZoomModeEnabled)
            {
                ScrollView.IsEnabled = false;
                var element = Image;
                var position = e.GetPosition(element);
                var transform = element.RenderTransform as MatrixTransform;
                var matrix = transform.Matrix;
                var scale = e.Delta >= 0 ? 1.1 : (1.0 / 1.1);

                matrix.ScaleAtPrepend(scale, scale, position.X, position.Y);
                element.RenderTransform = new MatrixTransform(matrix);
            }
            else
                ScrollView.IsEnabled = true;
        }
    }
}