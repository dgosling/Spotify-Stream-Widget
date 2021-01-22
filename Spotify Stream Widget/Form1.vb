Imports System.Drawing.Imaging
Public Class Form1
    'play gif animation by stripping frames
    Public WithEvents Timer1 As New System.Windows.Forms.Timer With {.Interval = 500}
    Private BitMaps As New List(Of Bitmap)
    Public imagecount As Integer = 0


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True

        'get images from gif file
        Using bm As New Bitmap(Application.StartupPath + "/hat.gif")
            Dim fd As New FrameDimension(bm.FrameDimensionsList(0))
            Dim framecount As Integer = bm.GetFrameCount(fd)
            If framecount > 1 Then
                For Each bmp As Bitmap In BitMaps
                    bmp.Dispose()

                Next
                BitMaps.Clear()
                For i As Integer = 0 To framecount - 1
                    bm.SelectActiveFrame(fd, i)
                    BitMaps.Add(New Bitmap(bm))
                    ' Console.WriteLine("added frame")
                Next
            End If
            fd = Nothing

        End Using
    End Sub
    Public Sub Test(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Start()
        'Console.WriteLine("started timer")
    End Sub
    Public Sub Timerint(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim cal As Double

        cal = 60000 / Viewer._tempo
        Dim interv As Integer = Math.Round(cal) / 15

        If (interv <> Timer1.Interval) Then
            Timer1.Interval = interv
            Console.WriteLine("set interval")
        End If

    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        imagecount += 1
        If imagecount > BitMaps.Count - 1 Then imagecount = 0
        PictureBox1.Invalidate()
    End Sub



    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        'draw the current frame
        e.Graphics.DrawImage(BitMaps(imagecount), 0, 0, PictureBox1.Size.Width, PictureBox1.Size.Height)

        'Console.WriteLine("drew image")

    End Sub
#Region "Form Dragging Function"
    'original: https://stackoverflow.com/a/24235555/9290012
    Dim _drag As Boolean
    Dim _mousex As Integer
    Dim _mousey As Integer

    Private Sub MoveForm_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown, PictureBox1.MouseDown
        _drag = True
        _mousex = Windows.Forms.Cursor.Position.X - Me.Left
        _mousey = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

    Private Sub MoveForm_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp, PictureBox1.MouseUp
        _drag = False
    End Sub

    Private Sub MoveForm_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove, PictureBox1.MouseMove
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
End Class