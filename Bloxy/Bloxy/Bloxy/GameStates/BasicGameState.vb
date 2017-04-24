Namespace Bloxy
    Namespace GameStates
        Public MustInherit Class BasicGameState
            Protected TransitionDelta As Single
            Protected Friend State As ScreenState
            Protected BloxyGame As Bloxy.BloxyGame
            Protected TransitionAlfa As Single
            Protected ElapsedTime As TimeSpan
            Protected TexManager As Bloxy.Resource.TextureManager
            Protected SndManager As Bloxy.Resource.SoundManager
            Protected FntManager As Bloxy.Resource.FontManager

            Public Overridable Sub Initialize(ByRef aBloxyGame As Bloxy.BloxyGame)
                Me.TransitionAlfa = 1.0F
                Me.TransitionDelta = 0.015F
                Me.State = ScreenState.Hidden
                Me.BloxyGame = aBloxyGame
                Me.TexManager = Bloxy.Resource.TextureManager.Instance
                Me.SndManager = Bloxy.Resource.SoundManager.Instance
                Me.FntManager = Bloxy.Resource.FontManager.Instance
            End Sub

            Public MustOverride Sub LoadContent()
            Public MustOverride Sub UnloadContent()
            Public MustOverride Function GetId() As Integer

            Public Overridable Sub Update(ByVal aGameTime As GameTime)
                ElapsedTime = ElapsedTime.Add(aGameTime.ElapsedGameTime)

                If State = ScreenState.TransitionFadeIn Then
                    TransitionAlfa -= TransitionDelta
                    If TransitionAlfa <= 0.0F Then
                        TransitionAlfa = 0.0F
                        State = ScreenState.Normal
                    End If
                ElseIf State = ScreenState.TransitionFadeOut Then
                    TransitionAlfa += TransitionDelta
                    If TransitionAlfa >= 1.0F Then
                        TransitionAlfa = 1.0F
                        State = ScreenState.Exited
                    End If
                End If

                If State = ScreenState.Entered Then
                    State = ScreenState.TransitionFadeIn
                ElseIf State = ScreenState.Exited Then
                    State = ScreenState.Hidden
                End If
            End Sub

            Public Sub BeginDraw(ByRef aSpriteBatch As SpriteBatch, ByVal aGameTimer As GameTime)
                aSpriteBatch.Begin(SpriteSortMode.BackToFront, Nothing)
            End Sub

            Public Sub EndDraw(ByRef aSpriteBatch As SpriteBatch, ByVal aGameTimer As GameTime)
                aSpriteBatch.End()
            End Sub


            Public Overridable Sub Draw(ByRef aSpriteBatch As SpriteBatch, ByVal aGameTimer As GameTime)
                aSpriteBatch.Draw(Resource.TextureManager.Instance.GetTexture("static\blackpixel"), New Rectangle(0, 0, Bloxy.BloxyGame.Width, Bloxy.BloxyGame.Height), Nothing, Color.White * Me.TransitionAlfa, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0)
            End Sub
        End Class
    End Namespace
End Namespace

