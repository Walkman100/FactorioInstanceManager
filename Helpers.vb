Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Enum OS
    Windows
    Other
End Enum

Public Class Helpers
    Public Shared Sub WriteAllLines(path As String, lines As IEnumerable(Of String), separator As String)
        Using writer As New StreamWriter(path)
            For Each line In lines
                writer.Write(line)
                writer.Write(separator)
            Next
        End Using
    End Sub

    Friend Shared Function GetOS() As OS
        Return If(Environment.GetEnvironmentVariable("OS") = "Windows_NT", OS.Windows, OS.Other)
    End Function
End Class

Module Extensions
    <Extension()>
    Public Sub DoubleBuffered(control As Control, enable As Boolean) ' thanks to https://stackoverflow.com/a/15268338/2999220
        Dim doubleBufferPropertyInfo = control.[GetType]().GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        doubleBufferPropertyInfo.SetValue(control, enable, Nothing)
    End Sub
End Module
