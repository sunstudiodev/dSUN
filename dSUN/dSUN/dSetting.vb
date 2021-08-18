Imports System.Collections.ObjectModel
Imports System.Management.Automation
Imports System.Management.Automation.Runspaces
Imports System.Runtime.InteropServices
Imports System.Text

Public Class setting
    Declare Function GetWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal uCmd As Integer) As IntPtr
    Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Int32

    Declare Function SendMessageHM Lib "user32.dll" Alias "SendMessageA" (
  ByVal hWnd As Int32,
  ByVal wMsg As Int32,
  ByVal wParam As Int32,
  ByVal lParam As String) As Int32
    Private Declare Function FindWindowEx Lib "user32.dll" Alias "FindWindowExA" (ByVal hwndParent As IntPtr, ByVal hwndChildAfter As IntPtr, ByVal lpszClass As String, ByVal lpszWindow As String) As IntPtr

    Const WM_SETTEXT As Long = &HC
    Const GW_CHILD As Long = 5
    Dim res_kq = ""
    Private Sub Init_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.first_open = True Then
            dSUN.Show()
            Me.Hide()
        Else

        End If
    End Sub
    Private Function RunScript(ByVal script As String) As String
        Dim runspace As Runspace = RunspaceFactory.CreateRunspace()
        runspace.Open()
        Dim pipeline As Pipeline = runspace.CreatePipeline()
        pipeline.Commands.AddScript(script)
        pipeline.Commands.Add("Out-String")

        Dim results As Collection(Of PSObject) = pipeline.Invoke()
        runspace.Close()
        Dim stringBuilder As StringBuilder = New StringBuilder()
        For Each ps As PSObject In results
            Console.WriteLine(ps)
            stringBuilder.AppendLine(ps.ToString())
        Next
        'Console.WriteLine(stringBuilder.ToString())
        Return stringBuilder.ToString()
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Dim myScript As String
        'myScript = "heroku login -i " & vbCrLf
        'myScript += "nhanntbsaf190011@fpt.edu.vn  " & vbCrLf
        'myScript += "TrungNhan_0911  " & vbCrLf
        'TextBox1.Text = RunScript(myScript)

        Dim pis As ProcessStartInfo = New ProcessStartInfo("notepad.exe")
        pis.UseShellExecute = True

        ' The process class is used to start the process
        ' it returns an object which can be used to control the started process
        'Dim cmd As Process = Process.Start(pis)

        ' SendMessage is used to send the clipboard message to notepad's
        ' main window.
        ' Dim textToAdd As String = "echo haha"
        ' SendMessageHM(cmd.MainWindowHandle, WM_SETTEXT, IntPtr.Zero, textToAdd)
        ' Dim hWnd1 As Int32 = FindWindow(vbNullString, "Untitled - Notepad")
        '  Dim hWndR2 As IntPtr = GetWindow(hWnd1, GW_CHILD)
        '  SendMessageHM(hWndR2, WM_SETTEXT, 0, "Hello World!")
        Dim Process() As Process = System.Diagnostics.Process.GetProcessesByName("powershell")
        For Each KeyProcess In Process
            Dim hWndR2 As IntPtr = GetWindow(KeyProcess.MainWindowHandle, GW_CHILD)
            SendMessageHM(hWndR2, &H111, 0, "get-process")
        Next
        'Dim hWindow As IntPtr = FindWindow(vbNullString, "Window Title")
        'Dim hButton As IntPtr = FindWindowEx(hWindow, vbNullString, vbNullString, "Button Text")
        'Dim result As Integer = SendMessageHM(hButton, &HF5, 0, 0)
        Timer1.Stop()

    End Sub

End Class