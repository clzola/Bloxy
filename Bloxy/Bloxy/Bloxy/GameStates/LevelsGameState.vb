Namespace Bloxy
    Namespace GameStates
        Public Class LevelsGameState
            Inherits BasicGameState

            Private BackgroundTexture As Texture2D
            Private Bar As Texture2D
            Private BarX As Integer
            Private BarY As Integer
            Private MoveDelta As Integer
            Private NextGameStateId As Integer
            Private LevelButtons As List(Of UserInterface.LevelButton)

            Private SlideLeftButton As UserInterface.BasicButton
            Private SlideRightButton As UserInterface.BasicButton
            Private BackToMenuButton As UserInterface.BasicButton

            Private PrevPage As Integer
            Private CurrPage As Integer
            Private NextPage As Integer

            Private SlideLeftOn As Boolean
            Private SlideRightOn As Boolean

            Private SlideLeftTempX As Integer
            Private SlideRightTempX As Integer
            Private LvlButtonMoveDelta As Integer

            Public Overrides Sub Initialize(ByRef aBloxyGame As BloxyGame)
                MyBase.Initialize(aBloxyGame)
                MoveDelta = 4
                TransitionDelta = 0.05F
                LvlButtonMoveDelta = 15

                BackgroundTexture = TexManager.GetTexture("images\background")
                Bar = TexManager.GetTexture("images\bar")

                SlideLeftButton = New UserInterface.BasicButton(0, 0, 47, 47)
                SlideLeftButton.SetTextures(TexManager.GetTexture("buttons\larrow"), TexManager.GetTexture("buttons\larrow_glow"))
                AddHandler SlideLeftButton.MouseClicked, AddressOf Me.SlideLeftButton_Click

                SlideRightButton = New UserInterface.BasicButton(0, 0, 47, 47)
                SlideRightButton.SetTextures(TexManager.GetTexture("buttons\rarrow"), TexManager.GetTexture("buttons\rarrow_glow"))
                AddHandler SlideRightButton.MouseClicked, AddressOf Me.SlideRightButton_Click

                BackToMenuButton = New UserInterface.BasicButton(0, 0, 47, 47)
                BackToMenuButton.SetTextures(TexManager.GetTexture("buttons\menu"), TexManager.GetTexture("buttons\menu_glow"))
                AddHandler BackToMenuButton.MouseClicked, AddressOf Me.BackToMenuButton_Click

                LevelButtons = New List(Of UserInterface.LevelButton)

                PrevPage = -1
                CurrPage = 0
                NextPage = 1

                Dim Y As Integer = 30
                Dim pageIndex As Integer = 0
                Dim iLevel As Integer = 0
                For Each ioLevel As Levels.Level2 In Levels.LevelManager2.Instance.Levels
                    If iLevel Mod 40 = 0 And iLevel > 0 Then
                        pageIndex += 1
                        Y = 30
                    End If

                    Dim X As Integer = (iLevel Mod 5) * 60 + ((iLevel Mod 5) + 1) * 30 + (480 * pageIndex)
                    If iLevel Mod 5 = 0 And iLevel > 0 And iLevel Mod 40 <> 0 Then
                        Y += 90
                    End If

                    Dim LvlBtn As UserInterface.LevelButton = New UserInterface.LevelButton(X, Y, 60, 60, iLevel + 1, ioLevel)
                    AddHandler LvlBtn.MouseClicked, AddressOf LevelButton_Click
                    iLevel = iLevel + 1
                    LevelButtons.Add(LvlBtn)
                Next
            End Sub

            Public Overrides Function GetId() As Integer
                Return GameState.Levels
            End Function

            Public Overrides Sub LoadContent()
                BarX = 0
                BarY = 810
                SlideLeftButton.Boundaries.X = 30
                SlideLeftButton.Boundaries.Y = 813
                SlideRightButton.Boundaries.X = 450 - 47
                SlideRightButton.Boundaries.Y = 813
                BackToMenuButton.Boundaries.X = CInt(480 / 2 - 47 / 2)
                BackToMenuButton.Boundaries.Y = 813
            End Sub

            Public Overrides Sub UnloadContent()

            End Sub

            Public Overrides Sub Update(aGameTime As GameTime)
                MyBase.Update(aGameTime)

                If State = ScreenState.TransitionFadeIn Then
                    If BarY <> 760 Then
                        BarY -= MoveDelta
                        SlideLeftButton.Boundaries.Y -= MoveDelta
                        SlideRightButton.Boundaries.Y -= MoveDelta
                        BackToMenuButton.Boundaries.Y -= MoveDelta
                        If BarY <= 760 Then
                            BarY = 760
                            SlideLeftButton.Boundaries.Y = 763
                            SlideRightButton.Boundaries.Y = 763
                            BackToMenuButton.Boundaries.Y = 763
                        End If
                    End If
                ElseIf State = ScreenState.TransitionFadeOut Then
                    If BarY <> 810 Then
                        BarY += MoveDelta
                        SlideLeftButton.Boundaries.Y += MoveDelta
                        SlideRightButton.Boundaries.Y += MoveDelta
                        BackToMenuButton.Boundaries.Y += MoveDelta
                        If BarY >= 810 Then
                            BarY = 810
                            SlideLeftButton.Boundaries.Y = 813
                            SlideRightButton.Boundaries.Y = 813
                            BackToMenuButton.Boundaries.Y = 813
                        End If
                    End If
                End If

                If State = ScreenState.Hidden Then
                    GameStateManager.Instance.Enter(NextGameStateId)
                End If

                If State = ScreenState.Normal Then
                    SlideLeftButton.Update()
                    SlideRightButton.Update()
                    BackToMenuButton.Update()

                    For iLvlBtn As Integer = 0 To LevelButtons.Count - 1
                        LevelButtons.ElementAt(iLvlBtn).Update()

                        'If Me.ElapsedTime.TotalMilliseconds Mod 20 = 0 Then
                        If SlideLeftOn = True Then
                            LevelButtons.ElementAt(iLvlBtn).Boundaries.X += Me.LvlButtonMoveDelta
                            LevelButtons.ElementAt(iLvlBtn).TextX += Me.LvlButtonMoveDelta
                        ElseIf SlideRightOn = True Then
                            LevelButtons.ElementAt(iLvlBtn).Boundaries.X -= Me.LvlButtonMoveDelta
                            LevelButtons.ElementAt(iLvlBtn).TextX -= Me.LvlButtonMoveDelta
                        End If
                        'End If
                    Next

                    If SlideLeftOn = True Then
                        SlideLeftTempX += Me.LvlButtonMoveDelta
                    ElseIf SlideRightOn = True Then
                        SlideRightTempX += Me.LvlButtonMoveDelta
                    End If

                    If SlideLeftTempX >= 480 Then
                        SlideLeftTempX = 0
                        SlideLeftOn = False
                        NextPage = CurrPage
                        CurrPage = PrevPage
                        PrevPage -= 1
                    ElseIf SlideRightTempX >= 480 Then
                        SlideRightTempX = 0
                        SlideRightOn = False
                        PrevPage = CurrPage
                        CurrPage = NextPage
                        NextPage += 1
                    End If
                End If
            End Sub

            Public Sub DrawLevelPage(ByRef aSpriteBatch As SpriteBatch)
                If PrevPage >= 0 Then
                    For Each LvlButton In LevelButtons.GetRange(PrevPage * 40, 40)
                        LvlButton.Draw(aSpriteBatch)
                    Next
                End If

                Dim LeftButtons As Integer = LevelButtons.Count - CurrPage * 40
                If LeftButtons >= 40 Then
                    LeftButtons = 40
                End If
                
                For Each LvlButton In LevelButtons.GetRange(CurrPage * 40, LeftButtons)
                    LvlButton.Draw(aSpriteBatch)
                Next

                If NextPage < CInt(Levels.LevelManager.Instance.NumberOfLevels / 40) + 1 Then
                    LeftButtons = LevelButtons.Count - NextPage * 40
                    If LeftButtons >= 40 Then
                        LeftButtons = 40
                    End If
                    For Each LvlButton In LevelButtons.GetRange(NextPage * 40, LeftButtons)
                        LvlButton.Draw(aSpriteBatch)
                    Next
                End If
            End Sub

            Public Overrides Sub Draw(ByRef aSpriteBatch As SpriteBatch, aGameTimer As GameTime)
                MyBase.Draw(aSpriteBatch, aGameTimer)

                aSpriteBatch.Draw(Me.Bar, New Rectangle(BarX, BarY, 480, 50), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.1F)

                SlideLeftButton.Draw(aSpriteBatch)
                SlideRightButton.Draw(aSpriteBatch)
                BackToMenuButton.Draw(aSpriteBatch)

                DrawLevelPage(aSpriteBatch)

                aSpriteBatch.Draw(Me.BackgroundTexture, New Rectangle(0, 0, 480, 810), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 1)
            End Sub

            Private Sub BackToMenuButton_Click()
                State = ScreenState.TransitionFadeOut
                NextGameStateId = GameState.Menu
            End Sub

            Private Sub SlideLeftButton_Click()
                If PrevPage > -1 Then
                    SlideLeftOn = True
                End If
            End Sub

            Private Sub SlideRightButton_Click()
                If NextPage < CInt(Levels.LevelManager.Instance.NumberOfLevels / 40) + 1 Then
                    SlideRightOn = True
                End If
            End Sub

            Private Sub LevelButton_Click(ByRef Sender As Object, ByVal LvlIndex As Integer, Lvl As Levels.Level2)
                State = ScreenState.TransitionFadeOut
                NextGameStateId = GameState.Level
                Levels.LevelManager2.Instance.CurrentLevel = Lvl
            End Sub
        End Class
    End Namespace
End Namespace

