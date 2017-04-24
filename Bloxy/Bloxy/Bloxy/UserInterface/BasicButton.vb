Namespace Bloxy
    Namespace UserInterface
        Public Class BasicButton
            Public Boundaries As Rectangle
            Public Hidden As Boolean
            Protected BtnTexture As Texture2D
            Protected BtnTextureHover As Texture2D
            Protected LastMouseState As MouseState
            Protected CurrentMouseState As MouseState
            Protected IsMouseOver As Boolean
            Protected ClickSound As SoundEffectInstance
            Protected HoverSound As SoundEffectInstance
            Protected MouseEnterd As Boolean
            Protected IsHoverSoundPlayed As Boolean

            Public Event MouseClicked()

            Public Sub New(aX As Integer, aY As Integer, W As Integer, H As Integer)
                Me.Boundaries = New Rectangle(aX, aY, W, H)
                Me.IsMouseOver = False
                Me.MouseEnterd = False
                Me.IsHoverSoundPlayed = False

                Me.Hidden = False

                Me.ClickSound = Resource.SoundManager.Instance.GetSoundEffectInstance("sounds\btnclck")
                Me.ClickSound.IsLooped = False
                Me.ClickSound.Volume = 1.0F

                Me.HoverSound = Resource.SoundManager.Instance.GetSoundEffectInstance("sounds\btnhover")
                Me.HoverSound.IsLooped = False
                Me.HoverSound.Volume = 0.2F
            End Sub

            Public Sub SetTextures(ByRef aBtnTexture As Texture2D, ByRef aBtnTextureHover As Texture2D)
                Me.BtnTexture = aBtnTexture
                Me.BtnTextureHover = aBtnTextureHover
            End Sub

            Public Sub Update()
                If Hidden = True Then
                    Exit Sub
                End If

                LastMouseState = CurrentMouseState
                CurrentMouseState = Mouse.GetState()

                If MouseEnterd = True And IsHoverSoundPlayed = False Then
                    Me.HoverSound.Play()
                    MouseEnterd = False
                    IsHoverSoundPlayed = True
                End If

                Me.MouseEnterd = False
                Me.IsMouseOver = False
                If Boundaries.Contains(New Point(CurrentMouseState.X, CurrentMouseState.Y)) Then
                    Me.IsMouseOver = True
                    MouseEnterd = True
                End If

                If MouseEnterd = False Then
                    IsHoverSoundPlayed = False
                End If

                If LastMouseState.LeftButton = ButtonState.Pressed And CurrentMouseState.LeftButton = ButtonState.Released Then
                    If Boundaries.Contains(New Point(CurrentMouseState.X, CurrentMouseState.Y)) Then
                        Me.ClickSound.Play()
                        RaiseEvent MouseClicked()
                    End If
                End If
            End Sub

            Public Sub Draw(ByRef aSpriteBatch As SpriteBatch)
                If Hidden = True Then
                    Exit Sub
                End If

                If IsMouseOver = True Then
                    aSpriteBatch.Draw(BtnTextureHover, Boundaries, Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.001)
                Else
                    aSpriteBatch.Draw(BtnTexture, Boundaries, Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.001)
                End If
            End Sub
        End Class
    End Namespace
End Namespace

