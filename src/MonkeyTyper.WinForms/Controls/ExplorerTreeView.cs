using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>
    /// Explorer-styled TreeView.
    /// </summary>
    public class ExplorerTreeView : TreeView
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string? pszSubIdList);

        /// <inheritdoc/>
        protected override void OnParentVisibleChanged(EventArgs e)
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetWindowTheme(Handle, "explorer", null);

            base.OnParentVisibleChanged(e);
        }
    }
}
