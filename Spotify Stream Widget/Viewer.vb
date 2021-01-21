﻿'===================================================================
'       Written by Marco Sadowski
'       Last Update: 2020-06-07
'       Please add your name after mine if you edit this code <3
'
'       Usage of the Viewer Form:
'       -   This is the main viewer which shows the current track from Spotify 
'       -   The Viewer uses the SmallViewer, NormalViewer and BigViewer as Template.
'       -   If you want to change the look of the form then you need to edit the template forms, not this form.
'===================================================================

Imports SpotifyAPI.Web, SpotifyAPI.Web.Auth, SpotifyAPI.Web.Enums, SpotifyAPI.Web.Models
Imports Spotify_Stream_Widget.Logger

Public Class Viewer
    Private Shared _spotify As SpotifyWebAPI
    Private Shared _spotifyAuth As TokenSwapAuth = New TokenSwapAuth("https://spotify-token-swap.camefrom.space/", "http://localhost:4002", Scope.UserReadPlaybackState)
    Dim _playback As PlaybackContext
    Dim _currentTrackId As String
    Dim _authorized As Boolean = False
    Dim _previousToken As Token
    Public _tempo As Single = 120
    Dim _audiof As AudioFeatures

    'Dim _pict As PictureBox
    'Dim WithEvents Timer1 As New System.Windows.Forms.Timer With {.Interval = 50}
    'Dim _pictTime As Decimal
    'Dim bitmaps As New List(Of Bitmap)
    'Dim imcount As Integer = 0

    'Public Sub test()
    '    Dim ttest As String
    '    ttest = Application.StartupPath
    '    Console.WriteLine(Application.StartupPath)
    'End Sub
    'Public Sub Pictget(sender As Object, e As EventArgs) Handles MyBase.Load
    '    Me.DoubleBuffered = True
    '    Console.WriteLine("testing")
    '    For Each foundfile In My.Computer.FileSystem.GetFiles(Application.StartupPath + "/resources/", FileIO.SearchOption.SearchTopLevelOnly, "*.png")
    '        Console.WriteLine("testing123")

    '        bitmaps.Add(New Bitmap(foundfile))
    '    Next
    'End Sub


    'Private Sub timer_tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    '    imcount = +1

    '    If imcount > bitmaps.Count - 1 Then imcount = 0
    '    PictureBox1.Invalidate()
    'End Sub




    'The Load Event apply the user settings and the event handler for the spotify API
    Private Sub Viewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ApplyProgressBarStyle()
        ApplySize()
        timeLabel.Text = "Don't forget to connect with Spotify."
        If My.Settings.DarkMode = False Then
            ApplyLightning(1)
        End If
        SetColor()
        Form1.Show()
    End Sub



