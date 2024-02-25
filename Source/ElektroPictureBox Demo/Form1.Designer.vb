<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1 : Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TrackBarZoom = New System.Windows.Forms.TrackBar()
        Me.LabelZoom = New System.Windows.Forms.Label()
        Me.LabelZoomValue = New System.Windows.Forms.Label()
        Me.ButtonLoadImage = New System.Windows.Forms.Button()
        Me.LabelImageSizeValue = New System.Windows.Forms.Label()
        Me.LabelSelectionValue = New System.Windows.Forms.Label()
        Me.LabelSelection = New System.Windows.Forms.Label()
        Me.LabelImageSize = New System.Windows.Forms.Label()
        Me.ElektroPictureBox1 = New ElektroKit.UserControls.ElektroPictureBox()
        CType(Me.TrackBarZoom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ElektroPictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TrackBarZoom
        '
        Me.TrackBarZoom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TrackBarZoom.Location = New System.Drawing.Point(9, 47)
        Me.TrackBarZoom.Maximum = 200
        Me.TrackBarZoom.Minimum = 1
        Me.TrackBarZoom.Name = "TrackBarZoom"
        Me.TrackBarZoom.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.TrackBarZoom.Size = New System.Drawing.Size(42, 630)
        Me.TrackBarZoom.TabIndex = 1
        Me.TrackBarZoom.TickFrequency = 10
        Me.TrackBarZoom.TickStyle = System.Windows.Forms.TickStyle.Both
        Me.TrackBarZoom.Value = 100
        '
        'LabelZoom
        '
        Me.LabelZoom.AutoSize = True
        Me.LabelZoom.Location = New System.Drawing.Point(12, 9)
        Me.LabelZoom.Name = "LabelZoom"
        Me.LabelZoom.Size = New System.Drawing.Size(34, 13)
        Me.LabelZoom.TabIndex = 2
        Me.LabelZoom.Text = "Zoom"
        '
        'LabelZoomValue
        '
        Me.LabelZoomValue.AutoSize = True
        Me.LabelZoomValue.Location = New System.Drawing.Point(12, 31)
        Me.LabelZoomValue.Name = "LabelZoomValue"
        Me.LabelZoomValue.Size = New System.Drawing.Size(33, 13)
        Me.LabelZoomValue.TabIndex = 3
        Me.LabelZoomValue.Text = "100%"
        '
        'ButtonLoadImage
        '
        Me.ButtonLoadImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonLoadImage.Location = New System.Drawing.Point(1159, 12)
        Me.ButtonLoadImage.Name = "ButtonLoadImage"
        Me.ButtonLoadImage.Size = New System.Drawing.Size(101, 29)
        Me.ButtonLoadImage.TabIndex = 5
        Me.ButtonLoadImage.Text = "Load image..."
        Me.ButtonLoadImage.UseVisualStyleBackColor = True
        '
        'LabelImageSizeValue
        '
        Me.LabelImageSizeValue.AutoSize = True
        Me.LabelImageSizeValue.Location = New System.Drawing.Point(51, 28)
        Me.LabelImageSizeValue.Name = "LabelImageSizeValue"
        Me.LabelImageSizeValue.Size = New System.Drawing.Size(104, 13)
        Me.LabelImageSizeValue.TabIndex = 8
        Me.LabelImageSizeValue.Text = "{Width=0, Height=0}"
        '
        'LabelSelectionValue
        '
        Me.LabelSelectionValue.AutoSize = True
        Me.LabelSelectionValue.Location = New System.Drawing.Point(184, 28)
        Me.LabelSelectionValue.Name = "LabelSelectionValue"
        Me.LabelSelectionValue.Size = New System.Drawing.Size(104, 13)
        Me.LabelSelectionValue.TabIndex = 9
        Me.LabelSelectionValue.Text = "{Width=0, Height=0}"
        '
        'LabelSelection
        '
        Me.LabelSelection.AutoSize = True
        Me.LabelSelection.Location = New System.Drawing.Point(184, 9)
        Me.LabelSelection.Name = "LabelSelection"
        Me.LabelSelection.Size = New System.Drawing.Size(54, 13)
        Me.LabelSelection.TabIndex = 10
        Me.LabelSelection.Text = "Selection:"
        '
        'LabelImageSize
        '
        Me.LabelImageSize.AutoSize = True
        Me.LabelImageSize.Location = New System.Drawing.Point(51, 9)
        Me.LabelImageSize.Name = "LabelImageSize"
        Me.LabelImageSize.Size = New System.Drawing.Size(62, 13)
        Me.LabelImageSize.TabIndex = 11
        Me.LabelImageSize.Text = "Image Size:"
        '
        'ElektroPictureBox1
        '
        Me.ElektroPictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ElektroPictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ElektroPictureBox1.Image = Nothing
        Me.ElektroPictureBox1.Location = New System.Drawing.Point(52, 47)
        Me.ElektroPictureBox1.Name = "ElektroPictureBox1"
        Me.ElektroPictureBox1.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.ElektroPictureBox1.SelectionEnabled = True
        Me.ElektroPictureBox1.Size = New System.Drawing.Size(1208, 630)
        Me.ElektroPictureBox1.TabIndex = 6
        Me.ElektroPictureBox1.TabStop = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(1272, 689)
        Me.Controls.Add(Me.LabelImageSize)
        Me.Controls.Add(Me.LabelSelection)
        Me.Controls.Add(Me.LabelSelectionValue)
        Me.Controls.Add(Me.LabelImageSizeValue)
        Me.Controls.Add(Me.ElektroPictureBox1)
        Me.Controls.Add(Me.ButtonLoadImage)
        Me.Controls.Add(Me.LabelZoomValue)
        Me.Controls.Add(Me.LabelZoom)
        Me.Controls.Add(Me.TrackBarZoom)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.TrackBarZoom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ElektroPictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TrackBarZoom As TrackBar
    Friend WithEvents LabelZoom As Label
    Friend WithEvents LabelZoomValue As Label
    Friend WithEvents ButtonLoadImage As Button
    Friend WithEvents ElektroPictureBox1 As ElektroKit.UserControls.ElektroPictureBox
    Friend WithEvents LabelImageSizeValue As Label
    Friend WithEvents LabelSelectionValue As Label
    Friend WithEvents LabelSelection As Label
    Friend WithEvents LabelImageSize As Label
End Class
