Imports System
Imports System.Collections.Generic
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

        ' preload values in case they aren't set
        WindowWidth = FactorioInstanceManager.Width
        WindowHeight = FactorioInstanceManager.Height
    End Sub

    Public Structure Install
        Public Path As String
        Public Version As Version
    End Structure
    Public Structure Instance
        Public Path As String
        Public Version As Version
        Public IconPath As String
    End Structure

    Public Shared Property WindowMaximised As Boolean
    Public Shared Property WindowWidth As Integer
    Public Shared Property WindowHeight As Integer
    Public Shared Property DefaultInstancePath As String
    Public Shared Property Installs As New List(Of Install)
    Public Shared Property Instances As New List(Of Instance)
End Class