#Region "Apply User Settings"
    'The user settings will be applied in this region. It change this size, change the color and change the theme.
    'If there is a new option that need to be applied in the viewer then add it in this region.
    Public Sub ApplyProgressBarStyle()
        'default to visable
        timeProgressBar.Visible = True
        Select Case My.Settings.ProgressBarStyle
            Case "Blocks"
                timeProgressBar.ProgressBarStyle = ProgressBarStyle.Blocks
            Case "Continuous"
                timeProgressBar.ProgressBarStyle = ProgressBarStyle.Continuous
            Case "Marquee"
                timeProgressBar.ProgressBarStyle = ProgressBarStyle.Marquee
            Case "Off"
                timeProgressBar.Visible = False
        End Select
    End Sub

    'copy the location of the objects and the size from the template based on the user settings.
    Public Sub ApplySize()
        Select Case My.Settings.Size
            Case "Small"
                Size = SmallViewer.Size
                AlbumCover.Size = SmallViewer.AlbumCover.Size
                AlbumCover.Location = SmallViewer.AlbumCover.Location
                TrackLabel.Location = SmallViewer.TrackLabel.Location
                ArtistLabel.Location = SmallViewer.ArtistLabel.Location
                timeProgressBar.Location = SmallViewer.timeProgressBar.Location
                timeProgressBar.Size = SmallViewer.timeProgressBar.Size
                Label1.Location = SmallViewer.Label1.Location
                Label1.Size = SmallViewer.Label1.Size
                timeLabel.Visible = False
                AlbumLabel.Visible = False

                'PictureBox1.Location = SmallViewer.PictureBox1.Location
                'PictureBox1.Size = SmallViewer.PictureBox1.Size
            Case "Normal"
                Size = NormalViewer.Size
                AlbumCover.Size = NormalViewer.AlbumCover.Size
                AlbumCover.Location = NormalViewer.AlbumCover.Location
                TrackLabel.Location = NormalViewer.TrackLabel.Location
                ArtistLabel.Location = NormalViewer.ArtistLabel.Location
                timeProgressBar.Location = NormalViewer.timeProgressBar.Location
                timeProgressBar.Size = NormalViewer.timeProgressBar.Size
                timeLabel.Visible = True
                timeLabel.Location = NormalViewer.timeLabel.Location
                timeLabel.Size = NormalViewer.timeLabel.Size
                Label1.Location = NormalViewer.Label1.Location
                Label1.Size = NormalViewer.Label1.Size
                'PictureBox1.Location = NormalViewer.PictureBox2.Location
                'PictureBox1.Size = NormalViewer.PictureBox2.Size
                AlbumLabel.Visible = False
            Case "Big"
                Size = BigViewer.Size
                AlbumCover.Size = BigViewer.AlbumCover.Size
                AlbumCover.Location = BigViewer.AlbumCover.Location
                TrackLabel.Location = BigViewer.TrackLabel.Location
                ArtistLabel.Location = BigViewer.ArtistLabel.Location
                timeProgressBar.Location = BigViewer.timeProgressBar.Location
                timeProgressBar.Size = BigViewer.timeProgressBar.Size
                timeLabel.Visible = True
                timeLabel.Location = BigViewer.timeLabel.Location
                timeLabel.Size = BigViewer.timeLabel.Size
                Label1.Location = BigViewer.Label1.Location
                Label1.Size = BigViewer.Label1.Size
                AlbumLabel.Visible = True
                AlbumLabel.Location = BigViewer.AlbumLabel.Location
                'PictureBox1.Location = BigViewer.PictureBox1.Location
                'PictureBox1.Size = BigViewer.PictureBox1.Size
        End Select

        'force refresh
        Me.Refresh()

    End Sub
    Public Sub SetPic()

    End Sub
    'change the style color of the viewer
    Public Sub SetColor()
        Select Case My.Settings.Color
            Case "Green"
                Style = MetroFramework.MetroColorStyle.Green
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Green
            Case "Black"
                Style = MetroFramework.MetroColorStyle.Black
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Black
            Case "White"
                Style = MetroFramework.MetroColorStyle.White
                timeProgressBar.Style = MetroFramework.MetroColorStyle.White
            Case "Silver"
                Style = MetroFramework.MetroColorStyle.Silver
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Silver
            Case "Blue"
                Style = MetroFramework.MetroColorStyle.Blue
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Blue
            Case "Lime"
                Style = MetroFramework.MetroColorStyle.Lime
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Lime
            Case "Teal"
                Style = MetroFramework.MetroColorStyle.Teal
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Teal
            Case "Orange"
                Style = MetroFramework.MetroColorStyle.Orange
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Orange
            Case "Brown"
                Style = MetroFramework.MetroColorStyle.Brown
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Brown
            Case "Pink"
                Style = MetroFramework.MetroColorStyle.Pink
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Pink
            Case "Magenta"
                Style = MetroFramework.MetroColorStyle.Magenta
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Magenta
            Case "Purple"
                Style = MetroFramework.MetroColorStyle.Purple
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Purple
            Case "Red"
                Style = MetroFramework.MetroColorStyle.Red
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Red
            Case "Yellow"
                Style = MetroFramework.MetroColorStyle.Yellow
                timeProgressBar.Style = MetroFramework.MetroColorStyle.Yellow
        End Select
        Refresh()
    End Sub

    'activate the Metro UI Light Theme // normaly it is dark so only needed to be switched when the user want a light overlay
    Public Sub ApplyLightning(mode As Integer)
        If mode = 1 Then
            Theme = MetroFramework.MetroThemeStyle.Light
            timeProgressBar.Theme = MetroFramework.MetroThemeStyle.Light
            timeLabel.Theme = MetroFramework.MetroThemeStyle.Light
            TrackLabel.ForeColor = Color.FromArgb(64, 64, 64)
            ArtistLabel.ForeColor = Color.FromArgb(64, 64, 64)
            AlbumLabel.ForeColor = Color.FromArgb(64, 64, 64)
            Label1.ForeColor = Color.FromArgb(64, 64, 64)
        Else
            Theme = MetroFramework.MetroThemeStyle.Dark
            timeProgressBar.Theme = MetroFramework.MetroThemeStyle.Dark
            timeLabel.Theme = MetroFramework.MetroThemeStyle.Dark
            TrackLabel.ForeColor = Color.FromArgb(171, 171, 171)
            ArtistLabel.ForeColor = Color.FromArgb(171, 171, 171)
            AlbumLabel.ForeColor = Color.FromArgb(171, 171, 171)
            Label1.ForeColor = Color.FromArgb(171, 171, 171)

        End If
        RefreshElements()
    End Sub

