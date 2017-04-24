Namespace Bloxy
    Namespace Resource
        Public Class FontManager
            Private Content As ContentManager
            Private Fonts As Dictionary(Of String, SpriteFont)
            Private Shared SharedInstance As FontManager = New FontManager()
            Private BloxyGame As Bloxy.BloxyGame

            Public Shared ReadOnly Property Instance As FontManager
                Get
                    Return SharedInstance
                End Get
            End Property

            Private Sub New()
                Fonts = New Dictionary(Of String, SpriteFont)
            End Sub

            Public Sub SetContentManager(ByRef aContent As ContentManager)
                Me.Content = aContent
            End Sub

            Public Sub SetGameReference(ByRef aBloxyGame As Bloxy.BloxyGame)
                Me.BloxyGame = aBloxyGame
            End Sub

            Public Sub LoadFontFromFile(aFilename As String)
                Try
                    Dim FontFromFile As SpriteFont = Me.Content.Load(Of SpriteFont)(aFilename)
                    Me.Fonts.Add(aFilename, FontFromFile)
                Catch Ex As Exception
                    Console.WriteLine(Ex.Message)
                    Me.BloxyGame.Exit()
                End Try
            End Sub

            Public Function GetFont(ByVal aFilename As String) As SpriteFont
                Dim aFont As SpriteFont = Nothing
                Me.Fonts.TryGetValue(aFilename, aFont)
                Return aFont
            End Function
        End Class
    End Namespace
End Namespace

