#Region " Imports "

Imports ElektroKit.UserControls

#End Region

Public NotInheritable Class Form1 : Inherits Form

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub ButtonLoadImage_Click(sender As Object, e As EventArgs) Handles ButtonLoadImage.Click
        Using ofd As New OpenFileDialog
            ofd.Title = "Select a image file..."
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.png) | *.jpg; *.jpeg; *.bmp; *.png"
            ofd.FilterIndex = 0

            If (ofd.ShowDialog = DialogResult.OK) Then
                Me.ElektroPictureBox1.Image = Image.FromFile(ofd.FileName)
            End If
        End Using

        Me.LogMethodName()
    End Sub

    Private Sub TrackBarZoom_Scroll(sender As Object, e As EventArgs) Handles TrackBarZoom.Scroll
        Dim trb As TrackBar = DirectCast(sender, TrackBar)
        Me.LabelZoomValue.Text = String.Format("{0}%", trb.Value)
        Me.ElektroPictureBox1.Zoom = (trb.Value / 100)

        Me.LogMethodName()
    End Sub

    Private Sub ElektroPictureBox1_ImageChanged(sender As Object, e As EventArgs) Handles ElektroPictureBox1.ImageChanged
        Me.LabelImageSizeValue.Text = DirectCast(sender, ElektroPictureBox).ImageDisplaySize.ToString()

        ' Reset zoom level and selection rectangle for new images.
        Me.ElektroPictureBox1.Zoom = 1.0R
        Me.TrackBarZoom.Value = 100
        Me.ElektroPictureBox1.SelectionEnabled = False
        Me.ElektroPictureBox1.SelectionEnabled = True
        Me.LabelZoomValue.Text = String.Format("{0}%", Me.TrackBarZoom.Value)

        Me.LogMethodName()
    End Sub

    Private Sub ElektroPictureBox1_ZoomChanged(sender As Object, e As EventArgs) Handles ElektroPictureBox1.ZoomChanged
        Me.LabelImageSizeValue.Text = DirectCast(sender, ElektroPictureBox).ImageDisplaySize.ToString()
    End Sub

    Private Sub ElektroPictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles ElektroPictureBox1.MouseMove
        Me.LabelSelectionValue.Text = DirectCast(sender, ElektroPictureBox).SelectionRectangle.ToString()
    End Sub

    Private Sub ElektroPictureBox1_Scroll(sender As Object, e As ScrollEventArgs) Handles ElektroPictureBox1.Scroll
        Me.LogMethodName()
    End Sub

    Private Sub ElektroPictureBox1_Click(sender As Object, e As EventArgs) Handles ElektroPictureBox1.Click
        Me.LogMethodName()
    End Sub

    Private Sub ElektroPictureBox1_SelectionClicked(sender As Object, e As MouseEventArgs) Handles ElektroPictureBox1.SelectionClicked
        Me.LogMethodName()
    End Sub

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Writes a log message with the name of the method that triggered.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <param name="methodName">
    ''' The name of the method that triggered.
    ''' </param>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    Private Sub LogMethodName(<CallerMemberName> ByVal Optional methodName As String = "")
        Console.WriteLine(String.Format("[event triggered] -> {0}", methodName))
    End Sub

End Class
