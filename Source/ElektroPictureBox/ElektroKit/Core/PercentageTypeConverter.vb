#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.ComponentModel.Design.Serialization
Imports System.Globalization

#End Region

#Region " PercentageRangeTypeConverter "

Namespace ElektroKit.Core

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Provides a unified way of converting types of values to a percentage value.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' &lt;TypeConverter(GetType(PercentageTypeConverter))&gt;
    ''' &lt;Browsable(True)&gt;
    ''' Public Property ImageZoom As Double
    '''     Get
    '''         Return Me.imageZoomB
    '''     End Get
    '''     Set(ByVal value As Double)
    '''         If (value &lt; 0.0R) Then ' Avoid negative values.
    '''             value = 0.0R
    '''         End If
    '''         If (value &gt; 2.0R) Then ' Limit image zoom to x2.0 ( translated as string: 200% )
    '''             value = 2.0R
    '''         End If
    '''         Me.imageZoomB = value
    '''     End Set
    ''' End Property
    ''' 
    ''' Private imageZoomB As Double = 1.0R ' Default zoom: x1.0 ( translated as string: 100% )
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <seealso cref="TypeConverter"/>
    ''' ----------------------------------------------------------------------------------------------------
    Public Class PercentageTypeConverter : Inherits TypeConverter

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="PercentageTypeConverter"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Returns whether this converter can convert an object of the given type to the type of this converter, 
        ''' using the specified context.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="context">
        ''' An <see cref="ITypeDescriptorContext"/> that provides a format context.
        ''' </param>
        ''' 
        ''' <param name="sourceType">
        ''' A <see cref="Type"/> that represents the type you want to convert from.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if this converter can perform the conversion; otherwise, <see langword="False"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overloads Overrides Function CanConvertFrom(ByVal context As ITypeDescriptorContext, ByVal sourceType As Type) As Boolean
            If (sourceType Is GetType(String)) Then
                Return True
            End If

            Return MyBase.CanConvertFrom(context, sourceType)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Converts the given object to the type of this converter, using the specified context and culture information.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="context">
        ''' An <see cref="ITypeDescriptorContext"/> that provides a format context.
        ''' </param>
        ''' 
        ''' <param name="culture">
        ''' The <see cref="CultureInfo"/> to use as the current culture.
        ''' </param>
        ''' 
        ''' <param name="value">
        ''' The <see cref="Object"/> to convert.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' An <see cref="Object"/> that represents the converted value.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="FormatException">
        ''' Valid Range is between 0% and 100%.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overloads Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo,
                                                        ByVal value As Object) As Object

            If (TypeOf value IsNot String) Then
                Return MyBase.ConvertFrom(context, culture, value)
            End If

            Dim currentValueAsString As String = CStr(value).Replace("%"c, " "c).Trim()
            Dim parsedValue As Double = Double.Parse(currentValueAsString, CultureInfo.CurrentCulture)
            If (CStr(value).IndexOf("%") > 0) AndAlso (parsedValue >= 0) Then
                currentValueAsString = parsedValue.ToString(CultureInfo.CurrentCulture)
            End If

            Dim returnValue As Double = 0.0R
            Try
                returnValue = CDbl(TypeDescriptor.GetConverter(GetType(Double)).ConvertFrom(context, culture, currentValueAsString))
                If (returnValue > 1.0R) Then
                    returnValue = (returnValue / 100)
                End If

            Catch ex As FormatException
                Throw New FormatException("Invalid Format.", ex)

            End Try

            Return returnValue
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Returns whether this converter can convert the object to the specified type, using the specified context.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="context">
        ''' An <see cref="ITypeDescriptorContext"/> that provides a format context.
        ''' </param>
        ''' 
        ''' <param name="destinationType">
        ''' A <see cref="Type"/> that represents the type you want to convert to.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if this converter can perform the conversion; otherwise, <see langword="False"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overloads Overrides Function CanConvertTo(ByVal context As ITypeDescriptorContext, ByVal destinationType As Type) As Boolean

            If (destinationType = GetType(InstanceDescriptor)) Then
                Return True
            End If

            Return MyBase.CanConvertTo(context, destinationType)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Converts the given value object to the specified type, using the specified context and culture information.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="context">
        ''' An <see cref="ITypeDescriptorContext"/> that provides a format context.
        ''' </param>
        ''' 
        ''' <param name="culture">
        ''' A <see cref="CultureInfo"/>. If null is passed, the current culture is assumed.
        ''' </param>
        ''' 
        ''' <param name="value">
        ''' The <see cref="Object"/> to convert.
        ''' </param>
        ''' 
        ''' <param name="destinationType">
        ''' The <see cref="Type"/> to convert the <paramref name="value"/> parameter to.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' An <see cref="Object" /> that represents the converted value.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' destinationType
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo,
                                                      ByVal value As Object, ByVal destinationType As Type) As Object

            If (destinationType Is Nothing) Then
                Throw New ArgumentNullException("destinationType")
            End If

            If (destinationType Is GetType(String)) Then
                Dim num As Double = CDbl(value)
                Dim num2 As Integer = CInt((num * 100))
                '  MsgBox(num2.ToString(CultureInfo.CurrentCulture) & "%")
                Return (num2.ToString(CultureInfo.CurrentCulture) & "%")
            End If

            Return MyBase.ConvertTo(context, culture, value, destinationType)

        End Function

#End Region

    End Class

End Namespace

#End Region
