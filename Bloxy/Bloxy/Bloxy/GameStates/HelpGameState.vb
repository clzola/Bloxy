Namespace Bloxy
    Namespace GameStates
        Public Class HelpGameState
            Inherits BasicGameState

            Private BackButton As UserInterface.BasicButton
            Private BackgroundTexture As Texture2D
            Private Bar As Texture2D
            Private BarX As Integer
            Private BarY As Integer
            Private MoveDelta As Integer

            Public Overrides Sub Initialize(ByRef aBloxyGame As BloxyGame)
                MyBase.Initialize(aBloxyGame)

                MoveDelta = 4
                TransitionDelta = 0.05F

                BackgroundTexture = TexManager.GetTexture("images\help1")
                Bar = TexManager.GetTexture("images\bar")

                BackButton = New UserInterface.BasicButton(0, 0, 47, 47)
                BackButton.SetTextures(TexManager.GetTexture("buttons\larrow"), TexManager.GetTexture("buttons\larrow_glow"))
                AddHandler BackButton.MouseClicked, AddressOf BackButton_Click
            End Sub

            Public Overrides Function GetId() As Integer
                Return GameState.Help
            End Function

            Public Overrides Sub LoadContent()
                BarX = 0
                BarY = 810
                BackButton.Boundaries.X = 30
                BackButton.Boundaries.Y = 813
            End Sub

            Public Overrides Sub UnloadContent()

            End Sub

            Public Overrides Sub Update(aGameTime As GameTime)
                MyBase.Update(aGameTime)

                If State = ScreenState.TransitionFadeIn Then
                    If BarY <> 760 Then
                        BarY -= MoveDelta
                        BackButton.Boundaries.Y -= MoveDelta
                        If BarY <= 760 Then
                            BarY = 760
                        End If
                    End If
                ElseIf State = ScreenState.TransitionFadeOut Then
                    If BarY <> 810 Then
                        BarY += MoveDelta
                        BackButton.Boundaries.Y += MoveDelta
                        If BarY >= 810 Then
                            BarY = 810
                        End If
                    End If
                End If

                If State = ScreenState.Hidden Then
                    GameStateManager.Instance.Enter(GameState.Menu)
                End If

                If State = ScreenState.Normal Then
                    BackButton.Update()
                End If
            End Sub

            Public Overrides Sub Draw(ByRef aSpriteBatch As SpriteBatch, aGameTimer As GameTime)
                MyBase.Draw(aSpriteBatch, aGameTimer)

                BackButton.Draw(aSpriteBatch)
                aSpriteBatch.Draw(Me.BackgroundTexture, New Rectangle(0, 0, 480, 810), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 1)
                aSpriteBatch.Draw(Me.Bar, New Rectangle(BarX, BarY, 480, 50), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 1)
            End Sub

            Private Sub BackButton_Click()
                State = ScreenState.TransitionFadeOut
            End Sub
        End Class
    End Namespace
End Namespace

