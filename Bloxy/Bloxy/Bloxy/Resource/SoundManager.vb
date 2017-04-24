Namespace Bloxy
    Namespace Resource
        Public Class SoundManager
            Private Content As ContentManager
            Private Sounds As Dictionary(Of String, SoundEffect)
            Private BloxyGame As Bloxy.BloxyGame
            Private Shared SharedInstance As SoundManager = New SoundManager()

            Public Shared ReadOnly Property Instance As SoundManager
                Get
                    Return SoundManager.SharedInstance
                End Get
            End Property

            Private Sub New()
                Me.Content = Nothing
                Me.Sounds = New Dictionary(Of String, SoundEffect)
                Me.BloxyGame = Nothing
            End Sub

            Public Sub SetContentManager(ByRef aContent As ContentManager)
                Me.Content = aContent
            End Sub

            Public Sub SetGameReference(ByRef aBloxyGame As Bloxy.BloxyGame)
                Me.BloxyGame = aBloxyGame
            End Sub

            Public Sub LoadSoundFromFile(ByVal aFilename As String)
                Try
                    Dim SoundFromFile As SoundEffect = Me.Content.Load(Of SoundEffect)(aFilename)
                    Me.Sounds.Add(aFilename, SoundFromFile)
                Catch Ex As Exception
                    Console.WriteLine(Ex.Message)
                    Me.BloxyGame.Exit()
                End Try
            End Sub

            Public Function GetSoundEffectInstance(ByVal aFilename As String) As SoundEffectInstance
                Dim aSound As SoundEffect = Nothing
                Me.Sounds.TryGetValue(aFilename, aSound)
                Return aSound.CreateInstance()
            End Function
        End Class
    End Namespace
End Namespace

