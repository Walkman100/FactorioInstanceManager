Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Windows.Forms
Imports System.Xml

Public Class Settings
    Private Shared _settingsPath As String
    Private Shared _loaded As Boolean = False

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

        If File.Exists(_settingsPath) Then
            LoadSettings()
        End If
        _loaded = True
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

    ' make sure _defaultInstancePath always has at least a space, to prevent XML saving/loading issues
    Private Shared _defaultInstancePath As String = " "
    Public Shared Property DefaultInstancePath As String
        Get
            Return _defaultInstancePath.Trim()
        End Get
        Set(value As String)
            If String.IsNullOrWhiteSpace(value) Then
                value = " "
            End If
            _defaultInstancePath = value
        End Set
    End Property

    Public Shared Property Installs As New List(Of Install)
    Public Shared Property Instances As New List(Of Instance)

    Private Shared Sub LoadSettings()
        Using reader As XmlReader = XmlReader.Create(_settingsPath)
            Try
                reader.Read()
            Catch ex As XmlException
                Return
            End Try

            If reader.IsStartElement() AndAlso reader.Name = "FactorioInstanceManager" Then
                If reader.Read() AndAlso reader.IsStartElement() AndAlso reader.Name = "Settings" AndAlso reader.Read() Then
                    While reader.IsStartElement()
                        Select Case reader.Name
                            Case "WindowMaximised"
                                reader.Read()
                                Boolean.TryParse(reader.Value, WindowMaximised)
                            Case "WindowWidth"
                                reader.Read()
                                Integer.TryParse(reader.Value, WindowWidth)
                            Case "WindowHeight"
                                reader.Read()
                                Integer.TryParse(reader.Value, WindowHeight)
                            Case "DefaultInstancePath"
                                reader.Read()
                                DefaultInstancePath = reader.Value
                        End Select
                        reader.Read() : reader.Read()
                    End While
                End If

                If reader.Read() AndAlso reader.IsStartElement() AndAlso reader.Name = "Installs" Then
                    Installs.Clear()
                    While reader.IsStartElement()
                        If reader.Read() AndAlso reader.IsStartElement() AndAlso reader.Name = "Install" Then
                            Dim install As New Install
                            install.Path = reader("path")
                            Version.TryParse(reader("version"), install.Version)
                            Installs.Add(install)
                        End If
                    End While
                End If

                If reader.Read() AndAlso reader.IsStartElement() AndAlso reader.Name = "Instances" Then
                    Instances.Clear()
                    While reader.IsStartElement()
                        If reader.Read() AndAlso reader.IsStartElement() AndAlso reader.Name = "Instance" Then
                            Dim instance As New Instance
                            instance.Path = reader("path")
                            Version.TryParse(reader("version"), instance.Version)
                            instance.IconPath = reader("iconpath")
                            Instances.Add(instance)
                        End If
                    End While
                End If
            End If
        End Using
    End Sub

    Friend Shared Sub SaveSettings()
        If Not _loaded Then Return
        Using writer As XmlWriter = XmlWriter.Create(_settingsPath, New XmlWriterSettings With {.Indent = True})
            writer.WriteStartDocument()
            writer.WriteStartElement("FactorioInstanceManager")

            writer.WriteStartElement("Settings")
            writer.WriteElementString("WindowMaximised", WindowMaximised.ToString())
            writer.WriteElementString("WindowWidth", WindowWidth.ToString())
            writer.WriteElementString("WindowHeight", WindowHeight.ToString())
            writer.WriteElementString("DefaultInstancePath", _defaultInstancePath)
            writer.WriteEndElement() ' Settings

            writer.WriteStartElement("Installs")
            For Each install As Install In Installs
                writer.WriteStartElement("Install")
                writer.WriteAttributeString("path", install.Path)
                writer.WriteAttributeString("version", install.Version?.ToString())
                writer.WriteEndElement()
            Next
            writer.WriteEndElement() ' Installs

            writer.WriteStartElement("Instances")
            For Each instance As Instance In Instances
                writer.WriteStartElement("Instance")
                writer.WriteAttributeString("path", instance.Path)
                writer.WriteAttributeString("version", instance.Version?.ToString())
                writer.WriteAttributeString("iconpath", instance.IconPath)
                writer.WriteEndElement()
            Next
            writer.WriteEndElement() ' Instances

            writer.WriteEndElement()
            writer.WriteEndDocument()
        End Using
    End Sub
End Class
