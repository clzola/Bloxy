Namespace Bloxy
    Namespace Levels
        Public Class Level
            Private LevelMatrix(25, 16) As Integer
            Private UserLevelMatrix(25, 16) As Integer
            Public Blocks As List(Of Block)
            Private WhitePixelTexture As Texture2D
            Private LevelBodyPixelTexture As Texture2D
            Public LevelFinished As Boolean
            Public LevelIndex As Integer
            Public ToastState As Integer
            Public ToastTexture As Texture2D
            Private ElapsedTime As Integer
            Private ToastScale As Single
            Private ToastX As Integer
            Private ToastY As Integer
            Private LvlFilename As String
            Private ToastAlfa As Single
            Public NextLevel As Level

            Public Sub New(LvlFilename As String)
                Me.Blocks = New List(Of Block)
                Me.WhitePixelTexture = Resource.TextureManager.Instance.GetTexture("static\whitepixel")
                Me.LevelBodyPixelTexture = Resource.TextureManager.Instance.GetTexture("static\levelbody")
                Me.ToastTexture = Resource.TextureManager.Instance.GetTexture("images\toast")
                Me.LvlFilename = LvlFilename
                Me.LevelFinished = False
                Me.ToastState = 0
                Me.ElapsedTime = 0
                Me.ToastScale = 0
                Me.ToastX = 240
                Me.ToastY = 500
                Me.ToastAlfa = 0
            End Sub

            Public Sub CleanMatrix()
                For I As Integer = 0 To 24 Step 1
                    For J As Integer = 0 To 15 Step 1
                        LevelMatrix(I, J) = 0
                        UserLevelMatrix(I, J) = 0
                    Next
                Next
            End Sub

            Public Sub LoadLevel(aFilename As String)
                CleanMatrix()

                Dim json As String = IO.File.ReadAllText(aFilename)
                Dim LvlBlueprint As JsonBlueprints.Bloxy.LevelJsonBlueprint = Newtonsoft.Json.JsonConvert.DeserializeObject(Of JsonBlueprints.Bloxy.LevelJsonBlueprint)(json)

                For Each LvlMapRect As JsonBlueprints.Bloxy.PointBlueprint In LvlBlueprint.Map
                    Me.LevelMatrix(CInt(LvlMapRect.Y / 30), CInt(LvlMapRect.X / 30)) = 1
                Next

                For Each PuzzleBlockBlueprint As JsonBlueprints.Bloxy.BlockJsonBlueprint In LvlBlueprint.Blocks
                    Dim NewPuzzleBlock As Block = New Block(PuzzleBlockBlueprint.X, PuzzleBlockBlueprint.Y, PuzzleBlockBlueprint.Texture)
                    For Each PuzzleBlockRect As JsonBlueprints.Bloxy.PointBlueprint In PuzzleBlockBlueprint.Body
                        NewPuzzleBlock.AddBodyRectangleAt(PuzzleBlockRect.X, PuzzleBlockRect.Y)
                    Next

                    Dim BlockId As Integer = LevelManager.Instance.GetBlockMapId(NewPuzzleBlock.TextureKey)
                    For Each aBox In NewPuzzleBlock.Body
                        UserLevelMatrix(CInt((aBox.Y + NewPuzzleBlock.Y) / 30), CInt((aBox.X + NewPuzzleBlock.X) / 30)) = BlockId
                    Next

                    Blocks.Add(NewPuzzleBlock)
                Next

                'Dim LevelFile As IO.FileStream = New IO.FileStream(aFilename, IO.FileMode.Open)
                'Dim BinaryLevelReader As IO.BinaryReader = New IO.BinaryReader(LevelFile)

                'For I As Integer = 0 To 24 Step 1
                'For J As Integer = 0 To 15 Step 1
                'LevelMatrix(I, J) = BinaryLevelReader.ReadInt32()
                'Next
                'Next

                'Dim nBlocks As Integer = BinaryLevelReader.ReadInt32()

                'For I As Integer = 0 To nBlocks - 1 Step 1
                'Dim NewBlock As Block = Block.LoadFromFile(BinaryLevelReader)
                'Blocks.Add(NewBlock)

                'Dim BlockId As Integer = LevelManager.Instance.GetBlockMapId(NewBlock.TextureKey)

                'For Each aBox In NewBlock.Body
                'UserLevelMatrix(CInt((aBox.Y + NewBlock.Y) / 30), CInt((aBox.X + NewBlock.X) / 30)) = BlockId
                'Next
                'Next

                'BinaryLevelReader.Close()
                'LevelFile.Close()
            End Sub


            Public Sub Update(aGameTime As GameTime)
                If ToastState = 1 Then
                    ToastScale += 0.04F
                    ToastX -= 4
                    ToastY -= 2

                    If ToastAlfa <= 1.0F Then
                        ToastAlfa += 0.08F
                    End If

                    If ToastScale >= 1.0F Then
                        ToastScale = 1.0F
                        ToastState = 2
                        ToastX = 140
                        ToastY = 450
                    End If
                ElseIf ToastState = 3 Then
                    ToastScale -= 0.04F
                    ToastX += 4
                    ToastY += 2

                    If ToastAlfa >= 0.0F Then
                        ToastAlfa -= 0.08F
                    End If

                    If ToastScale <= 0.0F Then
                        ToastScale = 0.0F
                        ToastScale = 4
                        ToastX = 240
                        ToastY = 500
                    End If
                End If

                If ToastState = 2 Then
                    ElapsedTime += aGameTime.ElapsedGameTime.Milliseconds
                    If ElapsedTime > 3000 Then
                        ToastState = 3
                    End If
                End If

                If Not LevelFinished Then
                    For Each aBlock In Blocks
                        aBlock.Update(aGameTime, UserLevelMatrix)
                    Next

                    For I As Integer = 0 To 24 Step 1
                        For J As Integer = 0 To 15 Step 1
                            If LevelMatrix(I, J) = 1 Then
                                If Not (UserLevelMatrix(I, J) > 1) Then
                                    Exit Sub
                                End If
                            End If
                        Next
                    Next

                    LevelFinished = True
                    If ToastState = 0 Then
                        ToastState = 1
                    End If
                End If
            End Sub

            Public Sub Draw(ByRef aSpriteBatch As SpriteBatch, aGameTime As GameTime)
                For Y As Integer = 0 To 24 Step 1
                    For X As Integer = 0 To 16 Step 1
                        If LevelMatrix(Y, X) = 1 Then
                            aSpriteBatch.Draw(LevelBodyPixelTexture, New Rectangle(X * 30, Y * 30, 30, 30), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.1F)
                            If LevelMatrix(Y, X + 1) = 0 Then 'Right
                                aSpriteBatch.Draw(WhitePixelTexture, New Rectangle((X + 1) * 30, Y * 30, 1, 30), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.01F)
                            End If
                            If LevelMatrix(Y, X - 1) = 0 Then 'Left
                                aSpriteBatch.Draw(WhitePixelTexture, New Rectangle(X * 30 - 1, Y * 30, 1, 30), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.01F)
                            End If
                            If LevelMatrix(Y - 1, X) = 0 Then 'Top
                                aSpriteBatch.Draw(WhitePixelTexture, New Rectangle(X * 30, Y * 30 - 1, 30, 1), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.01F)
                            End If
                            If LevelMatrix(Y + 1, X) = 0 Then 'Bottom
                                aSpriteBatch.Draw(WhitePixelTexture, New Rectangle(X * 30, Y * 30 + 30, 30, 1), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.01F)
                            End If
                        End If
                    Next
                Next

                For Each iBlock As Block In Blocks
                    iBlock.Draw(aSpriteBatch)
                Next

                If ToastState = 1 Or ToastState = 2 Or ToastState = 3 Then
                    aSpriteBatch.Draw(ToastTexture, New Vector2(ToastX, ToastY), Nothing, Color.White * ToastAlfa, 0.0F, New Vector2(0, 0), ToastScale, SpriteEffects.None, 0.001F)
                End If
            End Sub
        End Class
    End Namespace
End Namespace

