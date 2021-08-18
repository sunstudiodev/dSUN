Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Management.Automation
Imports System.Management.Automation.Runspaces
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks

Module InterfaceFile
    Private result_shell As String = ""
    Private projectname As String
    Private netver As String
    Private output As TextBox
    Private appName As TextBox
    Private sln As String
    Private maindocker As String = ""
    Private btnDeploy As Button
    Private btnLog As Button
    Private pr As ProgressBar

    'Private check_hero As Boolean = False

    'Chuc nang chinh nam o day

    'Call dotnet de build thu muc puslish trong realease
    Public Async Sub buildPublish(slnn As String, path1 As String, path2 As String, projectname2 As String, netver2 As String, output2 As TextBox, appName2 As TextBox, btnDeploy2 As Button, pr2 As ProgressBar, btL As Button)
        'gan vao
        sln = slnn
        projectname = projectname2
        netver = netver2
        output = output2
        appName = appName2
        btnDeploy = btnDeploy2
        pr = pr2
        btnLog = btL

        output.Text += "Đang kiểm tra Heroku..." + vbNewLine
        Await Task.Run(Sub() RunScript("heroku auth:whoami"))

        If (result_shell.Contains("@")) Then
            output.Text += "Đã đăng nhập Heroku với email:" + result_shell + vbNewLine + vbNewLine

            pr.Value += 1
            Thread.Sleep(1000)
            releasePr(path1, path2)
        ElseIf (result_shell.Contains("not logged in")) Then
            ' output.Text += "Bạn chưa đăng nhập Heroku. Vui lòng đăng nhập và thử lại sao!" + vbNewLine
            output.Text += "Bạn chưa đăng nhập Heroku." + vbNewLine + "Đang mở CMD tiến hành đăng nhập..." + vbNewLine + "Vui lòng thực hiện đăng nhập và thử Deploy lại sao!" + vbNewLine
            Process.Start("cmd", "/k heroku login")
            failStop()
        Else
            'If (check_hero = False) Then

            '    check_hero = True
            '    failStop()
            'Else
            output.Text += "Đã xảy ra lỗi, không thể thực hiện việc kiểm tra Heroku. Vui lòng kiểm tra các bước sau:" + vbNewLine + vbNewLine + "1. Hãy đảm bảo bạn đã cài đặt Heroku CLI." + vbNewLine + "2. Hãy đảm bảo bạn đã đăng nhập vào tài khoản Heroku trên Heroku CLI. Bạn có thể thử đăng nhập lại vào Heroku bằng cách click vào 'Login Heroku' phía trên." + vbNewLine + vbNewLine + "Nếu lòng kiểm tra các bước trên và thử lại sao!" + vbNewLine
            btnLog.Visible = True
            failStop()
            ' End If

        End If
        downScroll()
    End Sub

    Public Async Sub releasePr(p1 As String, p2 As String)
        ' output.Clear()
        output.Text += "Đang Release dự án..." + vbNewLine
        Await Task.Run(Sub() RunScript("cd '" + sln + "'
                                                    dotnet publish -c Release"))
        If (result_shell.Length > 1) Then
            If (result_shell.Contains("up-to-date") And result_shell.Contains("publish")) Then
                output.Text += "Release dự án thành công!" + vbNewLine + "Đang thêm Docker vào dự án..." + vbNewLine + vbNewLine
                pr.Value += 1
                Thread.Sleep(1000)
                'Bay gio tao file docker den step tiep theo
                makeDockerFile(p1, p2)
            Else
                output.Text += "Release dự án thất bại. Vui lòng kiểm tra các bước sau và thử lại:" + vbNewLine + "1. Đảm bảo bạn đã cài đặt phiên bản .NET Framework cùng phiên bản Project." + vbNewLine + "2. Đảm bảo bạn đang chọn đúng dự án ASP.NET" + vbNewLine
                failStop()
            End If
            ' Console.WriteLine(result_shell)
        End If
        downScroll()
    End Sub

    Public Sub makeDockerFile(path1 As String, path2 As String)

        '   Console.WriteLine(path1)
        '  Console.WriteLine(path2)
        If My.Computer.FileSystem.DirectoryExists(path1) Then
            maindocker = path1
        End If
        If My.Computer.FileSystem.DirectoryExists(path2) Then
            maindocker = path2
        End If
        Console.WriteLine(path1 + vbNewLine + path2)
        'ghi vao file
        If (maindocker.Length > 0) Then
            Dim sb As StringBuilder = New StringBuilder()
            sb.AppendLine("FROM mcr.microsoft.com/dotnet/aspnet:" + netver + " AS base")
            sb.AppendLine("COPY . .")
            sb.AppendLine("")
            sb.AppendLine("CMD ASPNETCORE_URLS=http://*:$PORT dotnet " + projectname + ".dll")
            If (My.Computer.FileSystem.FileExists(maindocker + "\Dockerfile")) Then
                File.Delete(maindocker + "\Dockerfile")
            End If
            File.AppendAllText(maindocker + "\Dockerfile", sb.ToString())
            ' My.Computer.FileSystem.RenameFile(maindocker + "\Dockerfile.tmp", "Dockerfile")

            output.Text += "Đã thêm Docker vào dự án." + vbNewLine + "Đang chuẩn bị đăng nhập vào Container Registry..." + vbNewLine + vbNewLine
            Console.WriteLine(maindocker + "\Dockerfile")
            pr.Value += 1
            Thread.Sleep(1000)
            logContainer()
        Else
            output.Text += "Chưa thể thêm Docker vào dự án. Bạn vui lòng kiểm tra các bước sau và thử lại: " + vbNewLine + "1. Đảm bảo đường dẫn đến mục dự án hợp lệ." + vbNewLine + "2. Đảm bảo bạn đã cấp quyền cho thư mục được đọc và viết" + vbNewLine + "3. Đảm bảo máy bạn đã cài đặt Docker Desktop thành công!" + vbNewLine
            failStop()
        End If
        downScroll()
    End Sub

    Async Sub logContainer()
        ' result_shell = ""
        Await Task.Run(Sub() RunScript("cd '" + sln + "'
                                                   heroku container:login"))
        If (result_shell.Contains("error during connect")) Then ' is not recognized
            output.Text += "Docker Desktop chưa được kích hoạt! Vui lòng thử lại sao!" + vbNewLine
            output.Text += result_shell + vbNewLine
            failStop()
        ElseIf (result_shell.Contains("Login Succeeded")) Then
            output.Text += "Đăng nhập Container Registry thành công!" + vbNewLine + "Đang chuẩn bị tạo Image cho dự án để thêm vào Docker" + vbNewLine + vbNewLine
            pr.Value += 1
            Thread.Sleep(1000)
            dockerbuild()
        Else
            output.Text += "Không thể tiếp tục thực hiện. Vui lòng kiểm tra các bước sau và thử lại: " + vbNewLine + "1. Hãy đẩm bảo bạn đã cài đặt Heroku CLI và Docker Desktop." + vbNewLine + "2. Đảm bảo Docker Desktop đã được chạy và kích hoạt." + vbNewLine
            output.Text += result_shell + vbNewLine
            failStop()
        End If
        downScroll()
    End Sub

    Async Sub dockerbuild()
        result_shell = ""
        Await Task.Run(Sub() RunScript("cd '" + maindocker + "'
docker build -t '" + appName.Text + "' '" + maindocker + "'
"))
        '        Console.WriteLine("cd '" + maindocker + "'
        'docker build -t '" + appName.Text + "' '" + maindocker + "'
        '")
        If (result_shell.Contains("FINISHED")) Then ' is not recognized
            output.Text += "Hoàn tất tạo Image Docker cho dự án với tên '" + appName.Text + "'" + vbNewLine + "Đang chuẩn bị Run Container (Linux)" + vbNewLine + vbNewLine
            pr.Value += 1
            Thread.Sleep(1000)
            dockerRun()
        Else
            If (result_shell.Trim.Length = 0) Then
                Await Task.Run(Sub() RunScript("docker images '" + appName.Text + "'
"))
                If (result_shell.Contains(appName.Text)) Then
                    output.Text += "Hoàn tất tạo Image Docker cho dự án với tên '" + appName.Text + "'" + vbNewLine + "Đang chuẩn bị Run Container (Linux)" + vbNewLine + vbNewLine
                    pr.Value += 1
                    Thread.Sleep(1000)
                    dockerRun()
                Else
                    output.Text += "Không thể tiếp tục thực hiện. Không thể tạo Image Docker. Hãy kiểm tra các bước sau và thử lại: " + vbNewLine + "1. Có thể tên Image đã tồn tại, vui lòng mở Docker Desktop và xóa nó đi." + vbNewLine + "2. Có thể chứa các ký tự không hợp lệ trong đường dẫn hoặc tên app, dự án của bạn." + vbNewLine + "3. Đảm bảo bạn đã chạy và kích hoạt Docker Desktop (Linux) thành công!" + vbNewLine
                    ' output.Text += result_shell + vbNewLine
                    failStop()
                End If
            Else
                output.Text += "Không thể tiếp tục thực hiện. Không thể tạo Image Docker. Hãy kiểm tra các bước sau và thử lại: " + vbNewLine + "1. Có thể tên Image đã tồn tại, vui lòng mở Docker Desktop và xóa nó đi." + vbNewLine + "2. Có thể chứa các ký tự không hợp lệ trong đường dẫn hoặc tên app, dự án của bạn." + vbNewLine + "3. Đảm bảo bạn đã chạy và kích hoạt Docker Desktop (Linux) thành công!" + vbNewLine
                output.Text += result_shell + vbNewLine
                failStop()
            End If


        End If
        downScroll()
    End Sub
    Async Sub dockerRun()
        ' result_shell = ""
        Await Task.Run(Sub() RunScript("cd '" + maindocker + "'
                                                   docker run -d -p 80:80 '" + appName.Text + "'"))
        If ((result_shell.Trim.Length = 256) Or (result_shell.Trim.Length = 64)) Then
            output.Text += "Chạy Image '" + appName.Text + "' thành công trên Linux Docker Desktop" + vbNewLine + "Đang chuẩn bị đăng ký Container với Heroku..." + vbNewLine + vbNewLine
            pr.Value += 1
            Thread.Sleep(1000)
            setHeroku()
        ElseIf (result_shell.Contains("already allocated")) Then
            output.Text += "Image với tên '" + appName.Text + "' đang chạy, vui lòng mở Docker Desktop để dừng lại Image hoặc xóa bỏ nó." + vbNewLine
            output.Text += result_shell + vbNewLine
            failStop()
        Else
            output.Text += "Gặp sự cố khi chạy Image '" + appName.Text + "' trên Linux Docker Desktop. Vui lòng kiểm tra các bước sau và thử lại:" + vbNewLine + "1. Có thể Image cùng tên đang được chay, mở Docker Desktop để xóa Container đó." + vbNewLine + "2. Có thể Image không tồn tại hoặc bị lỗi. Vui lòng xóa Image cùng tên trong Docker Desktop và thử Deploy lại." + vbNewLine
            output.Text += result_shell + vbNewLine
            failStop()
        End If
        downScroll()
    End Sub

    Async Sub setHeroku()
        result_shell = ""
        Await Task.Run(Sub() RunScript("docker tag '" + appName.Text + "' registry.heroku.com/" + appName.Text.Trim + "/web
"))
        ' If (result_shell.Length <= 1) Then
        output.Text += "Đăng ký Image với Heroku thành công!" + vbNewLine + "Đang đẩy dự án của bạn lên Heroku (quá trình này có thể mất nhiều thời gian). Vui lòng đợi!" + vbNewLine + vbNewLine
        pr.Value += 1
        Thread.Sleep(1000)
        pushHeroku()
        'Else
        'output.Text += result_shell + vbNewLine
        '   End If
        downScroll()
    End Sub

    Async Sub pushHeroku()
        '  result_shell = ""
        Await Task.Run(Sub() RunScript("cd '" + maindocker + "'
                                                   heroku container:push web -a '" + appName.Text + "'"))
        If (result_shell.Contains("Couldn't find that app")) Then
            output.Text += "Tên ứng dụng bạn đã thiết lập không có sẵn trên tài khoản Heroku của bạn! Vui lòng thử lại sao!" + vbNewLine
            output.Text += result_shell + vbNewLine
            failStop()
        ElseIf (result_shell.Contains("Pushing web")) Then
            output.Text += "Push dự án web asp.net của bạn thành công!" + vbNewLine + "Đang tiến thành xuất bản dự án" + vbNewLine + vbNewLine
            pr.Value += 1
            Thread.Sleep(1000)
            publicWEB()
        Else
            output.Text += "Gặp sự cố không xác định khi push web của bạn lên Heroku! Vui lòng kiểm tra các bước sau và thử lại: " + vbNewLine + "1. Đảm bảo Image và Container đã được kích hoạt thành công." + vbNewLine + "2. Đảm bảo tên ứng dụng Heroku của bạn là đúng với tên đã tạo trên Heroku." + vbNewLine + "3. Đảm bảo dung lượng lưu trữ của dự án không vượt quá dung lượng được phép của tài khoản của bạn." + vbNewLine

            output.Text += result_shell + vbNewLine
            failStop()
        End If
        downScroll()
    End Sub

    Async Sub publicWEB()
        '    result_shell = ""
        Await Task.Run(Sub() RunScript("cd '" + maindocker + "'
                                                   heroku container:release web -a '" + appName.Text + "'
                                                   "))
        ' If (result_shell.Contains("Releasing images web") Or result_shell.Contains("done") Or result_shell.Contains("already running")) Then
        output.Text += "Đã xuất bản web của bạn thành công. Đang tiến hành mở trang web..." + vbNewLine + vbNewLine
        downScroll()
        pr.Value += 1
        Thread.Sleep(1000)
        Dim webAddress As String = "https://" + appName.Text + ".herokuapp.com/"
        Process.Start(webAddress)
        output.Text += "Đang tiến hành dừng các Container đang chạy..." + vbNewLine + vbNewLine
        downScroll()
        Thread.Sleep(1000)
        stopContainer()
        ' output.Text += "Tất cả đã xong!"


        ' Else
        ' output.Text += "Gap su co khong xac dinh khi xuat ban trang web cua ban!" + vbNewLine
        ' output.Text += result_shell + vbNewLine
        '   End If

    End Sub
    Async Sub stopContainer()
        Await Task.Run(Sub() RunScript("docker kill $(docker ps -q)
docker rm $(docker ps -a -q)
docker rmi $(docker images -q)
"))
        output.Text += "Dừng các Container và dọn dẹp các Image hoàn tất." + vbNewLine + "Tất cả đã xong!" + vbNewLine
        downScroll()
        failStop()
    End Sub
    Sub failStop()
        appName.Enabled = True
        btnDeploy.Enabled = True
        downScroll()
    End Sub
    Sub downScroll()
        'output.Refresh()
        output.SelectionStart = output.Text.Length
        output.ScrollToCaret()
    End Sub
    'tinh nang phu nam o duoi
    Public Function SplitString(ByVal mainString As String, ByVal BeginString As String, ByVal EndString As String) As String
        Dim i_s As Integer : i_s = InStr(mainString, BeginString)
        Dim i_e As Integer : i_e = InStr(mainString, EndString)
        On Error Resume Next
        If i_s <> -1 And i_e <> -1 Then
            Dim s As String

            s = Mid(mainString, i_s + Len(BeginString), i_e - i_s - Len(BeginString))
            SplitString = s

        Else
            SplitString = ""
        End If
    End Function
    Private Sub RunScript(ByVal script As String)
        Dim runspace As Runspace = RunspaceFactory.CreateRunspace()
        runspace.Open()
        Dim pipeline As Pipeline = runspace.CreatePipeline()
        pipeline.Commands.AddScript(script)
        pipeline.Commands.Add("out-String")
        Dim results As Collection(Of PSObject) = pipeline.Invoke()
        runspace.Close()
        Dim stringBuilder As StringBuilder = New StringBuilder()
        For Each ps As PSObject In results
            Console.WriteLine(ps)
            stringBuilder.AppendLine(ps.ToString())
        Next
        'Console.WriteLine(stringBuilder.ToString())
        result_shell = stringBuilder.ToString()
    End Sub
End Module
