Imports System.Collections.Generic
Imports System.IO

Public Class Helpers
    Public Shared Sub WriteAllLines(path As String, lines As IEnumerable(Of String), separator As String)
        Using writer As New StreamWriter(path)
            For Each line In lines
                writer.Write(line)
                writer.Write(separator)
            Next
        End Using
    End Sub
End Class
