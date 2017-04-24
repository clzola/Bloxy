Namespace Bloxy
    Namespace Levels
        Public Class Level2
            Implements IEquatable(Of Level2)
            Implements IComparable(Of Level2)

            Public Id As String
            Public Index As Integer
            Public IsCompleted As Boolean
            Public BestTime As String
            Public Filename As String
            Public NextLevel As Level2

            Public LevelMatrix(25, 16) As Boolean
            Public PuzzleBlocks As List(Of Block)
            Public UserLevelMatrix(25, 16) As Integer

            Private WhitePixelTexture As Texture2D
            Private LevelBodyPixelTexture As Texture2D

            Public ToastState As Integer
            Public ToastTexture As Texture2D
            Private ElapsedTime As Integer
            Private ToastScale As Single
            Private ToastX As Integer
            Private ToastY As Integer
            Private ToastAlfa As Single
            Private JsonString As String

            Public Sub New(aFilename As String, aJson As String)
                Me.WhitePixelTexture = Resource.TextureManager.Instance.GetTexture("static\whitepixel")
                Me.LevelBodyPixelTexture = Resource.TextureManager.Instance.GetTexture("static\levelbody")

                Me.ToastTexture = Resource.TextureManager.Instance.GetTexture("images\toast")
                Me.ToastState = 0
                Me.ElapsedTime = 0
                Me.ToastScale = 0
                Me.ToastX = 240
                Me.ToastY = 500
                Me.ToastAlfa = 0
                Me.JsonString = aJson

                Me.Filename = aFilename
                Me.NextLevel = Nothing
                Me.PuzzleBlocks = New List(Of Block)

                Dim LevelBlueptint As JsonBlueprints.Bloxy.LevelJsonBlueprint = Newtonsoft.Json.JsonConvert.DeserializeObject(Of JsonBlueprints.Bloxy.LevelJsonBlueprint)(aJson)
                Me.Id = LevelBlueptint.Id
                Me.Index = LevelBlueptint.Index
                Me.IsCompleted = LevelBlueptint.Completed
                Me.BestTime = LevelBlueptint.BestTime

                ResetLevelMatrix()
                ResetUserLevelMatrix()

                For Each WhiteRect As JsonBlueprints.Bloxy.PointBlueprint In LevelBlueptint.Map
                    LevelMatrix(CInt(WhiteRect.Y / 30), CInt(WhiteRect.X / 30)) = True
                Next

                For Each PuzzleBlockBlueprint As JsonBlueprints.Bloxy.BlockJsonBlueprint In LevelBlueptint.Blocks
                    Dim NewPuzzleBlock As Block = New Block(PuzzleBlockBlueprint.X, PuzzleBlockBlueprint.Y, PuzzleBlockBlueprint.Texture)
                    NewPuzzleBlock.Id = PuzzleBlockBlueprint.Id
                    For Each PuzzleBlockRect As JsonBlueprints.Bloxy.PointBlueprint In PuzzleBlockBlueprint.Body
                        NewPuzzleBlock.AddBodyRectangleAt(PuzzleBlockRect.X, PuzzleBlockRect.Y)
                        UserLevelMatrix(PuzzleBlockRect.Y + CInt(NewPuzzleBlock.Y / 30), PuzzleBlockRect.X + CInt(NewPuzzleBlock.X / 30)) = PuzzleBlockBlueprint.Id
                    Next

                    Me.PuzzleBlocks.Add(NewPuzzleBlock)
                Next

                For Each PuzzleBlockSaveState As JsonBlueprints.Bloxy.BlockStateBlueprint In LevelBlueptint.LastState.Blocks
                    For Each aBlock As Block In Me.PuzzleBlocks
                        If aBlock.Id = PuzzleBlockSaveState.Id Then
                            aBlock.X = PuzzleBlockSaveState.X
                            aBlock.Y = PuzzleBlockSaveState.Y
                        End If
                    Next
                Next
            End Sub

            Public Overrides Function ToString() As String
                Return "ID: " & Me.Id & "   Name: " & IO.Path.GetFileName(Me.Filename)
            End Function

            Public Overrides Function Equals(obj As Object) As Boolean
                If obj Is Nothing Then
                    Return False
                End If
                Dim objAsLevel As Level2 = TryCast(obj, Level2)
                If objAsLevel Is Nothing Then
                    Return False
                Else
                    Return Equals(objAsLevel)
                End If
            End Function

            ' Default comparer for Level2. 
            Public Function CompareTo(compareLevel As Level2) As Integer Implements IComparable(Of Level2).CompareTo
                ' A null value means that this object is greater. 
                If compareLevel Is Nothing Then
                    Return 1
                Else

                    If Me.Index < compareLevel.Index Then
                        Return -1
                    ElseIf Me.Index > compareLevel.Index Then
                        Return 1
                    Else
                        Return 0
                    End If
                End If
            End Function

            Public Overloads Function Equals(other As Level2) As Boolean Implements IEquatable(Of Level2).Equals
                If other Is Nothing Then
                    Return False
                End If
                Return (Me.Index.Equals(other.Index))
            End Function

            Private Sub ResetLevelMatrix()
                For I As Integer = 0 To 24 Step 1
                    For J As Integer = 0 To 15 Step 1
                        Me.LevelMatrix(I, J) = False
                    Next
                Next
            End Sub

            Private Sub ResetUserLevelMatrix()
                For I As Integer = 0 To 24 Step 1
                    For J As Integer = 0 To 15 Step 1
                        Me.UserLevelMatrix(I, J) = -1
                    Next
                Next
            End Sub

            Sub Update(aGameTime As GameTime)
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

                If Not IsCompleted Then
                    For Each aBlock In PuzzleBlocks
                        aBlock.Update(aGameTime, UserLevelMatrix)
                    Next

                    For I As Integer = 0 To 24 Step 1
                        For J As Integer = 0 To 15 Step 1
                            If LevelMatrix(I, J) = True Then
                                If Not (UserLevelMatrix(I, J) > 1) Then
                                    Exit Sub
                                End If
                            End If
                        Next
                    Next

                    IsCompleted = True
                    Me.BestTime = "finished"
                    If ToastState = 0 Then
                        ToastState = 1
                    End If
                End If
            End Sub

            Sub Draw(aSpriteBatch As SpriteBatch, aGameTimer As GameTime)
                For Y As Integer = 0 To 24 Step 1
                    For X As Integer = 0 To 16 Step 1
                        If LevelMatrix(Y, X) = True Then
                            aSpriteBatch.Draw(LevelBodyPixelTexture, New Rectangle(X * 30, Y * 30, 30, 30), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.1F)
                            If LevelMatrix(Y, X + 1) = False Then 'Right
                                aSpriteBatch.Draw(WhitePixelTexture, New Rectangle((X + 1) * 30, Y * 30, 1, 30), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.01F)
                            End If
                            If LevelMatrix(Y, X - 1) = False Then 'Left
                                aSpriteBatch.Draw(WhitePixelTexture, New Rectangle(X * 30 - 1, Y * 30, 1, 30), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.01F)
                            End If
                            If LevelMatrix(Y - 1, X) = False Then 'Top
                                aSpriteBatch.Draw(WhitePixelTexture, New Rectangle(X * 30, Y * 30 - 1, 30, 1), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.01F)
                            End If
                            If LevelMatrix(Y + 1, X) = False Then 'Bottom
                                aSpriteBatch.Draw(WhitePixelTexture, New Rectangle(X * 30, Y * 30 + 30, 30, 1), Nothing, Color.White, 0.0F, New Vector2(0, 0), SpriteEffects.None, 0.01F)
                            End If
                        End If
                    Next
                Next

                For Each iBlock As Block In PuzzleBlocks
                    iBlock.Draw(aSpriteBatch)
                Next

                If ToastState = 1 Or ToastState = 2 Or ToastState = 3 Then
                    aSpriteBatch.Draw(ToastTexture, New Vector2(ToastX, ToastY), Nothing, Color.White * ToastAlfa, 0.0F, New Vector2(0, 0), ToastScale, SpriteEffects.None, 0.001F)
                End If
            End Sub

            Public Sub Save()
                Dim Json As String = IO.File.ReadAllText(Me.Filename)
                Dim LevelBlueprint As JsonBlueprints.Bloxy.LevelJsonBlueprint = Newtonsoft.Json.JsonConvert.DeserializeObject(Of JsonBlueprints.Bloxy.LevelJsonBlueprint)(Json)
                LevelBlueprint.Completed = Me.IsCompleted
                LevelBlueprint.BestTime = Me.BestTime

                Dim LastState As JsonBlueprints.Bloxy.LevelStateBlueprint = New JsonBlueprints.Bloxy.LevelStateBlueprint
                For Each PuzzleBlockRef As Block In Me.PuzzleBlocks
                    Dim BlockState As JsonBlueprints.Bloxy.BlockStateBlueprint = New JsonBlueprints.Bloxy.BlockStateBlueprint
                    BlockState.Id = PuzzleBlockRef.Id
                    BlockState.X = PuzzleBlockRef.X
                    BlockState.Y = PuzzleBlockRef.Y
                    LastState.Blocks.Add(BlockState)
                Next

                LevelBlueprint.LastState = LastState
                Json = Newtonsoft.Json.JsonConvert.SerializeObject(LevelBlueprint, Newtonsoft.Json.Formatting.Indented)
                IO.File.WriteAllText(Me.Filename, Json)
            End Sub

            Public Sub Reset()
                Me.IsCompleted = False
                ResetUserLevelMatrix()
                Me.PuzzleBlocks.Clear()

                Me.ToastState = 0
                Me.ElapsedTime = 0
                Me.ToastScale = 0
                Me.ToastX = 240
                Me.ToastY = 500
                Me.ToastAlfa = 0

                Dim LevelBlueptint As JsonBlueprints.Bloxy.LevelJsonBlueprint = Newtonsoft.Json.JsonConvert.DeserializeObject(Of JsonBlueprints.Bloxy.LevelJsonBlueprint)(Me.JsonString)

                For Each PuzzleBlockBlueprint As JsonBlueprints.Bloxy.BlockJsonBlueprint In LevelBlueptint.Blocks
                    Dim NewPuzzleBlock As Block = New Block(PuzzleBlockBlueprint.X, PuzzleBlockBlueprint.Y, PuzzleBlockBlueprint.Texture)
                    NewPuzzleBlock.Id = PuzzleBlockBlueprint.Id
                    For Each PuzzleBlockRect As JsonBlueprints.Bloxy.PointBlueprint In PuzzleBlockBlueprint.Body
                        NewPuzzleBlock.AddBodyRectangleAt(PuzzleBlockRect.X, PuzzleBlockRect.Y)
                        UserLevelMatrix(PuzzleBlockRect.Y + CInt(NewPuzzleBlock.Y / 30), PuzzleBlockRect.X + CInt(NewPuzzleBlock.X / 30)) = PuzzleBlockBlueprint.Id
                    Next

                    Me.PuzzleBlocks.Add(NewPuzzleBlock)
                Next

                Save()
            End Sub
        End Class
    End Namespace
End Namespace

