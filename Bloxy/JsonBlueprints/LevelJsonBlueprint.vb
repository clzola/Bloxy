Namespace Bloxy
    Public Class LevelJsonBlueprint
        Public Id As String
        Public Index As Integer
        Public Map As List(Of PointBlueprint)
        Public Blocks As List(Of BlockJsonBlueprint)
        Public Completed As Boolean
        Public BestTime As String
        Public Hints As List(Of HintBlueprint)
        Public UsedHints As List(Of HintBlueprint)
        Public LastState As LevelStateBlueprint

        Public Sub New()
            Me.Map = New List(Of PointBlueprint)()
            Me.Blocks = New List(Of BlockJsonBlueprint)()
            Me.Hints = New List(Of HintBlueprint)()
            Me.UsedHints = New List(Of HintBlueprint)()
            Me.LastState = New LevelStateBlueprint()
        End Sub
    End Class
End Namespace

