Public Class PuzzleBlock
    Private mId As Integer
    Private mTexture As Image
    Private mTextureKey As String
    Private mTextureId As Integer
    Private mBody As List(Of PuzzleBlockRectangle)

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.mId
        End Get
    End Property

    Public ReadOnly Property TextureKey() As String
        Get
            Return Me.mTextureKey
        End Get
    End Property

    Public ReadOnly Property TextureId() As Integer
        Get
            Return Me.mTextureId
        End Get
    End Property

    Public ReadOnly Property Body() As List(Of PuzzleBlockRectangle)
        Get
            Return Me.mBody
        End Get
    End Property

    Public Sub New(aId As Integer, aTex As Image, aTexKey As String, aTextId As Integer)
        Me.mId = aId
        Me.mTexture = aTex
        Me.mTextureId = aTextId
        Me.mTextureKey = aTexKey
        Me.mBody = New List(Of PuzzleBlockRectangle)
    End Sub

    Public Sub AddPuzzleBlockRectangle(ByRef aPuzzleBlockRectangle As PuzzleBlockRectangle)
        Me.mBody.Add(aPuzzleBlockRectangle)
    End Sub

    Public Sub Draw(ByRef graphics As Graphics)
        For Each PuzzleBlockRect As PuzzleBlockRectangle In Me.mBody
            graphics.DrawImage(Me.mTexture, PuzzleBlockRect.X, PuzzleBlockRect.Y)
        Next
    End Sub

    Public Function ToJsonBlueprint() As JsonBlueprints.Bloxy.BlockJsonBlueprint
        Dim PuzzleBlockBlueprint As JsonBlueprints.Bloxy.BlockJsonBlueprint = New JsonBlueprints.Bloxy.BlockJsonBlueprint()
        Dim X, Y As Integer

        X = 5000
        Y = 5000

        For Each PuzzleBlockRect As PuzzleBlockRectangle In Me.mBody
            If PuzzleBlockRect.X < X Then
                X = PuzzleBlockRect.X
            End If

            If PuzzleBlockRect.Y < Y Then
                Y = PuzzleBlockRect.Y
            End If
        Next

        PuzzleBlockBlueprint.Id = Me.mId
        PuzzleBlockBlueprint.X = X
        PuzzleBlockBlueprint.Y = Y
        PuzzleBlockBlueprint.Texture = Me.mTextureKey

        For Each PuzzleBlockRect As PuzzleBlockRectangle In Me.mBody
            Dim PuzzleBlockPointBlueprint As JsonBlueprints.Bloxy.PointBlueprint = New JsonBlueprints.Bloxy.PointBlueprint
            PuzzleBlockPointBlueprint.X = CInt((PuzzleBlockRect.X - X) / 30)
            PuzzleBlockPointBlueprint.Y = CInt((PuzzleBlockRect.Y - Y) / 30)
            PuzzleBlockBlueprint.Body.Add(PuzzleBlockPointBlueprint)
        Next

        Return PuzzleBlockBlueprint
    End Function
End Class
