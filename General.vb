Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Web.Script.Serialization

Namespace General
    Module Installs
        Function FindSteamInstall() As String

        End Function

        Function GetInstallVersion(installPath As String) As Version

        End Function

        Function GetInstallCurrentInstance(installPath As String) As String

        End Function

        Sub SetInstallCurrentInstance(installPath As String, instancePath As String)

        End Sub
    End Module

    Module Instances
        Function FindInstances(rootPath As String) As String()

        End Function

        Function GetInstanceVersion(instancePath As String) As Version
            'lastPlayedVersion in playerdata

        End Function

        Sub CreateInstance(instancePath As String)
            'create config folder only

        End Sub

        Sub DeleteInstance(instancePath As String)

        End Sub
    End Module
End Namespace
