Namespace Bloxy
    Namespace GameStates
        Public Class GameStateManager
            Private GameStates As Dictionary(Of Integer, BasicGameState)
            Private CurrentGameState As BasicGameState

            Private Shared SharedInstance As GameStateManager = New GameStateManager

            Public Shared ReadOnly Property Instance As GameStateManager
                Get
                    Return SharedInstance
                End Get
            End Property

            Private Sub New()
                Me.GameStates = New Dictionary(Of Integer, BasicGameState)
                Me.CurrentGameState = Nothing
            End Sub

            Public Sub Register(ByRef gameState As BasicGameState)
                Me.GameStates.Add(gameState.GetId(), gameState)
            End Sub

            Public Function GetGameState(GameStateId As GameState) As BasicGameState
                Dim GmState As BasicGameState = Nothing
                Me.GameStates.TryGetValue(GameStateId, GmState)
                Return GmState
            End Function

            Public Sub Initialize(ByRef game As Bloxy.BloxyGame)
                For Each GameStateIterator In GameStates
                    GameStateIterator.Value.Initialize(game)
                Next
            End Sub

            Public Sub Enter(ByVal gameStateId As Integer)
                If Not (CurrentGameState Is Nothing) Then
                    CurrentGameState.UnloadContent()
                End If

                GameStates.TryGetValue(gameStateId, CurrentGameState)
                CurrentGameState.LoadContent()
                CurrentGameState.State = ScreenState.Entered
            End Sub

            Public Sub Update(ByVal gameTime As GameTime)
                If Not (CurrentGameState Is Nothing) Then
                    CurrentGameState.Update(gameTime)
                End If
            End Sub

            Public Sub Draw(ByRef spriteBatch As SpriteBatch, ByVal gameTime As GameTime)
                If Not (CurrentGameState Is Nothing) Then
                    CurrentGameState.BeginDraw(spriteBatch, gameTime)
                    CurrentGameState.Draw(spriteBatch, gameTime)
                    CurrentGameState.EndDraw(spriteBatch, gameTime)
                End If
            End Sub
        End Class
    End Namespace
End Namespace
