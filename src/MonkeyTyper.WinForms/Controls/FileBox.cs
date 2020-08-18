using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MonkeyTyper.WinForms.Controls
{
    /// <summary>
    /// Provides a preview for the given file.
    /// </summary>
    public sealed partial class FileBox : UserControl
    {
        #region Var
        /// <inheritdoc/>
        public override bool AutoSize
        { 
            get => base.AutoSize;
            set 
            { 
                base.AutoSize = filenameLabel.AutoSize = value; 
                UpdateContainer(); 
            } 
        }

        /// <inheritdoc/>
        public override Font Font
        { 
            get => base.Font;
            set
            {
                base.Font = filenameLabel.Font = value;
                UpdateContainer();
            }
        }

        /// <summary>
        /// Icon location.
        /// </summary>
        [Category("Layout")]
        [Description("Icon location.")]
        public Point IconLocation
        {
            get => icon.Location;
            set
            {
                icon.Location = value;
                UpdateContainer();
            }
        }

        /// <summary>
        /// Icon anchor.
        /// </summary>
        [Category("Layout")]
        [Description("Icon anchor.")]
        public AnchorStyles IconAnchor
        {
            get => icon.Anchor; 
            set 
            { 
                icon.Anchor = value; 
                UpdateContainer(); 
            }
        }

        /// <summary>
        /// Icon size.
        /// </summary>
        [Category("Layout")]
        [Description("Icon size.")]
        public Size IconSize
        {
            get => icon.Size; 
            set 
            {
                icon.Size = value;
                UpdateContainer();
            }
        }

        /// <summary>
        /// Specifies whether to show only the filename (<see langword="true"/>)
        /// or its full path (<see langword="false"/>).
        /// </summary>
        [Category("Appearance")]
        [Description("Specifies whether to show only the filename or its full path.")]
        public bool ShowOnlyFilename
        {
            get => _showOnlyFilename;
            set
            {
                _showOnlyFilename = value;
                UpdateContainer();
            }
        }
        private bool _showOnlyFilename = true;

        /// <summary>
        /// A relative or absolute path to the file.
        /// </summary>
        [Category("Appearance")]
        [Description("A relative or absolute path to the file.")]
        public string Path
        {
            get => _path;
            set
            {
                _path = value ?? string.Empty;
                UpdateContainer();
            }
        }
        private string _path = string.Empty;
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="FileBox"/> class.
        /// </summary>
        public FileBox() => InitializeComponent();
        #endregion

        #region Functions
        private void UpdateContainer()
        {
            filenameLabel.Text = _showOnlyFilename ? System.IO.Path.GetFileName(_path) : _path;
            icon.BackgroundImage?.Dispose();
            Icon fileIcon;
            try
            {
                fileIcon = File.Exists(_path) ? Icon.ExtractAssociatedIcon(_path) : SystemIcons.Error;
            }
            catch
            {
                fileIcon = SystemIcons.Error;
            }
            icon.BackgroundImage = fileIcon.ToBitmap();
            fileIcon.Dispose();

            if (AutoSize)
            {
                Height = 3 + icon.Height + 3;
                Width = 3 + icon.Width + 3 + filenameLabel.Width + 3;
                icon.Left = 3;
                icon.Top = 3;
                filenameLabel.Top = (Height - filenameLabel.Height) / 2;
                filenameLabel.Left = icon.Right + 3;
            }
        }
        #endregion

        #region Handler
        private void Control_MouseClick(object sender, MouseEventArgs e) => OnMouseClick(e);
        #endregion
    }
}
