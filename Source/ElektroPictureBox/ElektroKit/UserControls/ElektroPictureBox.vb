#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Runtime.InteropServices
Imports System.Security.Permissions

Imports ElektroKit.Core
Imports ElektroKit.Interop.Win32
Imports ElektroKit.Interop.Win32.Enums

#End Region

#Region " ElektroPictureBox "

Namespace ElektroKit.UserControls

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' A extended <see cref="PictureBox"/> control.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <DisplayName(NameOf(ElektroPictureBox))>
    <Description("A extended PictureBox control.")>
    <DesignTimeVisible(True)>
    <DesignerCategory(NameOf(UserControl))>
    <ToolboxBitmap(GetType(PictureBox), "PictureBox.bmp")>
    <ToolboxItemFilter("System.Windows.Forms", ToolboxItemFilterType.Require)>
    <ClassInterface(ClassInterfaceType.AutoDispatch)>
    <ComVisible(True)>
    <DefaultBindingProperty(NameOf(ElektroPictureBox.Image))>
    <DefaultProperty(NameOf(ElektroPictureBox.Image))>
    <DefaultEvent(NameOf(ElektroPictureBox.Click))>
    <Docking(DockingBehavior.Ask)>
    <PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
    Public Class ElektroPictureBox : Inherits PictureBox

