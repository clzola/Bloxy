Public Class LevelEditorPane
    Inherits System.Windows.Forms.Panel

    Public Sub New()
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.SetStyle(ControlStyles.DoubleBuffer, True)
    End Sub
End Class
