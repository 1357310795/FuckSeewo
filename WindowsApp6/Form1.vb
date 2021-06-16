Imports System.Runtime.InteropServices

Public Class Form1
    Public seewo As Int32
    Public t2 As Timer
    Dim l As Label
    Private Declare Auto Function GetClientRect Lib "user32.dll" (
    ByVal hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    <StructLayout(LayoutKind.Sequential)>
    Public Structure RECT
        Public Left As Int32
        Public Top As Int32
        Public Right As Int32
        Public Bottom As Int32
    End Structure
    Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Int32
    Declare Function SetParent Lib "user32" Alias "SetParent" (ByVal hWndChild As Int32, ByVal hWndNewParent As Int32) As Int32
    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UIntPtr) As Boolean
    End Function
    Private Declare Function MoveWindow Lib "user32" (ByVal hwnd As Int32,
ByVal x As Int32, ByVal y As Int32, ByVal nWidth As Int32, ByVal nHeight As Int32,
ByVal bRepaint As Boolean) As Boolean
    Public Declare Function IsWindow Lib "user32" Alias "IsWindow" (ByVal hwnd As Int32) As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Shell("""C:\Program Files (x86)\Seewo\EasiCamera\sweclauncher\sweclauncher.exe""")

        l = New Label
        l.Parent = Me
        l.Font = New Font("宋体", 30)
        l.Text = "程序启动中，请稍后……"
        l.Visible = True
        l.AutoSize = True
        l.Top = Me.Height / 2 - l.Height / 2
        l.Left = Me.Width / 2 - l.Width / 2

        t2 = New Timer
        t2.Interval = 50
        AddHandler t2.Tick, AddressOf t2_Tick
        Timer1.Start()

    End Sub

    Private Sub t2_Tick(sender As Object, e As EventArgs)
        If IsWindow(seewo) = False Then
            End
        End If
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Dim a As RECT = New RECT
        Dim b As Int32 = GetClientRect(Me.Handle, a)
        MoveWindow(seewo, 0, 0, a.Right - a.Left, a.Bottom - a.Top, True)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        seewo = FindWindow(vbNullString, "希沃视频展台")
        If seewo <> 0 Then
            SetParent(seewo, Me.Handle)
            sender.stop()
            t2.Start()
            l.Text = ""
        End If
    End Sub
End Class
