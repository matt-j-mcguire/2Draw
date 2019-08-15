Imports System.IO
Imports System.Security.AccessControl
Imports System.Security.Principal

Public Class DirectoryIOAccess

    Public Enum Other
        Docs
        Pics
        Music
        Desktop
        UserAppData
        AllUserAppData
    End Enum

    Public Shared Function GetUsableDirectory(wanted As DirectoryInfo, ifnot As Other) As DirectoryInfo
        If Not IsWritable(wanted) Then
            Select Case ifnot
                Case Other.AllUserAppData
                    wanted = New DirectoryInfo(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData)
                Case Other.Desktop
                    wanted = New DirectoryInfo(My.Computer.FileSystem.SpecialDirectories.Desktop)
                Case Other.Docs
                    wanted = New DirectoryInfo(My.Computer.FileSystem.SpecialDirectories.MyDocuments)
                Case Other.Music
                    wanted = New DirectoryInfo(My.Computer.FileSystem.SpecialDirectories.MyMusic)
                Case Other.Pics
                    wanted = New DirectoryInfo(My.Computer.FileSystem.SpecialDirectories.MyPictures)
                Case Other.UserAppData
                    wanted = New DirectoryInfo(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData)
            End Select
        End If
        Return wanted
    End Function



    Public Shared Function IsReadable(di As DirectoryInfo) As Boolean
        Dim rules As AuthorizationRuleCollection
        Dim identity As WindowsIdentity
        Try

            rules = di.GetAccessControl().GetAccessRules(True, True, GetType(SecurityIdentifier))
            identity = WindowsIdentity.GetCurrent()

        Catch uae As UnauthorizedAccessException

            Debug.WriteLine(uae.ToString())
            Return False
        End Try


        Dim isAllow As Boolean = False
        Dim userSID As String = identity.User.Value

        For Each rule As FileSystemAccessRule In rules
            If rule.IdentityReference.ToString() = userSID Or identity.Groups.Contains(rule.IdentityReference) Then
                Dim fs As FileSystemRights = rule.FileSystemRights
                If ((fs And FileSystemRights.Read) AndAlso rule.AccessControlType = AccessControlType.Deny) Then
                    Return False
                ElseIf ((fs And FileSystemRights.Read) AndAlso rule.AccessControlType = AccessControlType.Allow) Then
                    isAllow = True
                End If
            End If
        Next
        Return isAllow
    End Function

    Public Shared Function IsWritable(di As DirectoryInfo) As Boolean
        Dim rules As AuthorizationRuleCollection
        Dim identity As WindowsIdentity
        Try

            rules = di.GetAccessControl().GetAccessRules(True, True, GetType(SecurityIdentifier))
            identity = WindowsIdentity.GetCurrent()

        Catch uae As UnauthorizedAccessException

            Debug.WriteLine(uae.ToString())
            Return False
        End Try


        Dim isAllow As Boolean = False
        Dim userSID As String = identity.User.Value

        For Each rule As FileSystemAccessRule In rules
            If rule.IdentityReference.ToString() = userSID Or identity.Groups.Contains(rule.IdentityReference) Then
                Dim fs As FileSystemRights = rule.FileSystemRights
                If ((fs And FileSystemRights.Write) AndAlso rule.AccessControlType = AccessControlType.Deny) Then
                    Return False
                ElseIf ((fs And FileSystemRights.Write) AndAlso rule.AccessControlType = AccessControlType.Allow) Then
                    isAllow = True
                End If
            End If
        Next
        Return isAllow
    End Function

End Class