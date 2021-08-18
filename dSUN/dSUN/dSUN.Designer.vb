<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dSUN
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dSUN))
        Me.btnDeploy = New System.Windows.Forms.Button()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.txtPathProject = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAppName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.txtNetVersion = New System.Windows.Forms.TextBox()
        Me.tientrinh = New System.Windows.Forms.ProgressBar()
        Me.btnLog = New System.Windows.Forms.Button()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.ckUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.btnSetting = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnDeploy
        '
        Me.btnDeploy.Enabled = False
        Me.btnDeploy.Location = New System.Drawing.Point(470, 126)
        Me.btnDeploy.Name = "btnDeploy"
        Me.btnDeploy.Size = New System.Drawing.Size(75, 36)
        Me.btnDeploy.TabIndex = 2
        Me.btnDeploy.Text = "Deploy"
        Me.btnDeploy.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpen.Location = New System.Drawing.Point(492, 24)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(53, 24)
        Me.btnOpen.TabIndex = 8
        Me.btnOpen.Text = "Open"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'txtPathProject
        '
        Me.txtPathProject.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPathProject.Location = New System.Drawing.Point(144, 24)
        Me.txtPathProject.Name = "txtPathProject"
        Me.txtPathProject.Size = New System.Drawing.Size(342, 22)
        Me.txtPathProject.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(25, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Net Core Version:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 16)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Project ASP.NET:"
        '
        'txtAppName
        '
        Me.txtAppName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAppName.Location = New System.Drawing.Point(144, 98)
        Me.txtAppName.Name = "txtAppName"
        Me.txtAppName.Size = New System.Drawing.Size(401, 22)
        Me.txtAppName.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(117, 16)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Heroku Appname:"
        '
        'txtOutput
        '
        Me.txtOutput.BackColor = System.Drawing.SystemColors.HotTrack
        Me.txtOutput.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutput.ForeColor = System.Drawing.Color.White
        Me.txtOutput.Location = New System.Drawing.Point(27, 174)
        Me.txtOutput.Multiline = True
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtOutput.Size = New System.Drawing.Size(518, 172)
        Me.txtOutput.TabIndex = 12
        '
        'txtNetVersion
        '
        Me.txtNetVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetVersion.Location = New System.Drawing.Point(144, 63)
        Me.txtNetVersion.Name = "txtNetVersion"
        Me.txtNetVersion.Size = New System.Drawing.Size(401, 22)
        Me.txtNetVersion.TabIndex = 13
        '
        'tientrinh
        '
        Me.tientrinh.Location = New System.Drawing.Point(27, 352)
        Me.tientrinh.Maximum = 9
        Me.tientrinh.Name = "tientrinh"
        Me.tientrinh.Size = New System.Drawing.Size(518, 19)
        Me.tientrinh.TabIndex = 14
        '
        'btnLog
        '
        Me.btnLog.Location = New System.Drawing.Point(158, 126)
        Me.btnLog.Name = "btnLog"
        Me.btnLog.Size = New System.Drawing.Size(98, 36)
        Me.btnLog.TabIndex = 15
        Me.btnLog.Text = "Login Heroku"
        Me.btnLog.UseVisualStyleBackColor = True
        Me.btnLog.Visible = False
        '
        'btnAbout
        '
        Me.btnAbout.Location = New System.Drawing.Point(366, 126)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(98, 36)
        Me.btnAbout.TabIndex = 16
        Me.btnAbout.Text = "&About"
        Me.btnAbout.UseVisualStyleBackColor = True
        '
        'ckUpdate
        '
        Me.ckUpdate.Enabled = True
        Me.ckUpdate.Interval = 1500
        '
        'btnSetting
        '
        Me.btnSetting.Location = New System.Drawing.Point(262, 126)
        Me.btnSetting.Name = "btnSetting"
        Me.btnSetting.Size = New System.Drawing.Size(98, 36)
        Me.btnSetting.TabIndex = 17
        Me.btnSetting.Text = "&Setting"
        Me.btnSetting.UseVisualStyleBackColor = True
        '
        'dSUN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(582, 383)
        Me.Controls.Add(Me.btnSetting)
        Me.Controls.Add(Me.btnAbout)
        Me.Controls.Add(Me.btnLog)
        Me.Controls.Add(Me.tientrinh)
        Me.Controls.Add(Me.txtNetVersion)
        Me.Controls.Add(Me.txtOutput)
        Me.Controls.Add(Me.txtAppName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.txtPathProject)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnDeploy)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "dSUN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "dSUN - Support deploy ASP.NET to Heroku"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnDeploy As Button
    Friend WithEvents btnOpen As Button
    Friend WithEvents txtPathProject As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtAppName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtOutput As TextBox
    Friend WithEvents txtNetVersion As TextBox
    Friend WithEvents tientrinh As ProgressBar
    Friend WithEvents btnLog As Button
    Friend WithEvents btnAbout As Button
    Friend WithEvents ckUpdate As Timer
    Friend WithEvents btnSetting As Button
End Class
