using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NorthwindForms
{
    public partial class FormSplitContainer : Form
    {
        // SplitContainer Class
        // https://msdn.microsoft.com/en-us/library/system.windows.forms.splitcontainer(v=vs.110).aspx
        //
        // The following code example shows both a vertical and horizontal SplitContainer. 
        // The vertical splitter moves in 10-pixel increments. The left panel of the vertical SplitContainer 
        // contains a TreeView control, and its right panel contains a horizontal SplitContainer. 
        // Both panels of the horizontal SplitContainer are filled with ListView controls, and the top panel 
        // is defined as a FixedPanel so that it does not resize when you resize the container. 

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private TreeView treeView1;
        private ListView listView2;
        private ListView listView1;

        public FormSplitContainer()
        {
            InitializeComponent();
        }

        private void FormSplitContainer_Load(object sender, EventArgs e)
        {
            splitContainer1 = new SplitContainer();
            treeView1 = new TreeView();
            splitContainer2 = new SplitContainer();
            listView1 = new ListView();
            listView2 = new ListView();
            splitContainer1.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();

            // Basic SplitContainer properties.
            // This is a vertical splitter that moves in 10-pixel increments.
            // This splitter needs no explicit Orientation property because Vertical is the default.
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.ForeColor = System.Drawing.SystemColors.Control;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // You can drag the splitter no nearer than 30 pixels from the left edge of the container.
            splitContainer1.Panel1MinSize = 30;
            // You can drag the splitter no nearer than 20 pixels from the right edge of the container.
            splitContainer1.Panel2MinSize = 20;
            splitContainer1.Size = new System.Drawing.Size(292, 273);
            splitContainer1.SplitterDistance = 79;
            // This splitter moves in 10-pixel increments.
            splitContainer1.SplitterIncrement = 10;
            splitContainer1.SplitterWidth = 6;
            // splitContainer1 is the first control in the tab order.
            splitContainer1.TabIndex = 0;
            splitContainer1.Text = "splitContainer1";

            // When the splitter moves, the cursor changes shape.
            splitContainer1.SplitterMoved += new SplitterEventHandler(splitContainer1_SplitterMoved);
            splitContainer1.SplitterMoving += new SplitterCancelEventHandler(splitContainer1_SplitterMoving);

            // Add a TreeView control to the left panel.
            splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            // Add a TreeView control to Panel1.
            splitContainer1.Panel1.Controls.Add(treeView1);
            splitContainer1.Panel1.Name = "splitterPanel1";
            // Controls placed on Panel1 support right-to-left fonts.
            splitContainer1.Panel1.RightToLeft = RightToLeft.Yes;

            // Add a SplitContainer to the right panel.
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Panel2.Name = "splitterPanel2";

            // This TreeView control is in Panel1 of splitContainer1.
            treeView1.Dock = DockStyle.Fill;
            treeView1.ForeColor = System.Drawing.SystemColors.InfoText;
            treeView1.ImageIndex = -1;
            treeView1.Location = new System.Drawing.Point(0, 0);
            treeView1.Name = "treeView1";
            treeView1.SelectedImageIndex = -1;
            treeView1.Size = new System.Drawing.Size(79, 273);
            // treeView1 is the second control in the tab order.
            treeView1.TabIndex = 1;

            // Basic SplitContainer properties.
            // This is a horizontal splitter whose top and bottom panels are ListView controls. The top panel is fixed.
            splitContainer2.Dock = DockStyle.Fill;
            // The top panel remains the same size when the form is resized.
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer2.Location = new System.Drawing.Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // Create the horizontal splitter.
            splitContainer2.Orientation = Orientation.Horizontal;
            splitContainer2.Size = new System.Drawing.Size(207, 273);
            splitContainer2.SplitterDistance = 125;
            splitContainer2.SplitterWidth = 6;
            // splitContainer2 is the third control in the tab order.
            splitContainer2.TabIndex = 2;
            splitContainer2.Text = "splitContainer2";

            // This splitter panel contains the top ListView control.
            splitContainer2.Panel1.Controls.Add(listView1);
            splitContainer2.Panel1.Name = "splitterPanel3";

            // This splitter panel contains the bottom ListView control.
            splitContainer2.Panel2.Controls.Add(listView2);
            splitContainer2.Panel2.Name = "splitterPanel4";

            // This ListView control is in the top panel of splitContainer2.
            listView1.Dock = DockStyle.Fill;
            listView1.Location = new System.Drawing.Point(0, 0);
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(207, 125);
            // listView1 is the fourth control in the tab order.
            listView1.TabIndex = 3;

            // This ListView control is in the bottom panel of splitContainer2.
            listView2.Dock = DockStyle.Fill;
            listView2.Location = new System.Drawing.Point(0, 0);
            listView2.Name = "listView2";
            listView2.Size = new System.Drawing.Size(207, 142);
            // listView2 is the fifth control in the tab order.
            listView2.TabIndex = 4;

            // These are basic properties of the form.
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(splitContainer1);
            this.Name = "FormSplitContainer";
            this.Text = "SplitContainer Demo";
            splitContainer1.ResumeLayout(false);
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void splitContainer1_SplitterMoving(System.Object sender, SplitterCancelEventArgs e)
        {
            // As the splitter moves, change the cursor type.
            Cursor.Current = Cursors.NoMoveVert;
        }
        private void splitContainer1_SplitterMoved(System.Object sender, SplitterEventArgs e)
        {
            // When the splitter stops moving, change the cursor back to the default.
            Cursor.Current = Cursors.Default;
        }
    }
}
