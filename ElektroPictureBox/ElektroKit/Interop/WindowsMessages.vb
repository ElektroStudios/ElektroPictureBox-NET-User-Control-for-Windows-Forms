Namespace ElektroKit.Interop.Win32.Enums

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' The system sends or posts a system-defined message when it communicates with an application. 
    ''' <para></para>
    ''' It uses these messages to control the operations of applications and to provide input and other information for applications to process. 
    ''' <para></para>
    ''' An application can also send or post system-defined messages.
    ''' <para></para>
    ''' Applications generally use these messages to control the operation of control windows created by using preregistered window classes.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/ms644927%28v=vs.85%29.aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    Public Enum WindowMessages As Integer

        ' *****************************************************************************
        '                            WARNING!, NEED TO KNOW...
        '
        '  THIS ENUMERATION IS PARTIALLY DEFINED TO MEET THE PURPOSES OF THIS API
        ' *****************************************************************************

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The <see cref="WindowMessages.Null"/> message performs no operation.
        ''' <para></para>
        ''' An application sends the <see cref="WindowMessages.Null"/> message if it wants to 
        ''' send a message that the recipient window will ignore.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Null = &H0

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The message is sent to the focus window when the mouse wheel is rotated.
        ''' <para></para>
        ''' The <c>DefWindowProc</c> function propagates the message to the window's parent. 
        ''' <para></para>
        ''' There should be no internal forwarding of the message since <c>DefWindowProc</c> propagates it up the 
        ''' parent chain until it finds a window that processes it.
        ''' <para></para>
        ''' 
        ''' <c>wParam</c> 
        ''' The high-order word indicates the distance the wheel is rotated, 
        ''' expressed in multiples or divisions of WHEEL_DELTA, which is 120. 
        ''' A positive value indicates that the wheel was rotated forward, away from the user; 
        ''' a negative value indicates that the wheel was rotated backward, toward the user.
        ''' <para></para>
        ''' The low-order word indicates whether various virtual keys are down.
        ''' <para></para>
        ''' 
        ''' <c>lParam</c> 
        ''' The low-order word specifies the x-coordinate of the pointer, relative to the upper-left corner of the screen.
        ''' <para></para>
        ''' The high-order word specifies the y-coordinate of the pointer, relative to the upper-left corner of the screen
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="https://docs.microsoft.com/en-us/windows/desktop/inputdev/wm-mousewheel"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        WM_MouseWheel = &H20A

    End Enum

End Namespace