#Region " Private Fields "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The horizontal scroll bar.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private WithEvents HScrollBar As HScrollBar

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The vertical scroll bar.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private WithEvents VScrollBar As VScrollBar

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The view area size.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private viewSize As Size

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The start location of the selection rectangle.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private selectionStartPoint As Point

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Keeps track of the selection rectangle drawing.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private isDrawingSelectionRect As Boolean

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets a value that indicates the index in the TAB order that this control will occupy.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' A value that indicates the index in the TAB order that this control will occupy.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Localizable(True)>
        <Category("(Extended) Behavior")>
        <Description("The index in the TAB order that this control will occupy.")>
        <DefaultValue(GetType(Integer), "0")>
        Public Shadows Property TabIndex As Integer
            Get
                Return MyBase.TabIndex
            End Get
            Set(ByVal value As Integer)
                MyBase.TabIndex = value
            End Set
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets a value that indicates whether the user can give the focus to this control by using the TAB key.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' <see langword="True"/> if tabstop is enabled; <see langword="False"/> otherwise.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <PermissionSet>
        '''   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        '''   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        '''   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        '''   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ''' </PermissionSet>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Localizable(True)>
        <Category("(Extended) Behavior")>
        <Description("Indicates whether the user can use the TAB key to give focus to the control.")>
        <DefaultValue(GetType(Boolean), "False")>
        Public Shadows Property TabStop As Boolean
            Get
                Return MyBase.TabStop
            End Get
            Set(ByVal value As Boolean)
                MyBase.TabStop = value
            End Set
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the background color for the control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The background color for the control.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <PermissionSet>
        '''   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ''' </PermissionSet>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Localizable(True)>
        <Category("Appearance")>
        <Description("Indicates whether the user can use the TAB key to give focus to the control.")>
        <DefaultValue(GetType(Color), "Black")>
        Public Overrides Property BackColor As Color
            Get
                Return MyBase.BackColor
            End Get
            Set(ByVal value As Color)
                MyBase.BackColor = value
            End Set
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the default size of the control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The default size of the control.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overrides ReadOnly Property DefaultSize As Size
            Get
                Return New Size(96, 96)
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the image displayed in the control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The image displayed in the control.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(True)>
        <Bindable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Localizable(True)>
        <Category("Appearance")>
        <Description("The image displayed in the control.")>
        Public Overloads Property Image As Image
            Get
                Return Me.imageB
            End Get
            Set(ByVal value As Image)
                Me.imageB = value
                Me.VScrollBar.Value = Me.VScrollBar.Minimum
                Me.HScrollBar.Value = Me.HScrollBar.Minimum
                Me.DisplayScrollbars()
                Me.SetScrollbarValues()
                MyBase.Invalidate(invalidateChildren:=False)
                Me.OnImageChanged(EventArgs.Empty)
            End Set
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing Field )
        ''' <para></para>
        ''' The image displayed in the control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private imageB As Image

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the size of the image as it is shown in the control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The size of the image as it is shown in the control.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Localizable(True)>
        <Category("Layout")>
        <Description("The size of the image as it is shown in the control.")>
        <TypeConverter(GetType(SizeConverter))>
        Public ReadOnly Property ImageDisplaySize As Size
            Get
                If (Me.imageB IsNot Nothing) Then
                    Return New Size(CInt(Me.imageB.Size.Width * Me.zoomB), CInt(Me.imageB.Size.Height * Me.zoomB))
                End If
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the zoom level of the image shown by the control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The zoom level of the image shown by the control.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Localizable(True)>
        <Category("Appearance")>
        <Description("The zoom level of the image shown by the control.")>
        <TypeConverter(GetType(PercentageTypeConverter))>
        <DefaultValue(GetType(Double), "1.0")>
        Public Property Zoom As Double
            Get
                Return Me.zoomB
            End Get
            Set(ByVal value As Double)
                If (value <= 0.001R) Then
                    value = 0.001R
                End If
                Me.zoomB = value
                Me.DisplayScrollbars()
                Me.SetScrollbarValues()
                MyBase.Invalidate(invalidateChildren:=False)
                Me.OnZoomChanged(EventArgs.Empty)
            End Set
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing Field )
        ''' <para></para>
        ''' The zoom level of the image shown by the control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private zoomB As Double

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the canvas size.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The canvas size.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(False)>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        <Localizable(False)>
        Protected Property CanvasSize() As Size
            Get
                Return Me.canvasSizeB
            End Get
            Set(ByVal value As Size)
                Me.canvasSizeB = value
                Me.DisplayScrollbars()
                Me.SetScrollbarValues()
                MyBase.Invalidate(invalidateChildren:=False)
            End Set
        End Property
        Private canvasSizeB As Size

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the interpolation mode that determine the image quality.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The interpolation mode that determine the image quality.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Localizable(True)>
        <Category("Appearance")>
        <Description("The interpolation mode that determine the image quality.")>
        <DefaultValue(InterpolationMode.HighQualityBicubic)>
        Public Property InterpolationMode As InterpolationMode

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets a value that determine whether the user can select a portion of the image using the mouse.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' A value that determine whether the user can select a portion of the image using the mouse.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Localizable(True)>
        <Category("Behavior")>
        <Description("Determine whether the user can select a portion of the image using the mouse.")>
        <DefaultValue(GetType(Boolean), "False")>
        Public Property SelectionEnabled As Boolean
            Get
                Return Me.selectionEnabledB
            End Get
            Set(value As Boolean)
                Me.selectionEnabledB = value
                If Not (value) Then
                    Me.selectionRectangleB = Rectangle.Empty
                    Me.InvokePaint(Me, New PaintEventArgs(Me.CreateGraphics, Rectangle.Empty))
                End If
            End Set
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing Field )
        ''' <para></para>
        ''' A value that determine whether the user can select a portion of the image using the mouse.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private selectionEnabledB As Boolean

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the color used to draw the selection rectangle.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The color used to draw the selection rectangle.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Localizable(True)>
        <Category("Appearance")>
        <Description("The color used to draw the selection rectangle.")>
        <DefaultValue(GetType(Color), "Color.FromArgb(128, 72, 145, 220)")>
        Public Property SelectionColor As Color

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the selection rectangle.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The selection rectangle.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(True)>
        <EditorBrowsable(EditorBrowsableState.Always)>
        <Localizable(True)>
        <Category("Behavior")>
        <Description("The selection rectangle.")>
        Public ReadOnly Property SelectionRectangle As Rectangle
            Get
                Return Me.selectionRectangleB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing Field )
        ''' <para></para>
        ''' The selection rectangle.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private selectionRectangleB As Rectangle

#End Region

#Region " Events "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Occurs when the user or code scrolls through the client area of the control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Action")>
        <Description("Occurs when the user or code scrolls through the client area of the control.")>
        Public Event Scroll As ScrollEventHandler

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Occurs when the value of the <see cref="ElektroPictureBox.Image"/> property changes.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Property Changed")>
        <Description("Occurs when the value of the ElektroPictureBox.Image property changes.")>
        Public Event ImageChanged As EventHandler

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Occurs when the value of the <see cref="ElektroPictureBox.Zoom"/> property changes.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Property Changed")>
        <Description("Occurs when the value of the ElektroPictureBox.Zoom property changes.")>
        Public Event ZoomChanged As EventHandler

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Occurs when the user clicks inside the selection rectangle.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Action")>
        <Description("Occurs when the user clicks inside the selection rectangle.")>
        Public Event SelectionClicked As MouseEventHandler

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="ElektroPictureBox"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub New()

            MyBase.SuspendLayout()

            MyBase.DoubleBuffered = True
            Me.SetStyle(ControlStyles.Selectable, True)
            Me.TabStop = True
            Me.BackColor = Color.Black
            Me.InterpolationMode = InterpolationMode.HighQualityBicubic
            Me.zoomB = 1.0R
            Me.canvasSizeB = Me.DefaultSize
            Me.selectionEnabledB = False
            Me.SelectionColor = Color.FromArgb(128, 72, 145, 220)
            Me.selectionRectangleB = Rectangle.Empty
            Me.selectionStartPoint = Point.Empty

            Me.HScrollBar = New HScrollBar With {.Visible = False, .TabStop = False}
            Me.VScrollBar = New VScrollBar With {.Visible = False, .TabStop = False}
            Me.Controls.AddRange({Me.HScrollBar, Me.VScrollBar})

            MyBase.ResumeLayout(performLayout:=False)

        End Sub

#End Region

#Region " Event Invocators "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.Scroll"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' A <see cref="ScrollEventArgs"/> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overridable Sub OnScroll(ByVal e As ScrollEventArgs)

            If (Me.ScrollEvent IsNot Nothing) Then
                RaiseEvent Scroll(Me, e)
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.ImageChanged"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' A <see cref="EventArgs"/> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overridable Sub OnImageChanged(ByVal e As EventArgs)

            If (Me.ImageChangedEvent IsNot Nothing) Then
                RaiseEvent ImageChanged(Me, e)
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.ZoomChanged"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' A <see cref="EventArgs"/> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overridable Sub OnZoomChanged(ByVal e As EventArgs)

            If (Me.ZoomChangedEvent IsNot Nothing) Then
                RaiseEvent ZoomChanged(Me, e)
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.SelectionClicked"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' A <see cref="MouseEventArgs"/> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overridable Sub OnSelectionClicked(ByVal e As MouseEventArgs)

            If (Me.SelectionClickedEvent IsNot Nothing) Then
                RaiseEvent SelectionClicked(Me, e)
            End If

        End Sub

#End Region

#Region " Event Invocators (Overriden) "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.LoadCompleted"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' A <see cref="AsyncCompletedEventArgs"/> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnLoadCompleted(ByVal e As AsyncCompletedEventArgs)

            Me.DisplayScrollbars()
            Me.SetScrollbarValues()
            MyBase.OnLoadCompleted(e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.MouseDown"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' A <see cref="MouseEventArgs"/> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)

            MyBase.Focus()

            If (Me.selectionEnabledB) AndAlso (e.Button = MouseButtons.Left) AndAlso Not (Me.selectionRectangleB.Contains(e.Location)) Then
                Me.selectionStartPoint = e.Location
                Me.isDrawingSelectionRect = True
                MyBase.Invalidate(invalidateChildren:=False)
            End If

            MyBase.OnMouseDown(e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.MouseMove"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' A <see cref="MouseEventArgs"/> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)

            If (Me.isDrawingSelectionRect) Then
                Dim tempEndPoint As Point = e.Location
                Me.selectionRectangleB.Location = New Point(Math.Min(Me.selectionStartPoint.X, tempEndPoint.X), Math.Min(Me.selectionStartPoint.Y, tempEndPoint.Y))
                Me.selectionRectangleB.Size = New Size(Math.Abs(Me.selectionStartPoint.X - tempEndPoint.X), Math.Abs(Me.selectionStartPoint.Y - tempEndPoint.Y))
                MyBase.Invalidate(invalidateChildren:=False)
            End If

            MyBase.OnMouseMove(e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.MouseUp"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' A <see cref="MouseEventArgs"/> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)

            If (Me.selectionEnabledB) Then

                Select Case e.Button

                    Case MouseButtons.Left
                        If (Me.isDrawingSelectionRect) Then
                            Me.isDrawingSelectionRect = False

                        ElseIf (Me.selectionRectangleB.Contains(e.Location)) Then
                            Me.OnSelectionClicked(e)

                        End If

                    Case Else
                        If (Me.selectionRectangleB.Contains(e.Location)) Then
                            Me.OnSelectionClicked(e)
                        End If

                End Select

            End If

            MyBase.OnMouseUp(e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.Enter"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' An <see cref="EventArgs"/> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnEnter(ByVal e As EventArgs)

            MyBase.Invalidate(invalidateChildren:=False)
            MyBase.OnEnter(e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.Leave"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' An <see cref="EventArgs"/> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnLeave(ByVal e As EventArgs)

            MyBase.Invalidate(invalidateChildren:=False)
            MyBase.OnLeave(e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.KeyDown"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' A <see cref="KeyEventArgs" /> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)

            If (e.KeyCode = Keys.Enter) Then
                MyBase.InvokeOnClick(Me, e)
            End If

            MyBase.OnKeyDown(e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.Resize"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' A <see cref="EventArgs"/> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnResize(ByVal e As EventArgs)

            Me.DisplayScrollbars()
            Me.SetScrollbarValues()
            MyBase.Invalidate(invalidateChildren:=False)
            MyBase.OnResize(e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.MouseWheel"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' A <see cref="MouseEventArgs" /> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnMouseWheel(ByVal e As MouseEventArgs)

            If (Me.VScrollBar.Visible) Then
                Dim oldScrollValue As Integer = Me.VScrollBar.Value
                NativeMethods.SendMessage(Me.VScrollBar.Handle, WindowMessages.WM_MouseWheel, New IntPtr((e.Delta) << 16), e.Location)
                Me.OnScroll(New ScrollEventArgs(ScrollEventType.SmallIncrement, oldScrollValue, Me.VScrollBar.Value, ScrollOrientation.VerticalScroll))
            End If

            MyBase.OnMouseWheel(e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ElektroPictureBox.Paint"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' A <see cref="PaintEventArgs"/> that contains the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)

            MyBase.OnPaint(e)

            If (MyBase.Focused) Then
                Dim rc As Rectangle = MyBase.ClientRectangle
                rc.Inflate(-2, -2)
                ControlPaint.DrawFocusRectangle(e.Graphics, rc)
            End If

            ' Draw image.
            If (Me.imageB IsNot Nothing) Then

                Dim pt As New Point(CInt(Math.Truncate(Me.HScrollBar.Value / Me.zoomB)), CInt(Math.Truncate(Me.VScrollBar.Value / Me.zoomB)))

                Dim srcRect As Rectangle
                If ((Me.canvasSizeB.Width * Me.zoomB) < Me.viewSize.Width) AndAlso ((Me.canvasSizeB.Height * Me.zoomB) < Me.viewSize.Height) Then
                    srcRect = New Rectangle(0, 0, Me.canvasSizeB.Width, Me.canvasSizeB.Height) ' View entire image.

                Else
                    srcRect = New Rectangle(pt, New Size(CInt(Math.Truncate(Me.viewSize.Width / Me.zoomB)), CInt(Math.Truncate(Me.viewSize.Height / Me.zoomB)))) ' View a portion of image.

                End If

                Dim dstRect As Rectangle = New Rectangle((-srcRect.Width \ 2), (-srcRect.Height \ 2), srcRect.Width, srcRect.Height) ' The center of apparent image is on origin.

                Dim mx As New Matrix() ' Create an identity matrix.
                Dim scaleFactor As Single = CSng(Me.zoomB)
                mx.Scale(scaleFactor, scaleFactor) ' Zoom image.
                mx.Translate((Me.viewSize.Width / 2.0F), (Me.viewSize.Height / 2.0F), MatrixOrder.Append) ' Move image to view window center.

                With e.Graphics
                    ' Draw image.
                    .InterpolationMode = Me.InterpolationMode
                    .Transform = mx
                    .DrawImage(Me.imageB, dstRect, srcRect, GraphicsUnit.Pixel)

                    ' Draw selection rectangle.
                    If (Me.selectionEnabledB) AndAlso (Me.selectionRectangleB.Width > 0) AndAlso (Me.selectionRectangleB.Height > 0) Then
                        .ResetTransform()
                        Using br As New SolidBrush(Me.SelectionColor)
                            .FillRectangle(br, Me.selectionRectangleB)
                        End Using
                    End If
                End With

            End If

        End Sub

#End Region

#Region " Event Handlers "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ScrollBar.Scroll"/> event of the <see cref="ElektroPictureBox.HScrollBar"/> control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="ScrollEventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub HScrollBar_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs) Handles HScrollBar.Scroll

            MyBase.Invalidate(invalidateChildren:=False)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ScrollBar.Scroll"/> event of the <see cref="ElektroPictureBox.VScrollBar"/> control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="ScrollEventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub VScrollBar_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs) Handles VScrollBar.Scroll

            MyBase.Invalidate(invalidateChildren:=False)

        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Generates a <see cref="Control.Click"/> event for this control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub PerformClick()

            MyBase.InvokeOnClick(Me, EventArgs.Empty)

        End Sub

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Display the <see cref="ElektroPictureBox.HScrollBar"/> and <see cref="ElektroPictureBox.VScrollBar"/> controls.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepperBoundary>
        Protected Sub DisplayScrollbars()
            Me.viewSize.Width = Me.Width
            Me.viewSize.Height = Me.Height

            If (Me.imageB IsNot Nothing) Then
                Me.canvasSizeB = Me.imageB.Size
            End If

            ' If the zoomed image is wider than view window, show the HScrollBar and adjust the view window.
            If (Me.viewSize.Width > (Me.canvasSizeB.Width * Me.zoomB)) Then
                Me.HScrollBar.Visible = False
                Me.viewSize.Height = Me.Height

            Else
                Me.HScrollBar.Visible = True
                Me.viewSize.Height = Me.Height

            End If

            ' If the zoomed image is taller than view window, show the VScrollBar and adjust the view window.
            If (Me.viewSize.Height > (Me.canvasSizeB.Height * Me.zoomB)) Then
                Me.VScrollBar.Visible = False
                Me.viewSize.Width = Me.Width

            Else
                Me.VScrollBar.Visible = True
                Me.viewSize.Width = Me.Width

            End If

            ' Set up scrollbars.
            Me.HScrollBar.Location = New Point(0, Me.Height - Me.HScrollBar.Height)
            Me.HScrollBar.Width = (Me.viewSize.Width - Me.VScrollBar.Width)
            Me.VScrollBar.Location = New Point(Me.Width - Me.VScrollBar.Width, 0)
            Me.VScrollBar.Height = Me.viewSize.Height

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the <see cref="ElektroPictureBox.HScrollBar"/> and <see cref="ElektroPictureBox.VScrollBar"/> values.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepperBoundary>
        Protected Sub SetScrollbarValues()

            '-----------
            ' HScrollBar
            ' ----------
            If Me.HScrollBar.Visible Then
                Me.HScrollBar.Minimum = 0

                ' If the offset does not make the Maximum less than zero, set its value. 
                If ((Me.canvasSizeB.Width * Me.zoomB - Me.viewSize.Width) > 0) Then
                    Me.HScrollBar.Maximum = (CInt(Math.Truncate(Me.canvasSizeB.Width * Me.zoomB)) - Me.viewSize.Width)
                End If

                ' If the VScrollBar is visible, adjust the Maximum of the HSCrollBar to account for the width of the VScrollBar.  
                If (Me.VScrollBar.Visible) Then
                    Me.HScrollBar.Maximum += Me.VScrollBar.Width
                End If
                Me.HScrollBar.LargeChange = (Me.HScrollBar.Maximum \ 10)
                Me.HScrollBar.SmallChange = (Me.HScrollBar.Maximum \ 20)

                ' Adjust the HScrollBar.Maximum value to make the raw Maximum value attainable by user interaction.
                Me.HScrollBar.Maximum += Me.HScrollBar.LargeChange
            End If

            '-----------
            ' VScrollBar
            ' ----------
            If Me.VScrollBar.Visible Then
                Me.VScrollBar.Minimum = 0

                ' If the offset does not make the Maximum less than zero, set its value.    
                If ((Me.canvasSizeB.Height * Me.zoomB - Me.viewSize.Height) > 0) Then
                    Me.VScrollBar.Maximum = (CInt(Math.Truncate(Me.canvasSizeB.Height * Me.zoomB)) - Me.viewSize.Height)
                End If

                ' If the HScrollBar is visible, adjust the Maximum of the VSCrollBar to account for the width of the HScrollBar.
                If (Me.HScrollBar.Visible) Then
                    Me.VScrollBar.Maximum += Me.HScrollBar.Height
                End If
                Me.VScrollBar.LargeChange = (Me.VScrollBar.Maximum \ 10)
                Me.VScrollBar.SmallChange = (Me.VScrollBar.Maximum \ 20)

                ' Adjust the VScrollBar.Maximum value to make the raw Maximum value attainable by user interaction.
                Me.VScrollBar.Maximum += Me.VScrollBar.LargeChange
            End If

        End Sub

#End Region

#Region " Dispose Method "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Releases unmanaged and optionally managed resources.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="disposing">
        ''' true to release both managed and unmanaged resources; false to release only unmanaged resources.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)

            If (disposing) Then
                If (Me.HScrollBar IsNot Nothing) Then
                    RemoveHandler Me.HScrollBar.Scroll, AddressOf Me.HScrollBar_Scroll
                    Me.HScrollBar.Dispose()
                    Me.HScrollBar = Nothing
                End If

                If (Me.VScrollBar IsNot Nothing) Then
                    RemoveHandler Me.VScrollBar.Scroll, AddressOf Me.VScrollBar_Scroll
                    Me.VScrollBar.Dispose()
                    Me.VScrollBar = Nothing
                End If
            End If

            MyBase.Dispose(disposing)

        End Sub

#End Region

    End Class

End Namespace

#End Region
