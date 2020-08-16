using MonkeyTyper.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MonkeyTyper.WinForms.Forms
{
    /// <summary>
    /// Gives a person the ability to change the settings
    /// provided by the <see cref="ISettingsProvider"/>.
    /// </summary>
    public partial class SettingsForm : Form
    {
        #region Var
        private ISettingsProvider SettingsProvider { get; }

        private Dictionary<TreeNode, DataGridView> SettingsViews { get; }
        #endregion

        #region Init
        /// <summary>
        /// Initialize a new instance of the <see cref="SettingsForm"/> class.
        /// </summary>
        /// <param name="settingsProvider">
        /// The <see cref="ISettingsProvider"/> instance.
        /// </param>
        public SettingsForm(ISettingsProvider settingsProvider)
        {
            SettingsProvider = settingsProvider ?? throw new ArgumentNullException(nameof(settingsProvider));
            SettingsViews = new Dictionary<TreeNode, DataGridView>();

            InitializeComponent();
        }

        private void InitializeOkButton(Button button)
        {
            const int width = 2;

            button.FlatAppearance.BorderSize = width;
            button.FlatAppearance.CheckedBackColor = button.BackColor;
            button.MouseDown += (s, e) => button.FlatAppearance.BorderSize = width / 2;
            button.MouseUp += (s, e) => sections.Focus();
            button.MouseEnter += (s, e) => button.FlatAppearance.BorderSize = width / 2;
            button.MouseLeave += (s, e) => button.FlatAppearance.BorderSize = width;
        }

        private void InitializeCancelButton(Button button)
        {
            const int width = 1;
            Color defaultColor = Color.FromArgb(173, 173, 173);
            Color hoverColor = Color.FromArgb(0, 120, 215);
            Color downColor = Color.FromArgb(0, 84, 153);

            button.FlatAppearance.BorderSize = width;
            button.FlatAppearance.BorderColor = defaultColor;
            button.FlatAppearance.CheckedBackColor = button.BackColor;
            button.MouseDown += (s, e) => { button.FlatAppearance.BorderSize = width; button.FlatAppearance.BorderColor = downColor; };
            button.MouseUp += (s, e) => sections.Focus();
            button.MouseEnter += (s, e) => { button.FlatAppearance.BorderSize = width; button.FlatAppearance.BorderColor = hoverColor; };
            button.MouseLeave += (s, e) => { button.FlatAppearance.BorderSize = width; button.FlatAppearance.BorderColor = defaultColor; };
        }

        private void InitializeSettings(TreeView sections, ISettingsProvider provider)
        {
            foreach (ISettings settings in provider.Settings)
            {
                TreeNode root = sections.Nodes.Add(settings.DisplayName);
                InitializeSettingsSection(root, settings, "General");
                if (root.Nodes.Count == 0)
                    sections.Nodes.Remove(root);
            }

            if (sections.Nodes.Count > 0)
                sections.SelectedNode = sections.Nodes[0];
        }

        private void InitializeSettingsSection(TreeNode rootNode, ISettings section, string generalName)
        {
            TreeNode node = rootNode.Nodes.Add(generalName);

            foreach (KeyValuePair<string, object?> entry in section.Where(x => x.Value is ISettings))
            {
                ISettings innerSettings = (ISettings)entry.Value!;
                TreeNode subNode = rootNode.Nodes.Add(section.GetDisplayName(entry.Key));
                InitializeSettingsSection(subNode, innerSettings, innerSettings.DisplayName);
                if (subNode.Nodes.Count == 0)
                    rootNode.Nodes.Remove(subNode);
            }

            DataGridView grid = CreateDefaultDataGridView();
            grid.Tag = section;
            foreach (KeyValuePair<string, object?> entry in section.Where(x => !(x.Value is ISettings)))
                grid.Rows.Add(new DataGridViewRow
                {
                    Cells =
                    {
                        new DataGridViewTextBoxCell { Value = entry.Key },
                        new DataGridViewTextBoxCell { Value = section.GetDisplayName(entry.Key) },
                        CreateValueCell(entry.Value)
                    }
                });

            if (grid.Rows.Count > 0)
            {
                Controls.Add(grid);
                SettingsViews.Add(node, grid);
                grid.CellEndEdit += Cell_Edited;
            }
            else if (node.Nodes.Count == 0)
            {
                rootNode.Nodes.Remove(node);
            }
        }
        #endregion

        #region Functions
        private DataGridView CreateDefaultDataGridView() => new DataGridView
        {
            Columns =
            {
                new DataGridViewColumn(new DataGridViewTextBoxCell()) { Visible = false, Name = "Name", ReadOnly = true },
                new DataGridViewColumn(new DataGridViewTextBoxCell()) { Name = "DisplayName", ReadOnly = true },
                new DataGridViewColumn(new DataGridViewTextBoxCell()) { Name = "Value" }
            },
            BackgroundColor = SystemColors.Control,
            Left = sections.Right + 6,
            Top = 12,
            Width = ClientSize.Width - sections.Width - sections.Left - 6 - 12,
            Height = sections.Height,
            RowHeadersVisible = false,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToOrderColumns = false,
            AllowUserToResizeColumns = false,
            AllowUserToResizeRows = false,
            AllowDrop = false,
            ColumnHeadersVisible = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            MultiSelect = false,
            ShowCellErrors = true,
            ShowEditingIcon = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
        };

        private static DataGridViewCell CreateValueCell(object? cell) => cell switch
        {
            bool _ => new DataGridViewCheckBoxCell { Value = cell, ValueType = typeof(bool) },
            Enum e => CreateEnumCell(e), 
            _ => new DataGridViewTextBoxCell { Value = cell, ValueType = typeof(string) }
        };

        private static DataGridViewCell CreateEnumCell(Enum value)
        {
            DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell
            { 
                Value = value,
                ValueType = value.GetType()
            };

            cell.Items.AddRange(Enum.GetValues(cell.ValueType).Cast<object>().ToArray());

            return cell;
        }
        #endregion

        #region Handlers
        protected override void OnShown(EventArgs e)
        {
            foreach (DataGridView grid in SettingsViews.Values)
            {
                Controls.Remove(grid);
                grid.Dispose();
            }
            SettingsViews.Clear();
            sections.Nodes.Clear();

            InitializeSettings(sections, SettingsProvider);
            InitializeOkButton(okButton);
            InitializeCancelButton(cancelButton);
            base.OnShown(e);
        }

        private void Cell_Edited(object sender, DataGridViewCellEventArgs e)
        {
            if (!(sender is DataGridView { Tag: ISettings settings } grid))
                return;

            DataGridViewCell cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string name = (string)grid.Rows[e.RowIndex].Cells["Name"].Value;
            try
            {
                settings[name] = cell.Value;
                cell.ErrorText = string.Empty;
            }
            catch (Exception ex)
            {
                while (ex.InnerException is { } inner && !string.IsNullOrEmpty(inner.Message))
                    ex = inner;

                MessageBox.Show(this, ex.Message, $"Value \"{cell.Value}\" is not valid for this field", MessageBoxButtons.OK, MessageBoxIcon.Error);

                cell.ErrorText = ex.Message;
                cell.Value = settings[name];
            }
        }

        private void Sections_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (SettingsViews.TryGetValue(e.Node, out DataGridView view) || e.Node.FirstNode is { } && SettingsViews.TryGetValue(e.Node.FirstNode, out view))
                view.BringToFront();
        }
        #endregion
    }
}
