Namespace Bloxy
    Namespace Levels
        Public Class Block
            Public Id As Integer
            Public X As Integer
            Public Y As Integer
            Public SnapX As Integer
            Public SnapY As Integer
            Public DiffX As Integer
            Public DiffY As Integer
            Public DropShadow As Boolean
            Public BlockTexture As Texture2D
            Public TextureKey As String
            Public Body As LinkedList(Of Rectangle)

            Private Dragging As Boolean
            Private LastMouseState As MouseState
            Private CurrentMouseState As MouseState
            Private Depth As Single

            Public Width As Integer
            Public Height As Integer

            Public Sub New(aX As Integer, aY As Integer, aTexKey As String)
                Me.X = aX
                Me.Y = aY
                Me.SnapX = aX
                Me.SnapY = aY
                Me.DropShadow = False
                Me.TextureKey = aTexKey
                Me.BlockTexture = Bloxy.Resource.TextureManager.Instance.GetTexture(aTexKey)
                Me.Body = New LinkedList(Of Rectangle)
                Me.Depth = 0.001F
                Me.Width = 0
                Me.Height = 0
            End Sub

            Public Sub AddBodyRectangleAt(aX As Integer, aY As Integer)
                If Me.Width < aX * 30 + 30 Then
                    Me.Width = aX * 30 + 30
                End If

                If Me.Height < aY * 30 + 30 Then
                    Me.Height = aY * 30 + 30
                End If

                aX = aX * 30
                aY = aY * 30
                Me.Body.AddLast(New Rectangle(aX, aY, 30, 30))
            End Sub

            Public Sub Update(aGameTime As GameTime, ByRef ULM(,) As Integer)
                Me.LastMouseState = CurrentMouseState
                Me.CurrentMouseState = Mouse.GetState()

                If Me.LastMouseState.LeftButton = ButtonState.Released And Me.CurrentMouseState.LeftButton = ButtonState.Pressed Then
                    For Each aBox In Me.Body
                        ULM(CInt((aBox.Y + Me.Y) / 30), CInt((aBox.X + Me.X) / 30)) = 0
                    Next
                    MousePressedCallback()
                End If

                If Me.LastMouseState.LeftButton = ButtonState.Pressed And Me.CurrentMouseState.LeftButton = ButtonState.Released Then
                    MouseReleasedCallback()
                    For Each aBox In Me.Body
                        ULM(CInt((aBox.Y + Me.Y) / 30), CInt((aBox.X + Me.X) / 30)) = LevelManager.Instance.GetBlockMapId(TextureKey)
                    Next
                End If



                If Dragging Then
                    MouseDraggedCallback()
                End If
            End Sub

            Private Sub MousePressedCallback()
                Dim MousePosition As Point = New Point(CurrentMouseState.X, CurrentMouseState.Y)
                For Each Box In Body
                    Dim TmpBox As Rectangle = New Rectangle(Box.X + X, Box.Y + Y, 30, 30)
                    If TmpBox.Contains(MousePosition) = True Then
                        Dragging = True
                        Depth = 0.0001F

                        Me.SnapX = Me.X
                        Me.SnapY = Me.Y
                        Me.DiffX = MousePosition.X - Me.X
                        Me.DiffY = MousePosition.Y - Me.Y

                        Exit For
                    End If
                Next
            End Sub

            Private Sub MouseReleasedCallback()
                If Dragging = True Then
                    Dragging = False
                    Depth = 0.001F
                    Me.X = SnapX
                    Me.Y = SnapY
                End If
            End Sub

            Private Sub MouseDraggedCallback()
                Dim TmpX As Integer = CurrentMouseState.X - DiffX
                Dim TmpY As Integer = CurrentMouseState.Y - DiffY

                If (TmpX <= 0) Then
                    TmpX = 0
                End If

                If TmpY <= 0 Then
                    TmpY = 0
                End If

                If TmpX + Width >= 480 Then
                    TmpX = 480 - Width
                End If

                If TmpY + Height >= 750 Then
                    TmpY = 750 - Height
                End If

                Me.X = TmpX
                Me.Y = TmpY

                Dim inCollision As Boolean = False
                Dim Lvl As Level2 = LevelManager2.Instance.CurrentLevel
                For Each PuzzlePeace In Lvl.PuzzleBlocks
                    If Me.Intersects(PuzzlePeace) Then
                        inCollision = True
                        Exit For
                    End If
                Next


                If inCollision = False Then
                    Me.SnapX = Me.X
                    Me.SnapY = Me.Y

                    Dim ModX As Integer = Me.X Mod 30
                    Dim ModY As Integer = Me.Y Mod 30

                    If ModX <= 15 Then
                        Me.SnapX = Me.SnapX - ModX
                    Else
                        Me.SnapX = Me.SnapX + (30 - ModX)
                    End If

                    If ModY <= 15 Then
                        Me.SnapY = Me.SnapY - ModY
                    Else
                        Me.SnapY = Me.SnapY + (30 - ModY)
                    End If
                End If

            End Sub

            Public Sub Draw(ByRef aSpriteBatch As SpriteBatch)
                If Dragging = True Then
                    For Each iBlock As Rectangle In Body
                        aSpriteBatch.Draw(BlockTexture, New Rectangle(iBlock.X + SnapX, iBlock.Y + SnapY, 30, 30), Nothing, Color.White * 0.25F, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.001F)
                    Next
                End If

                For Each iBlock As Rectangle In Body
                    aSpriteBatch.Draw(BlockTexture, New Rectangle(iBlock.X + X, iBlock.Y + Y, 30, 30), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, Depth)
                Next
            End Sub

            Public Function Intersects(ByRef aBlock As Block) As Boolean
                If aBlock Is Me Then
                    Return False
                End If

                For Each MeBox In Body
                    For Each aBox In aBlock.Body
                        Dim TmpMeBox As Rectangle = New Rectangle(MeBox.X + X, MeBox.Y + Y, 30, 30)
                        Dim TmpABox As Rectangle = New Rectangle(aBox.X + aBlock.X, aBox.Y + aBlock.Y, 30, 30)

                        If TmpMeBox.Intersects(TmpABox) Then
                            Return True
                        End If
                    Next
                Next

                Return False
            End Function

            Public Shared Function LoadFromFile(ByRef BlockReader As IO.BinaryReader) As Block
                Dim X As Integer = BlockReader.ReadInt32()
                Dim Y As Integer = BlockReader.ReadInt32()
                Dim TextureKeyLength As Short = BlockReader.ReadInt16()
                Dim TextureKey As String = BlockReader.ReadChars(TextureKeyLength)

                Dim NewBlock As Block = New Block(X, Y, TextureKey)

                Dim nBlocks As Short = BlockReader.ReadInt16()
                For iBlock As Short = 0 To nBlocks - CShort(1) Step 1
                    Dim bX As Integer = BlockReader.ReadInt32()
                    Dim bY As Integer = BlockReader.ReadInt32()

                    NewBlock.AddBodyRectangleAt(bX, bY)
                Next

                Return NewBlock
            End Function
        End Class
    End Namespace
End Namespace
