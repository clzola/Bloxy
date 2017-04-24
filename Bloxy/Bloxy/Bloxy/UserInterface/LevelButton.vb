Namespace Bloxy
    Namespace UserInterface
        Public Class LevelButton
            Public Boundaries As Rectangle
            Public LevelIndex As Integer
            Public Level As Levels.Level2

            Protected BtnTexture As Texture2D
            Protected BtnTextureHover As Texture2D
            Protected BtnTextureBorder As Texture2D
            Protected BtnTextureFnished As Texture2D

            Protected LastMouseState As MouseState
            Protected CurrentMouseState As MouseState
            Protected IsMouseOver As Boolean
            Protected ClickSound As SoundEffectInstance
            Protected MouseEnterd As Boolean

            Protected TextFont As SpriteFont
            Protected Text As String
            Public TextX As Integer
            Public TextY As Integer

            Public Event MouseClicked(ByRef Sender As Object, Lvl As Integer, LvlP As Levels.Level2)

            Public Sub New(aX As Integer, aY As Integer, W As Integer, H As Integer, LvlIndex As Integer, LvlPlaceholder As Levels.Level2)
                Me.Boundaries = New Rectangle(aX, aY, W, H)
                Me.IsMouseOver = False
                Me.MouseEnterd = False
                Me.LevelIndex = LvlIndex
                Me.Level = LvlPlaceholder

                Me.ClickSound = Resource.SoundManager.Instance.GetSoundEffectInstance("sounds\btnclck")
                Me.ClickSound.IsLooped = False
                Me.ClickSound.Volume = 1.0F



                Me.TextFont = Resource.FontManager.Instance.GetFont("fonts\lvlbuttons")
                Me.Text = CStr(LvlIndex)
                Me.TextX = Me.Boundaries.X + CInt(Me.Boundaries.Width / 2) - CInt(Me.TextFont.MeasureString(Text).X / 2)
                Me.TextY = Me.Boundaries.Y + CInt(Me.Boundaries.Height / 2) - CInt(Me.TextFont.MeasureString(Text).Y / 2)

                Me.BtnTexture = Resource.TextureManager.Instance.GetTexture("static\btnlevel_back")
                Me.BtnTextureHover = Resource.TextureManager.Instance.GetTexture("static\btnlevel_hover")
                Me.BtnTextureBorder = Resource.TextureManager.Instance.GetTexture("static\btnlevel_border")
                Me.BtnTextureFnished = Resource.TextureManager.Instance.GetTexture("static\btnlevel_finished")
            End Sub

            Public Sub Update()
                LastMouseState = CurrentMouseState
                CurrentMouseState = Mouse.GetState()

                Me.MouseEnterd = False
                Me.IsMouseOver = False
                If Boundaries.Contains(New Point(CurrentMouseState.X, CurrentMouseState.Y)) Then
                    Me.IsMouseOver = True
                    MouseEnterd = True
                End If

                If LastMouseState.LeftButton = ButtonState.Pressed And CurrentMouseState.LeftButton = ButtonState.Released Then
                    If Boundaries.Contains(New Point(CurrentMouseState.X, CurrentMouseState.Y)) Then
                        Me.ClickSound.Play()
                        RaiseEvent MouseClicked(Me, Me.LevelIndex, Me.Level)
                    End If
                End If
            End Sub

            Public Sub Draw(ByRef aSpriteBatch As SpriteBatch)
                aSpriteBatch.DrawString(TextFont, Text, New Vector2(TextX, TextY), Color.White, 0.0F, New Vector2(0, 0), 1.0F, SpriteEffects.None, 0.0003F)

                If Level.BestTime IsNot Nothing Then
                    aSpriteBatch.Draw(BtnTextureFnished, Boundaries, Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.002F)
                Else
                    aSpriteBatch.Draw(BtnTexture, Boundaries, Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.002F)
                End If

                aSpriteBatch.Draw(BtnTextureBorder, New Rectangle(Boundaries.X, Boundaries.Y, Boundaries.Width, 2), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.0005F)
                aSpriteBatch.Draw(BtnTextureBorder, New Rectangle(Boundaries.X, Boundaries.Y, 2, Boundaries.Height), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.0015F)
                aSpriteBatch.Draw(BtnTextureBorder, New Rectangle(Boundaries.X, Boundaries.Y + Boundaries.Height - 2, Boundaries.Width, 2), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.0005F)
                aSpriteBatch.Draw(BtnTextureBorder, New Rectangle(Boundaries.X + Boundaries.Width - 2, Boundaries.Y, 2, Boundaries.Height), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.0005F)

                If IsMouseOver = True Then
                    aSpriteBatch.Draw(BtnTextureHover, Boundaries, Nothing, Color.Red * 0.25F, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.001F)
                End If
            End Sub
        End Class
    End Namespace
End Namespace

