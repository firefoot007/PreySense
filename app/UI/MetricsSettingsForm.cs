using System;
using System.Drawing;
using System.Windows.Forms;
using PreySense.Overlay;
using PreySense.UI.Controls;

namespace PreySense.UI
{
    public class MetricsSettingsForm : RForm
    {
        private readonly PredatorDropDown[] _combos = new PredatorDropDown[8];
        private readonly RCheckBox[] _graphs = new RCheckBox[8];
        private static readonly string[] MetricOptions = { "None", "Usage", "Temperature", "Clock", "Power", "Memory", "Fan Speed", "Voltage", "Memory Speed" };

        public MetricsSettingsForm()
        {
            InitTheme(true);
            
            float scale = UiTheme.DialogScale(this, 1.0f);
            int width = (int)(340 * scale);
            int height = (int)(350 * scale); // Fits 8 rows and header checkbox perfectly

            ClientSize = new Size(width, height);
            UiTheme.ApplyFixedDialog(this, "Metrics Overlay Settings");
            ShowIcon = false;

            var builder = new UiBuilder(scale, width);
            var root = builder.RootStack(this, width);
            
            Font fontHeader = UiTheme.Font(scale, 10f, FontStyle.Bold);
            Font fontRegular = UiTheme.Font(scale, 9f);

            // Columns Card (pass empty title to construct custom 2-column header inside body)
            var colsCard = builder.StackCard(root, string.Empty, fontHeader, width - 24, out TableLayoutPanel colsBody);

            // 2-column header table: [Overlay Settings Title] [Show FPS Checkbox]
            var headerTable = builder.Table(width - 48,
                new ColumnStyle(SizeType.Percent, 100F),
                new ColumnStyle(SizeType.AutoSize));

            var titleLabel = builder.Text("Overlay Settings", fontHeader, UiTheme.TextPrimary);

            var chkShowFps = new RCheckBox
            {
                Text = "Show FPS",
                Font = fontRegular,
                ForeColor = foreMain,
                BackColor = buttonSecond,
                AutoSize = true,
                Margin = Padding.Empty,
                Padding = new Padding((int)(8 * scale), (int)(3 * scale), (int)(8 * scale), (int)(3 * scale)),
                UseVisualStyleBackColor = false,
                Checked = PreySense.Overlay.AppConfig.Get("overlay_show_fps", 1) == 1
            };
            chkShowFps.CheckedChanged += (s, e) => {
                PreySense.Overlay.AppConfig.Set("overlay_show_fps", chkShowFps.Checked ? 1 : 0);
                Program.GetHardwareOverlay().RefreshDisplayFlags();
            };

            builder.AddRow(headerTable, titleLabel, chkShowFps);
            headerTable.Margin = new Padding(0, 0, 0, (int)Math.Round(8f * scale));
            builder.AddRow(colsBody, headerTable);

            for (int i = 0; i < 8; i++)
            {
                int colIdx = i;
                string configKey = $"overlay_col_{colIdx + 1}";
                string graphKey = $"overlay_col_{colIdx + 1}_graph";
                
                // Get default setting matching screenshot
                string defaultVal = colIdx switch
                {
                    0 => "Usage",
                    1 => "Temperature",
                    2 => "Voltage",
                    3 => "Clock",
                    4 => "Power",
                    5 => "Memory",
                    _ => "None"
                };
                string currentVal = PreySense.Overlay.AppConfig.GetString(configKey, defaultVal);
                if (string.IsNullOrEmpty(currentVal)) currentVal = defaultVal;

                // Graph checkbox (White text, checked by default for column 5 / Power)
                int defaultChecked = (colIdx == 4) ? 1 : 0;
                var chkGraph = new RCheckBox
                {
                    Text = "Graph",
                    Font = fontRegular,
                    ForeColor = foreMain,
                    BackColor = buttonSecond,
                    AutoSize = true,
                    Margin = Padding.Empty,
                    Padding = new Padding((int)(8 * scale), (int)(4 * scale), (int)(8 * scale), (int)(4 * scale)),
                    UseVisualStyleBackColor = false,
                    Checked = PreySense.Overlay.AppConfig.Get(graphKey, defaultChecked) == 1
                };
                chkGraph.CheckedChanged += (s, e) => {
                    PreySense.Overlay.AppConfig.Set(graphKey, chkGraph.Checked ? 1 : 0);
                    Program.GetHardwareOverlay().RefreshDisplayFlags();
                };

                _graphs[colIdx] = chkGraph;

                var lbl = builder.Text($"Col {colIdx + 1}:", fontRegular, UiTheme.TextMuted);
                var combo = builder.Combo((int)(120 * scale), fontRegular, 28);
                combo.Items.AddRange(MetricOptions);
                int selIdx = Array.IndexOf(MetricOptions, currentVal);
                combo.SelectedIndex = selIdx >= 0 ? selIdx : 0;
                
                combo.SelectedIndexChanged += (s, e) => {
                    string selected = combo.SelectedItem?.ToString() ?? "None";
                    PreySense.Overlay.AppConfig.Set(configKey, selected);
                    Program.GetHardwareOverlay().RefreshDisplayFlags();
                };

                _combos[colIdx] = combo;

                // 3-column row table: [Label] [Dropdown] [Checkbox]
                var rowTable = builder.Table(width - 48, 
                    new ColumnStyle(SizeType.Absolute, 55F * scale), 
                    new ColumnStyle(SizeType.Percent, 100F),
                    new ColumnStyle(SizeType.Absolute, 75F * scale));
                
                builder.AddRow(rowTable, lbl, combo, chkGraph);
                builder.AddRow(colsBody, rowTable);
                rowTable.Margin = new Padding(0, 0, 0, (int)Math.Round(2.0f * scale));
            }
        }
    }
}
