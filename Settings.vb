Imports System
Imports System.IO
Imports System.Windows.Forms

Public Class Settings
    Private Shared _settingsPath As String

    Public Shared Sub Init()
        Dim configFileName As String = "FactorioInstanceManager.xml"

        If Helpers.GetOS() = OS.Windows Then
            If Not       Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS")) Then
                Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS"))
            End If
            _settingsPath =               Path.Combine(Environment.GetEnvironmentVariable("AppData"), "WalkmanOSS", configFileName)
        Else
            If Not       Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS")) Then
                Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS"))
            End If
            _settingsPath =               Path.Combine(Environment.GetEnvironmentVariable("HOME"), ".config", "WalkmanOSS", configFileName)
        End If

        If      File.Exists(Path.Combine(Application.StartupPath, configFileName)) Then
            _settingsPath = Path.Combine(Application.StartupPath, configFileName)
        ElseIf               File.Exists(configFileName) Then
            _settingsPath = New FileInfo(configFileName).FullName
        End If
    End Sub

End Class