#End Region


    'Connects with Spotify. Needs to be called before all other SpotifyAPI functions
    Public Sub SpotifyConnect()
        timeLabel.Text = ""
        Settings.StatusLabel.Text = "Status: Connecting..."
        Settings.StatusLabel.ForeColor = Color.Yellow
        Settings.SpotifyConnectBtn.Enabled = False
        'Spotify Auth
        MsgBox("The Widget will open your browser to connect to the Spotify API")
        AddHandler _spotifyAuth.AuthReceived, AddressOf _spotify_AuthReceived
        AddHandler _spotifyAuth.OnAccessTokenExpired, AddressOf _spotify_OnAccessTokenExpired
        _spotifyAuth.Start()
        _spotifyAuth.OpenBrowser()
        UpdateTrack()
    End Sub

    'Handler for the auth process
    Private Async Sub _spotify_AuthReceived(sender, e)
        _previousToken = Await _spotifyAuth.ExchangeCodeAsync(e.Code)

        _spotify = New SpotifyWebAPI() With {
            .TokenType = _previousToken.TokenType,
            .AccessToken = _previousToken.AccessToken
            }

        _authorized = True
        _spotifyAuth.Stop()
    End Sub

    Private Async Sub _spotify_OnAccessTokenExpired(sender, e)
        Dim _newToken = Await _spotifyAuth.RefreshAuthAsync(_previousToken.RefreshToken)
        _spotify.AccessToken = _newToken.AccessToken
        _previousToken.AccessToken = _newToken.AccessToken
        _previousToken.CreateDate = _newToken.CreateDate
        Log("Auth refreshed: " & _spotify.AccessToken)
    End Sub

    Private Async Sub UpdateTrack()

        'Kill it if the Viewer is closed.
        If Settings.SpotifyConnectBtn.Text = "Open Viewer" Then Return

        'If the API is not authorized yet then _spotify is nothing. 
        'I don't trigger UpdateTrack() after AuthReceived got fired because I don't want to Invoke everything.
        If _spotify Is Nothing Then
            Await Task.Delay(5000)
            UpdateTrack()
            Return
        End If

        'get the current track
        'could fail if you do not have a internet connection
        Try
            'check if new token is needed


            If _previousToken.IsExpired() Then
                Log("Token expired")
                Settings.StatusLabel.Text = "Status: Connection lost. Try to refresh..."
                Settings.StatusLabel.ForeColor = Color.Yellow
                _spotify_OnAccessTokenExpired("", "")
                Await Task.Delay(5000)
                UpdateTrack()
                Return
            Else
                Settings.StatusLabel.Text = "Status: Connected"
                Settings.StatusLabel.ForeColor = Color.LimeGreen
                _playback = _spotify.GetPlayback()
                'Debug Info
                My.Settings.ApiCalls += 1
            End If

        Catch ex As Exception
            Log(3, "GetPlayback() Exception: " & ex.ToString())
            MsgBox("There was a problem with reaching the Spotify API. Please check your network connection and try again." + vbNewLine + "GetPlayback() Exception: " + ex.Message)
            Close()

            Return
        End Try

        'With HasError() you can check the error that got back of the spotify API.
        If _playback.HasError() Then
            Log(2, "Error Status: " & _playback.Error.Status & " Msg: " & _playback.Error.Message)
            Await Task.Delay(5000)
            UpdateTrack()
            Return
        End If

        If _playback.Item Is Nothing Then
            Await Task.Delay(5000)
            UpdateTrack()
            Return
        End If

        'update the time
        UpdateProgressBar(CInt(_playback.ProgressMs), CInt(_playback.Item.DurationMs))



        _audiof = _spotify.GetAudioFeatures(_playback.Item.Id)
        _tempo = _audiof.Tempo


        'Dim _dectempo As Decimal
        '_dectempo = Convert.ToDecimal(_tempo)
        Dim _inttempo As Int32
        _inttempo = Convert.ToInt32(_tempo)
        Label1.Text = _inttempo.ToString





        'check if the song is the same so the entire UI don't need to update. It also fake some seconds.
        If _currentTrackId = _playback.Item.Uri Then
            Dim fakeSeconds As Integer = 0
            Do While fakeSeconds <= 6
                If _playback.IsPlaying Then
                    UpdateProgressBar(timeProgressBar.Value + 1000, CInt(_playback.Item.DurationMs))
                End If
                Await Task.Delay(1000)
                fakeSeconds += 1
            Loop
            UpdateTrack()
            Return
        Else
            _currentTrackId = _playback.Item.Uri
        End If

        TrackLabel.Text = _playback.Item.Name
        Dim artists = ""
        For Each artist As SimpleArtist In _playback.Item.Artists
            If artists = "" Then
                artists = artist.Name
            Else
                artists = artists + " - " + artist.Name
            End If

        Next
        ArtistLabel.Text = artists
        AlbumLabel.Text = _playback.Item.Album.Name


        If _playback.Item.Album.Images.Count > 0 Then
            Try

                AlbumCover.Image = If(_playback.Item.Album.Images.Item(0).Url IsNot Nothing, GetImageFromUri(_playback.Item.Album.Images.Item(1).Url), Nothing)
            Catch ex As Exception
                Log(3, "AlbumCover Exception: " & ex.ToString())
                Dim dummyImage As Drawing.Image = New Bitmap(1, 1)
                AlbumCover.Image = dummyImage
            End Try
        Else
            AlbumCover.Image = My.Resources.albumArt
        End If

        'Refresh the elements
        RefreshElements()

        'change text size when the title is longer
        ResponsiveText()

        'export all details
        If My.Settings.ExportMode Then ExportData(_playback.Item.Name, _playback.Item.Album.Name, artists, AlbumCover.Image, _tempo.ToString)

        Await Task.Delay(1000)
        UpdateTrack()
    End Sub
    'Private Sub playani(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
    '    e.Graphics.DrawImage(bitmaps(imcount), PictureBox1.Location.X, PictureBox1.Location.Y)
    'End Sub

    Private Sub ExportData(track As String, album As String, artists As String, albumCover As Drawing.Image, _tempo As String)
        Try
            My.Computer.FileSystem.WriteAllText(Application.StartupPath + "/exported-details/track.txt", track, False)
            My.Computer.FileSystem.WriteAllText(Application.StartupPath + "/exported-details/album.txt", album, False)
            My.Computer.FileSystem.WriteAllText(Application.StartupPath + "/exported-details/artists.txt", artists, False)
            My.Computer.FileSystem.WriteAllText(Application.StartupPath + "/exported-datails/bpm.txt", _tempo.ToString, False)
            albumCover.Save(Application.StartupPath + "/exported-details/albumCover.png")
        Catch ex As Exception
            Log(3, "ExportData Exception: " & ex.ToString())
        End Try
    End Sub
    Private Sub UpdateProgressBar(cur As Integer, max As Integer)
        'check if one of the values are below 0
        If cur < 0 Or max < 0 Then Exit Sub
        timeProgressBar.Maximum = max
        If cur < max Then timeProgressBar.Value = cur
        timeLabel.Text = $"{FormatTime(cur)}/{FormatTime(max)}"

    End Sub

    'change the text size based on the text length
    Private Sub ResponsiveText()
        'Track
        Select Case TrackLabel.Text.Length
            Case 0 To 24
                TrackLabel.Font = New Font("Calibri", 20)
            Case 25 To 30
                TrackLabel.Font = New Font("Calibri", 17)
            Case 31 To 35
                TrackLabel.Font = New Font("Calibri", 15)
            Case Is >= 36
                TrackLabel.Font = New Font("Calibri", 12)
        End Select
        'Artist
        Select Case ArtistLabel.Text.Length
            Case 0 To 24
                ArtistLabel.Font = New Font("Calibri", 16)
            Case 25 To 30
                ArtistLabel.Font = New Font("Calibri", 14)
            Case 31 To 35
                ArtistLabel.Font = New Font("Calibri", 12)
            Case Is >= 36
                ArtistLabel.Font = New Font("Calibri", 10)
        End Select
        'Album
        Select Case AlbumLabel.Text.Length
            Case 0 To 30
                AlbumLabel.Font = New Font("Calibri", 14)
            Case 31 To 35
                AlbumLabel.Font = New Font("Calibri", 12)
            Case Is >= 36
                AlbumLabel.Font = New Font("Calibri", 10)
        End Select
        'tempo
        Select Case Label1.Text.Length
            Case 0 To 24
                Label1.Font = New Font("Calibri", 16)
            Case 25 To 30
                Label1.Font = New Font("Calibri", 14)
            Case 31 To 35
                Label1.Font = New Font("Calibri", 12)
            Case Is >= 36
                Label1.Font = New Font("Calibri", 10)
        End Select
    End Sub



    Private Shared Function FormatTime(ByVal ms As Double) As String
        Dim span As TimeSpan = TimeSpan.FromMilliseconds(ms)
        Dim secs As String = span.Seconds.ToString(), mins As String = span.Minutes.ToString()
        If secs.Length < 2 Then secs = "0" & secs
        Return mins & ":" + secs
    End Function

    Private Function GetImageFromUri(ByVal url As String) As Drawing.Image

        Dim retVal As Drawing.Image = Nothing

        Try
            If Not String.IsNullOrWhiteSpace(url) Then
                Dim req As Net.WebRequest = Net.WebRequest.Create(url.Trim)

                Using request As Net.WebResponse = req.GetResponse
                    Using stream As IO.Stream = request.GetResponseStream
                        retVal = New Bitmap(Drawing.Image.FromStream(stream))
                    End Using
                End Using
            End If

        Catch ex As Exception
            Log(3, "GetImageFromUri() Exception: " & ex.ToString())
            'if it fail to get the image then just take the dummy image. 
            retVal = My.Resources.albumArt
        End Try

        Return retVal

    End Function


