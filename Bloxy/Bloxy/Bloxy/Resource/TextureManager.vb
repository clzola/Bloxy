Namespace Bloxy
    Namespace Resource
        Public Class TextureManager
            Private Content As ContentManager
            Private Textures As Dictionary(Of String, Texture2D)
            Private BloxyGame As Bloxy.BloxyGame
            Private Shared SharedInstance As TextureManager = New TextureManager()

            Public Shared ReadOnly Property Instance As TextureManager
                Get
                    Return TextureManager.SharedInstance
                End Get
            End Property

            Private Sub New()
                Me.Content = Nothing
                Me.Textures = New Dictionary(Of String, Texture2D)
                Me.BloxyGame = Nothing
            End Sub

            Public Sub SetContentManager(ByRef aContent As ContentManager)
                Me.Content = aContent
            End Sub

            Public Sub SetGameReference(ByRef aBloxyGame As Bloxy.BloxyGame)
                Me.BloxyGame = aBloxyGame
            End Sub

            Public Sub LoadTextureFromFile(ByVal aFilename As String)
                Try
                    Dim TextureFromFile As Texture2D = Me.Content.Load(Of Texture2D)(aFilename)
                    Me.Textures.Add(aFilename, TextureFromFile)
                Catch Ex As Exception
                    Console.WriteLine(Ex.Message)
                    Me.BloxyGame.Exit()
                End Try
            End Sub

            Public Sub LoadTextureFromMemory(ByVal Key As String, ByRef Tex As Texture2D)
                Me.Textures.Add(Key, Tex)
            End Sub

            Public Function GetTexture(ByVal aFilename As String) As Texture2D
                Dim aTexture As Texture2D = Nothing
                Me.Textures.TryGetValue(aFilename, aTexture)
                Return aTexture
            End Function
        End Class
    End Namespace
End Namespace

