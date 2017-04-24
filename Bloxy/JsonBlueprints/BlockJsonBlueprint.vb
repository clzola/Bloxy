Namespace Bloxy
    Public Class BlockJsonBlueprint
        Public Id As Integer
        Public X As Integer
        Public Y As Integer
        Public Texture As String
        Public Body As List(Of PointBlueprint)

        Public Sub New()
            Me.Body = New List(Of PointBlueprint)
        End Sub
    End Class
End Namespace

