Namespace Bloxy
    Namespace GameStates
        Public Class SplashGameState
            Inherits BasicGameState

            Private SplashTexture As Texture2D

            Public Overrides Sub Initialize(ByRef aBloxyGame As BloxyGame)
                MyBase.Initialize(aBloxyGame)

                SplashTexture = TexManager.GetTexture("images\splash")
                TransitionDelta = 0.04F
            End Sub

            Public Overrides Function GetId() As Integer
                Return GameState.Splash
            End Function

            Public Overrides Sub LoadContent()

            End Sub

            Public Overrides Sub UnloadContent()

            End Sub

            Public Overrides Sub Update(aGameTime As GameTime)
                MyBase.Update(aGameTime)

                If State = ScreenState.Normal Then
                    If MyBase.ElapsedTime.TotalMilliseconds > 3000 Then
                        State = ScreenState.TransitionFadeOut
                    End If
                ElseIf State = ScreenState.Hidden Then
                    GameStateManager.Instance.Enter(GameStates.GameState.Menu)
                End If
            End Sub

            Public Overrides Sub Draw(ByRef aSpriteBatch As SpriteBatch, aGameTimer As GameTime)
                MyBase.Draw(aSpriteBatch, aGameTimer)
                aSpriteBatch.Draw(Me.SplashTexture, New Rectangle(0, 0, 480, 810), Nothing, Color.White, 0, New Vector2(0, 0), SpriteEffects.None, 1)
            End Sub
        End Class
    End Namespace
End Namespace

