<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NormalViewer
    Inherits MetroFramework.Forms.MetroForm

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NormalViewer))
        Me.timeLabel = New MetroFramework.Controls.MetroLabel()
        Me.timeProgressBar = New MetroFramework.Controls.MetroProgressBar()
        Me.ArtistLabel = New System.Windows.Forms.Label()
        Me.TrackLabel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.AlbumCover = New System.Windows.Forms.PictureBox()
        CType(Me.AlbumCover, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'timeLabel
        '
        Me.timeLabel.Location = New System.Drawing.Point(172, 134)
        Me.timeLabel.Name = "timeLabel"
        Me.timeLabel.Size = New System.Drawing.Size(300, 19)
        Me.timeLabel.TabIndex = 14
        Me.timeLabel.Text = "time"
        Me.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.timeLabel.Theme = MetroFramework.MetroThemeStyle.Dark
        '
        'timeProgressBar
        '
        Me.timeProgressBar.Location = New System.Drawing.Point(174, 114)
        Me.timeProgressBar.Name = "timeProgressBar"
        Me.timeProgressBar.ProgressBarStyle = System.Windows.Forms.ProgressBarStyle.Blocks
        Me.timeProgressBar.Size = New System.Drawing.Size(298, 17)
        Me.timeProgressBar.Style = MetroFramework.MetroColorStyle.Green
        Me.timeProgressBar.TabIndex = 13
        Me.timeProgressBar.Theme = MetroFramework.MetroThemeStyle.Dark
        '
        'ArtistLabel
        '
        Me.ArtistLabel.AutoSize = True
        Me.ArtistLabel.Font = New System.Drawing.Font("Calibri", 16.0!)
        Me.ArtistLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.ArtistLabel.Location = New System.Drawing.Point(167, 71)
        Me.ArtistLabel.Name = "ArtistLabel"
        Me.ArtistLabel.Size = New System.Drawing.Size(174, 27)
        Me.ArtistLabel.TabIndex = 11
        Me.ArtistLabel.Text = "<< Artist Name >>"
        '
        'TrackLabel
        '
        Me.TrackLabel.AutoSize = True
        Me.TrackLabel.Font = New System.Drawing.Font("Calibri", 20.0!)
        Me.TrackLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.TrackLabel.Location = New System.Drawing.Point(166, 33)
        Me.TrackLabel.Name = "TrackLabel"
        Me.TrackLabel.Size = New System.Drawing.Size(205, 33)
        Me.TrackLabel.TabIndex = 10
        Me.TrackLabel.Text = "<< Track Name >>"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label1.Location = New System.Drawing.Point(424, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "<<BPM>>"
        '
        'AlbumCover
        '
        Me.AlbumCover.Location = New System.Drawing.Point(23, 33)
        Me.AlbumCover.Name = "AlbumCover"
        Me.AlbumCover.Size = New System.Drawing.Size(120, 120)
        Me.AlbumCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.AlbumCover.TabIndex = 9
        Me.AlbumCover.TabStop = False
        '
        'NormalViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 177)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.timeLabel)
        Me.Controls.Add(Me.timeProgressBar)
        Me.Controls.Add(Me.ArtistLabel)
        Me.Controls.Add(Me.TrackLabel)
        Me.Controls.Add(Me.AlbumCover)
        Me.DisplayHeader = False
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NormalViewer"
        Me.Padding = New System.Windows.Forms.Padding(20, 30, 20, 20)
        Me.Resizable = False
        Me.Style = MetroFramework.MetroColorStyle.Green
        Me.Text = "Spotify Viewer"
        Me.Theme = MetroFramework.MetroThemeStyle.Dark
        CType(Me.AlbumCover, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents timeLabel As MetroFramework.Controls.MetroLabel
    Friend WithEvents ArtistLabel As Label
    Friend WithEvents TrackLabel As Label
    Friend WithEvents AlbumCover As PictureBox
    Public WithEvents timeProgressBar As MetroFramework.Controls.MetroProgressBar
    Friend WithEvents Label1 As Label
End Class