#Region "Form Dragging Function"
    'original: https://stackoverflow.com/a/24235555/9290012
    Dim _drag As Boolean
    Dim _mousex As Integer
    Dim _mousey As Integer

    Private Sub MoveForm_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, TrackLabel.MouseDown, ArtistLabel.MouseDown, AlbumCover.MouseDown, AlbumLabel.MouseDown, timeLabel.MouseDown, timeProgressBar.MouseDown
        _drag = True
        _mousex = Windows.Forms.Cursor.Position.X - Me.Left
        _mousey = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

    Private Sub MoveForm_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, TrackLabel.MouseUp, ArtistLabel.MouseUp, AlbumCover.MouseUp, AlbumLabel.MouseUp, timeLabel.MouseUp, timeProgressBar.MouseUp
        _drag = False
    End Sub

    Private Sub MoveForm_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, TrackLabel.MouseMove, ArtistLabel.MouseMove, AlbumCover.MouseMove, AlbumLabel.MouseMove, timeLabel.MouseMove, timeProgressBar.MouseMove
        If _drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - _mousey
            Me.Left = Windows.Forms.Cursor.Position.X - _mousex
        End If
    End Sub
#End Region

    Private Sub CloseApp(sender As Object, e As EventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub

    'the viewer need to be in normal state, even if the configurator is hidden.
    Private Sub CatchMinimized(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub RefreshElements()
        Me.Refresh()
        AlbumCover.Refresh()
        ArtistLabel.Refresh()
        AlbumLabel.Refresh()
        TrackLabel.Refresh()
        Label1.Refresh()
        Form1.Refresh()
        'PictureBox1.Refresh()

        'Timer1.Interval = 60000 / _tempo
        'Timer1.Start()

        ' PictureBox1.Image = bitmaps(imcount)


    End Sub
End Class
