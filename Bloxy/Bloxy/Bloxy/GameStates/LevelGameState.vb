Namespace Bloxy
    Namespace GameStates
        Public Class LevelGameState
            Inherits BasicGameState

            Private BackgroundTexture As Texture2D
            Private NextGameStateId As Integer
            Private Bar As Texture2D
            Private BarX As Integer
            Private BarY As Integer
            Private MoveDelta As Integer
            Private Level As Levels.Level2

            Private BackToLevelsButton As UserInterface.BasicButton
            Private NextLevelButton As UserInterface.BasicButton
            Private ResetLevelButton As UserInterface.BasicButton

            Public Overrides Sub Initialize(ByRef aBloxyGame As BloxyGame)
                MyBase.Initialize(aBloxyGame)
                TransitionDelta = 0.05F
                MoveDelta = 4

                Me.Bar = TexManager.GetTexture("images\bar")
                Me.BackgroundTexture = TexManager.GetTexture("images\background")

                BackToLevelsButton = New UserInterface.BasicButton(0, 0, 47, 47)
                BackToLevelsButton.SetTextures(TexManager.GetTexture("buttons\menu"), TexManager.GetTexture("buttons\menu_glow"))
                AddHandler BackToLevelsButton.MouseClicked, AddressOf Me.BackToLevelsButton_Click

                ResetLevelButton = New UserInterface.BasicButton(0, 0, 47, 47)
                ResetLevelButton.SetTextures(TexManager.GetTexture("buttons\reset"), TexManager.GetTexture("buttons\reset_glow"))
                AddHandler ResetLevelButton.MouseClicked, AddressOf Me.ResetLevelButton_Click

                NextLevelButton = New UserInterface.BasicButton(0, 0, 47, 47)
                NextLevelButton.Hidden = True
                NextLevelButton.SetTextures(TexManager.GetTexture("buttons\rarrow"), TexManager.GetTexture("buttons\rarrow_glow"))
                AddHandler NextLevelButton.MouseClicked, AddressOf Me.NextLevelButton_Click
            End Sub

            Public Overrides Function GetId() As Integer
                Return GameState.Level
            End Function

            Public Overrides Sub LoadContent()
                BarX = 0
                BarY = 810
                ResetLevelButton.Boundaries.X = 30
                ResetLevelButton.Boundaries.Y = 813
                BackToLevelsButton.Boundaries.X = CInt(480 / 2 - 47 / 2)
                BackToLevelsButton.Boundaries.Y = 813
                Level = Levels.LevelManager2.Instance.CurrentLevel

                NextLevelButton.Boundaries.X = 450 - 47
                NextLevelButton.Boundaries.Y = 813
            End Sub

            Public Overrides Sub UnloadContent()

            End Sub

            Public Overrides Sub Update(aGameTime As GameTime)
                MyBase.Update(aGameTime)

                If State = ScreenState.TransitionFadeIn Then
                    If BarY <> 760 Then
                        BarY -= MoveDelta
                        ResetLevelButton.Boundaries.Y -= MoveDelta
                        BackToLevelsButton.Boundaries.Y -= MoveDelta
                        NextLevelButton.Boundaries.Y -= MoveDelta
                        If BarY <= 760 Then
                            BarY = 760
                            ResetLevelButton.Boundaries.Y = 763
                            NextLevelButton.Boundaries.Y = 763
                        End If
                    End If
                ElseIf State = ScreenState.TransitionFadeOut Then
                    If BarY <> 810 Then
                        BarY += MoveDelta
                        ResetLevelButton.Boundaries.Y += MoveDelta
                        BackToLevelsButton.Boundaries.Y += MoveDelta
                        NextLevelButton.Boundaries.Y += MoveDelta
                        If BarY >= 810 Then
                            BarY = 810
                            ResetLevelButton.Boundaries.Y = 813
                            NextLevelButton.Boundaries.Y = 813
                        End If
                    End If
                End If

                If State = ScreenState.Hidden Then
                    GameStateManager.Instance.Enter(NextGameStateId)
                End If

                ResetLevelButton.Update()
                BackToLevelsButton.Update()
                NextLevelButton.Update()
                Level.Update(aGameTime)
                If Level.IsCompleted Then
                    NextLevelButton.Hidden = False
                End If
            End Sub

            Public Overrides Sub Draw(ByRef aSpriteBatch As SpriteBatch, aGameTimer As GameTime)
                MyBase.Draw(aSpriteBatch, aGameTimer)

                ResetLevelButton.Draw(aSpriteBatch)
                BackToLevelsButton.Draw(aSpriteBatch)
                NextLevelButton.Draw(aSpriteBatch)

                Level.Draw(aSpriteBatch, aGameTimer)
                aSpriteBatch.Draw(Me.Bar, New Rectangle(BarX, BarY, 480, 50), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.2F)
                aSpriteBatch.Draw(Me.BackgroundTexture, New Rectangle(0, 0, 480, 810), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.3F)
            End Sub

            Private Sub BackToLevelsButton_Click()
                State = ScreenState.TransitionFadeOut
                NextGameStateId = GameState.Levels
                Levels.LevelManager2.Instance.CurrentLevel.Save()
            End Sub

            Private Sub NextLevelButton_Click()
                NextLevelButton.Hidden = True
                Levels.LevelManager2.Instance.CurrentLevel.Save()
                Level = Level.NextLevel

                If Level Is Nothing Then
                    State = ScreenState.TransitionFadeOut
                    NextGameStateId = GameState.Levels
                    Levels.LevelManager2.Instance.CurrentLevel.Save()
                End If

                Levels.LevelManager2.Instance.CurrentLevel = Level
            End Sub

            Private Sub ResetLevelButton_Click()
                NextLevelButton.Hidden = True
                Levels.LevelManager2.Instance.CurrentLevel.Reset()
            End Sub

        End Class
    End Namespace
End Namespace

