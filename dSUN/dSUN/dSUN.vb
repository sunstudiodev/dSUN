Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks

Public Class dSUN

    Private docker_path1 As String = ""
    Private docker_path2 As String = ""
    Private project_name As String = ""
    Private net_ver As String = ""

    Private Sub dSUN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtPathProject.ReadOnly = True
        txtNetVersion.ReadOnly = True
        txtOutput.ReadOnly = True
    End Sub

    Async Sub CheckUpdateAsync()
        ckUpdate.Stop()
        If My.Computer.Network.IsAvailable Then

            Using client As New HttpClient()
                Try
                    'Them dong nay de co the tuong tac voi https
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                    Dim getStringTask As Task(Of String) = client.GetStringAsync("https://raw.githubusercontent.com/sunstudiodev/dSUN/main/version.txt")

                    Dim version As String = Await getStringTask
                    If (My.Application.Info.Version.ToString.Equals(version)) Then

                    Else
                        Dim result As DialogResult = MessageBox.Show("dSUN has a new version, do you want to update?",
                          "Confirm update",
                          MessageBoxButtons.YesNo)

                        If (result = DialogResult.Yes) Then
                            'cau truc cap nhat /<version>/dSUN.exe
                            Process.Start("https://github.com/sunstudiodev/dSUN/releases/download/" + version + "/dSUN.exe")
                        End If
                    End If
                Catch

                End Try

            End Using
        End If
    End Sub

    Private Sub btnDeploy_Click(sender As Object, e As EventArgs) Handles btnDeploy.Click
        clickDeploy()

    End Sub

    Sub clickDeploy()
        txtOutput.Clear()
        tientrinh.Value = 0
        txtAppName.Enabled = False
        If (txtAppName.Text.Trim.Length > 2) Then
            buildPublish(txtPathProject.Text, docker_path1, docker_path2, project_name, net_ver, txtOutput, txtAppName, btnDeploy, tientrinh, btnLog)
            btnDeploy.Enabled = False
            txtAppName.Enabled = False
        Else
            MsgBox("Vui lòng nhập tên app Heroku của bạn. Đảm bảo tên ứng dụng là chính xác với tên app bạn đã tạo trên Heroku.")
            txtAppName.Enabled = True
        End If
    End Sub
    'Private Sub ExcuteDeploy()
    '    'txtOutput.Text += RunScript("cd '" + txtPathProject.Text + "'
    '    '                            dotnet publish -c Release")
    '    MsgBox("Deployment completed!")
    'End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Dim slcFolder As New FolderBrowserDialog
        If slcFolder.ShowDialog() = DialogResult.OK Then
            txtPathProject.Text = slcFolder.SelectedPath

            'Tim file du an sln
            Dim files() As String
            files = Directory.GetFiles(slcFolder.SelectedPath, "*.sln", SearchOption.TopDirectoryOnly)

            Dim filename As String

            If (files.Length > 0) Then

                filename = Path.GetFileName(files(0)).Replace(".sln", "")
                project_name = filename
                'Lay file name xong, gio lay phien ban netcore
                'Boi vi sln nam chung thu muc hoac ben ngoai nen se check duong dan tep .csp 2 lan

                Dim path1 As String = slcFolder.SelectedPath.ToString() + "\" + filename + "\" + filename + ".csproj"
                Dim path2 As String = slcFolder.SelectedPath.ToString() + "\" + filename + ".csproj"
                Dim pathCs As String = ""

                If My.Computer.FileSystem.FileExists(path1) Then
                    pathCs = path1
                ElseIf (My.Computer.FileSystem.FileExists(path2)) Then
                    pathCs = path2
                Else
                    MsgBox("Không tìm thấy tệp .csproj của dự án!")
                End If

                'Kiem  tra csproj ton tai moi tiep tuc

                If (pathCs.Trim.Length > 1) Then


                    'Doc file va lay phien ban

                    Dim value_netcore As String = File.ReadAllText(pathCs)

                    Dim nameNetcore As String = SplitString(value_netcore, "<TargetFramework>", "</TargetFramework>")
                    ' Dim outPut As String = SplitString(value_netcore, "<OutputType>", "</OutputType>")

                    'check co phai la asp.net ko
                    If (value_netcore.Contains("Microsoft.NET.Sdk.Web")) Then

                        'check co lay duoc phien ban ko

                        If (nameNetcore.Trim.Contains("netcore")) Then
                            txtNetVersion.Text = nameNetcore
                            net_ver = nameNetcore.Replace("netcoreapp", "").Trim

                            'neu kiem tra ok thi make path cua debug de tao file Docker

                            ' tao 2 path boi vi co 2 truong hop xay ra

                            docker_path1 = slcFolder.SelectedPath.ToString() + "\bin\Release\" + txtNetVersion.Text + "\publish"
                            docker_path2 = slcFolder.SelectedPath.ToString() + "\" + filename + "\bin\Release\" + txtNetVersion.Text + "\publish"
                            '   Console.WriteLine(docker_path1)
                            '   Console.WriteLine(docker_path2)
                            'thanh cong
                            'If (txtAppName.Text.Length > 0) Then
                            btnDeploy.Enabled = True
                            '   End If
                        Else
                            MsgBox("Không tìm thấy Net Core của Project này!" + vbNewLine + "Có thể phiên bản .NET Core bạn đang dùng không được hỗ trợ!" + vbNewLine + "Phiên bản .NET Core được hỗ trợ: .NET Core 3.1, .NET Core 2.1")
                        End If
                    Else
                        MsgBox("Project này không được hỗ trợ! Công cụ chỉ hỗ trợ các dự án ASP.NET được tạo ra từ Visual Studio.")
                    End If
                End If

            Else
                txtPathProject.Clear()
                MsgBox("Không tìm thấy tệp .sln của dự án!")
            End If
        End If
    End Sub

    Private Sub btnLog_Click(sender As Object, e As EventArgs) Handles btnLog.Click
        Process.Start("cmd", "/k heroku login")
        btnLog.Visible = False
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        AboutBox.Show()
    End Sub

    Private Sub ckUpdate_Tick(sender As Object, e As EventArgs) Handles ckUpdate.Tick
        CheckUpdateAsync()
        ckUpdate.Stop()
    End Sub

    Private Sub btnSetting_Click(sender As Object, e As EventArgs) Handles btnSetting.Click
        setting.Show()
    End Sub
End Class
