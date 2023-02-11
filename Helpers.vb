Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Public Class Helpers
    Public Shared Sub WriteAllLines(path As String, lines As IEnumerable(Of String), separator As String)
        Using writer As New StreamWriter(path)
            For Each line In lines
                writer.Write(line)
                writer.Write(separator)
            Next
        End Using
    End Sub

    Public Shared Sub OpenFolder(folderPath As String)
        If Not Directory.Exists(folderPath) Then
            Throw New DirectoryNotFoundException($"Could not find directory ""{folderPath}""")
        End If

        Select Case WalkmanLib.GetOS()
            Case WalkmanLib.OS.Windows
                Diagnostics.Process.Start(folderPath)
            Case WalkmanLib.OS.Linux
                Diagnostics.Process.Start("xdg-open", $"""{folderPath}""")
            Case WalkmanLib.OS.MacOS
                Diagnostics.Process.Start("open", $"""{folderPath}""")
        End Select
    End Sub

    Public Shared Function ResizeImage(img As Image, newSize As Integer) As Image
        Dim rtnImg As New Bitmap(newSize, newSize)
        Using gr As Graphics = Graphics.FromImage(rtnImg)
            gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            gr.DrawImage(img, New Rectangle(0, 0, newSize, newSize), New Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel)
        End Using
        Return rtnImg
    End Function

    Public Shared Function GetInput(ByRef input As String, Optional windowTitle As String = "",
                                    Optional header As String = "", Optional content As String = Nothing) As DialogResult
        If OokiiDialogsLoaded() Then
            Return OokiiInputBox(input, windowTitle, header, content)
        Else
            Dim inputBoxPrompt As String = header
            If content IsNot Nothing Then
                inputBoxPrompt &= Environment.NewLine & content
            End If

            input = Microsoft.VisualBasic.InputBox(inputBoxPrompt, windowTitle, input)
            If String.IsNullOrEmpty(input) Then
                Return DialogResult.Cancel
            Else
                Return DialogResult.OK
            End If
        End If
    End Function
    Private Shared Function OokiiInputBox(ByRef input As String, Optional windowTitle As String = "",
                                          Optional header As String = "", Optional content As String = "") As DialogResult
        Dim ooInput As New Ookii.Dialogs.InputDialog With {
            .Input = input,
            .WindowTitle = windowTitle,
            .MainInstruction = header,
            .Content = content
        }

        Dim returnResult = ooInput.ShowDialog(FactorioInstanceManager)
        input = ooInput.Input
        Return returnResult
    End Function

    Public Shared Function SelectFolderDialog(ByRef selectedPath As String, Optional description As String = "",
                                              Optional showNewFolderButton As Boolean = True) As DialogResult
        If OokiiDialogsLoaded() Then
            Return OokiiFolderBrowserDialog(selectedPath, description, showNewFolderButton)
        Else
            Dim fbd As New FolderBrowserDialog() With {
                .SelectedPath = selectedPath,
                .Description = description,
                .ShowNewFolderButton = showNewFolderButton
            }
            Try
                Return fbd.ShowDialog()
            Finally
                selectedPath = fbd.SelectedPath
            End Try
        End If
    End Function
    Private Shared Function OokiiFolderBrowserDialog(ByRef selectedPath As String, Optional description As String = "",
                                                     Optional showNewFolderButton As Boolean = True) As DialogResult
        Dim fbd As New Ookii.Dialogs.VistaFolderBrowserDialog() With {
            .SelectedPath = selectedPath,
            .Description = description,
            .ShowNewFolderButton = showNewFolderButton,
            .UseDescriptionForTitle = True
        }
        Try
            Return fbd.ShowDialog()
        Finally
            selectedPath = fbd.SelectedPath
        End Try
    End Function

    Private Shared Function OokiiDialogsLoaded() As Boolean
        Try
            OokiiDialogsLoadedDelegate()
            Return True
        Catch ex As FileNotFoundException When ex.FileName.StartsWith("FactorioInstanceManager-Ookii.Dialogs")
            Return False
        Catch ex As Exception
            WalkmanLib.CustomMsgBox("Unexpected error loading FactorioInstanceManager-Ookii.Dialogs.dll!" & Environment.NewLine & Environment.NewLine & ex.Message,
                                    FactorioInstanceManager.Theme, "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End Try
    End Function
    Private Shared Sub OokiiDialogsLoadedDelegate() ' because calling a not found class will fail the caller of the method not directly in the method
        Dim test = Ookii.Dialogs.TaskDialogIcon.Information
    End Sub
End Class

Module Extensions
    <Extension()>
    Public Sub DoubleBuffered(control As Control, enable As Boolean) ' thanks to https://stackoverflow.com/a/15268338/2999220
        Dim doubleBufferPropertyInfo = control.[GetType]().GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        doubleBufferPropertyInfo.SetValue(control, enable, Nothing)
    End Sub
End Module
