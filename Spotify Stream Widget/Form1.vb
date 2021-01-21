Imports System.IO
Imports System.Drawing.Imaging

Public Class Form1
    WithEvents MasterButton As New Button With {.Text = "Start", .Size = New Size(75, 25)}
    WithEvents NUD As New NumericUpDown With {.Minimum = 1, .Maximum = 500, .Value = 5, .Size = New Size(75, 25)}
    WithEvents Timer As New Timer With {.Interval = 5, .Enabled = False}
    Dim animatedImage As Bitmap
    Dim ImageCount As Integer
    Dim ImageWidth As Integer
    Dim ImageHeight As Integer
    Dim ImagePath As String = "C:\Users\danie_000\Source\Repos\Spotify-Stream-Widget\Spotify Stream Widget\bin\Debug\test\"
    Dim AnimatedGifPath As String = "C:\Users\danie_000\Source\Repos\Spotify-Stream-Widget\Spotify Stream Widget\bin\Debug\hat.gif"
    Dim PF As PointF
    Dim Index As Integer
    Private Function DisassembleAniGif(GifFile As Bitmap) As Integer
        Dim oDimension As New FrameDimension(GifFile.FrameDimensionsList(0))
        Dim FrameCount As Integer = animatedImage.GetFrameCount(oDimension)
        If FrameCount = 0 Then
            Return FrameCount
        End If
        ImageCount = FrameCount

        GifFile.SelectActiveFrame(oDimension, 0)
        Using AniGifFrame As New Bitmap(GifFile) ' one time just to get dimensions
            ImageWidth = AniGifFrame.Width
            ImageHeight = AniGifFrame.Height
        End Using

        For i As Integer = 0 To FrameCount - 1 ' process all if needed to get images saved
            GifFile.SelectActiveFrame(oDimension, i)
            If Not File.Exists(ImagePath & i.ToString("0000") & ".jpg") Then
                Using AniGifFrame As New Bitmap(animatedImage)
                    AniGifFrame.Save(ImagePath & i.ToString("0000") & ".jpg", ImageFormat.Jpeg)
                End Using
            End If
        Next
        Index = 0
        Return FrameCount
    End Function

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not File.Exists(AnimatedGifPath) Then
            MessageBox.Show(Me, "Cannot Find the Animated GIF", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End If
        animatedImage = Image.FromFile(AnimatedGifPath)
        If DisassembleAniGif(animatedImage) = 0 Then
            MessageBox.Show(Me, "Found the Animated GIF, but it has no frames", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End If
        MasterButton.Left = ImageWidth + 20
        MasterButton.Top = 10
        Me.Controls.Add(MasterButton)
        NUD.Left = ImageWidth + 20
        NUD.Top = MasterButton.Bottom + 10
        Me.Controls.Add(NUD)
        MasterButton.Text = "Start"
        Me.DoubleBuffered = True
        Me.Width = ImageWidth + NUD.Width + 100
        Me.Height = ImageHeight + 100
        PF = New PointF(Me.ClientRectangle.Left, Me.ClientRectangle.Top)
    End Sub

    Private Sub MasterButton_Click(sender As System.Object, e As System.EventArgs) Handles MasterButton.Click
        If MasterButton.Text = "Stop" Then
            Timer.Enabled = False
            MasterButton.Text = "Start"
        Else
            Timer.Enabled = True
            MasterButton.Text = "Stop"
        End If
    End Sub

    Private Sub NUD_ValueChanged(sender As Object, e As EventArgs) Handles NUD.ValueChanged
        Timer.Interval = NUD.Value
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Me.Invalidate()
        Index += 1
        Me.Text = ImagePath & Index.ToString("0000") & ".jpg"
        If Index >= 24 Then
            Index = 0
        End If
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        e.Graphics.DrawImage(Image.FromFile(ImagePath & Index.ToString("0000") & ".jpg"), PF)
    End Sub
End Class