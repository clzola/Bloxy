Namespace Bloxy
    Namespace GameStates
        Public Class MenuGameState
            Inherits BasicGameState

            Private PlayButton As UserInterface.BasicButton
            Private HelpButton As UserInterface.BasicButton
            Private QuitButton As UserInterface.BasicButton
            Private BackgroundTexture As Texture2D
            Private NextGameStateId As Integer

            Public Overrides Sub Initialize(ByRef aBloxyGame As BloxyGame)
                MyBase.Initialize(aBloxyGame)

                TransitionDelta = 0.05F

                Me.BackgroundTexture = TexManager.GetTexture("images\background")
                Me.PlayButton = New UserInterface.BasicButton(187, 200, 105, 37)
                Me.PlayButton.SetTextures(TexManager.GetTexture("buttons\btnplay"), TexManager.GetTexture("buttons\btnplay_hover"))
                Me.HelpButton = New UserInterface.BasicButton(187, 257, 105, 37)
                Me.HelpButton.SetTextures(TexManager.GetTexture("buttons\btnhelp"), TexManager.GetTexture("buttons\btnhelp_hover"))
                Me.QuitButton = New UserInterface.BasicButton(187, 314, 105, 37)
                Me.QuitButton.SetTextures(TexManager.GetTexture("buttons\btnquit"), TexManager.GetTexture("buttons\btnquit_hover"))

                AddHandler Me.PlayButton.MouseClicked, AddressOf Me.PlayButton_Click
                AddHandler Me.HelpButton.MouseClicked, AddressOf Me.HelpButton_Click
                AddHandler Me.QuitButton.MouseClicked, AddressOf Me.QuitButton_Click
            End Sub

            Public Overrides Function GetId() As Integer
                Return GameState.Menu
            End Function

            Public Overrides Sub LoadContent()

            End Sub

            Public Overrides Sub UnloadContent()

            End Sub

            Public Overrides Sub Update(aGameTime As GameTime)
                MyBase.Update(aGameTime)

                PlayButton.Update()
                HelpButton.Update()
                QuitButton.Update()
            End Sub

            Public Overrides Sub Draw(ByRef aSpriteBatch As SpriteBatch, aGameTimer As GameTime)
                MyBase.Draw(aSpriteBatch, aGameTimer)

                PlayButton.Draw(aSpriteBatch)
                HelpButton.Draw(aSpriteBatch)
                QuitButton.Draw(aSpriteBatch)

                If State = ScreenState.Hidden Then
                    GameStateManager.Instance.Enter(NextGameStateId)
                End If

                aSpriteBatch.Draw(Me.BackgroundTexture, New Rectangle(0, 0, 480, 810), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 1)
            End Sub

            Public Sub PlayButton_Click()
                State = ScreenState.TransitionFadeOut
                NextGameStateId = GameState.Levels
            End Sub

            Public Sub HelpButton_Click()
                State = ScreenState.TransitionFadeOut
                NextGameStateId = GameState.Help
            End Sub

            Public Sub QuitButton_Click()
                Me.BloxyGame.Exit()
            End Sub
        End Class
    End Namespace
End Namespace

