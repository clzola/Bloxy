Public Class PuzzleBlockRectangle
    Public Id As Integer

    Private mId As Integer
    Private mX As Integer
    Private mY As Integer
    Private mDiffX As Integer
    Private mDiffY As Integer
    Private mSnapX As Integer
    Private mSnapY As Integer
    Private mTextureId As Integer
    Private mTextureKey As String
    Private mParent As PuzzleBlock

    Public Property X() As Integer
        Get
            Return mX
        End Get
        Set(ByVal value As Integer)
            mX = value
        End Set
    End Property

    Public Property Y() As Integer
        Get
            Return mY
        End Get
        Set(ByVal value As Integer)
            mY = value
        End Set
    End Property

    Public Property DiffX() As Integer
        Get
            Return mDiffX
        End Get
        Set(ByVal value As Integer)
            mDiffX = value
        End Set
    End Property

    Public Property DiffY() As Integer
        Get
            Return mDiffY
        End Get
        Set(ByVal value As Integer)
            mDiffY = value
        End Set
    End Property

    Public Property SnapX() As Integer
        Get
            Return mSnapX
        End Get
        Set(ByVal value As Integer)
            mSnapX = value
        End Set
    End Property

    Public Property SnapY() As Integer
        Get
            Return mSnapY
        End Get
        Set(ByVal value As Integer)
            mSnapY = value
        End Set
    End Property

    Public Property TextureId() As Integer
        Get
            Return mTextureId
        End Get
        Set(ByVal value As Integer)
            mTextureId = value
        End Set
    End Property

    Public Property TextureKey() As String
        Get
            Return mTextureKey
        End Get
        Set(ByVal value As String)
            mTextureKey = value
        End Set
    End Property

    Public Property Parent() As PuzzleBlock
        Get
            Return mParent
        End Get
        Set(value As PuzzleBlock)
            mParent = value
        End Set
    End Property

    Public Sub New(aX As Integer, aY As Integer, aTexId As Integer, aTexKey As String)
        Me.mX = aX
        Me.mY = aY
        Me.mDiffX = 0
        Me.mDiffY = 0
        Me.mSnapX = aX
        Me.mSnapY = aY
        Me.mTextureId = aTexId
        Me.mTextureKey = aTexKey
    End Sub
End Class
