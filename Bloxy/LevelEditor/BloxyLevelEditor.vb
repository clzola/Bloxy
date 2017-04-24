Public Class BloxyLevelEditor
    Private Filename As String
    Private FullFilename As String
    Private IsSaved As Boolean
    Private IsEdited As Boolean
    Private SelectedTool As BloxyTool
    Private SelectedButton As Button
    Private SelectedTextureId As Integer
    Private SelectedTextureKey As String

    Private LevelBlocks As List(Of Rectangle)
    Private LevelBlocksVisited(25, 16) As Boolean
    Private TexturesMap As Dictionary(Of String, Image)
    Private TexturesMapId As Dictionary(Of String, Integer)
    Private PuzzleBlocks As List(Of PuzzleBlock)
    Private PuzzleBlocksVisited(25, 16) As PuzzleBlockRectangle
    Private NextPuzzleBlockId As Integer
    Private NextPuzzleBlockRectangleId As Integer
    Private SelectedPuzzleBlock As PuzzleBlock

    Private GridPen As Pen
    Private GridBrush As SolidBrush
    Private MouseIn As Boolean
    Private GridHoverX As Integer
    Private GridHoverY As Integer
    Private LevelBlockBrush As SolidBrush
    Private LevelBlockBorderPen As Pen

    Private DebugWindow As MapDebug
    Private SaveDialogWindow As SaveFileDialog
    Private OpenDialogWindow As OpenFileDialog

    Private BloxyLevelBlueprint As JsonBlueprints.Bloxy.LevelJsonBlueprint

    Public Sub New()
        InitializeComponent()
        SaveDialogWindow = New SaveFileDialog()
        OpenDialogWindow = New OpenFileDialog()
        SaveDialogWindow.DefaultExt = "blx"
        OpenDialogWindow.DefaultExt = "blx"
        SaveDialogWindow.AddExtension = True
        OpenDialogWindow.AddExtension = True
        SaveDialogWindow.Filter = "Bloxy Level|*.blx"
        OpenDialogWindow.Filter = "Bloxy Level|*.blx"
        OpenDialogWindow.CheckFileExists = True
        DebugWindow = New MapDebug()
        DebugWindow.Show()
        IsSaved = False

        SelectedTool = BloxyTool.MouseTool
        LevelBlocks = New List(Of Rectangle)
        PuzzleBlocks = New List(Of PuzzleBlock)
        NextPuzzleBlockId = 1
        NextPuzzleBlockRectangleId = 1

        For I As Integer = 0 To 24 Step 1
            For J As Integer = 0 To 15 Step 1
                LevelBlocksVisited(I, J) = False
                PuzzleBlocksVisited(I, J) = Nothing
            Next
        Next

        Me.IsEdited = True
        Me.Filename = "Untitled"
        Me.Text = "BloxyLevelEditor - [" & Me.Filename & "]"

        GridPen = New Pen(Color.FromArgb(102, Color.White))
        GridBrush = New SolidBrush(Color.FromArgb(50, Color.Azure))
        MouseIn = False

        LevelBlockBrush = New SolidBrush(Color.FromArgb(102, Color.White))
        LevelBlockBorderPen = Pens.White

        TexturesMap = New Dictionary(Of String, Image)
        TexturesMap.Add("blocks\blue", BlueBlockButton.BackgroundImage)
        TexturesMap.Add("blocks\cyan", CyanBlockButton.BackgroundImage)
        TexturesMap.Add("blocks\green", GreenBlockButton.BackgroundImage)
        TexturesMap.Add("blocks\orange", OrangeBlockButton.BackgroundImage)
        TexturesMap.Add("blocks\pink", PinkBlockButton.BackgroundImage)
        TexturesMap.Add("blocks\purple", PurpleBlockButton.BackgroundImage)
        TexturesMap.Add("blocks\red", RedBlockButton.BackgroundImage)
        TexturesMap.Add("blocks\yellow", YellowBlockButton.BackgroundImage)
        TexturesMap.Add("blocks\brown", BrownBlockButton.BackgroundImage)

        TexturesMapId = New Dictionary(Of String, Integer)
        TexturesMapId.Add("blocks\blue", 1)
        TexturesMapId.Add("blocks\cyan", 2)
        TexturesMapId.Add("blocks\green", 4)
        TexturesMapId.Add("blocks\orange", 3)
        TexturesMapId.Add("blocks\pink", 8)
        TexturesMapId.Add("blocks\purple", 7)
        TexturesMapId.Add("blocks\red", 5)
        TexturesMapId.Add("blocks\yellow", 6)
        TexturesMapId.Add("blocks\brown", 9)

        ButtonSelect(MouseTool)

        DebugWindow.Render(PuzzleBlocksVisited)
    End Sub

    Private Sub LvlEditor_Paint(sender As Object, e As PaintEventArgs) Handles LvlEditor.Paint
        DrawGrid(e.Graphics)
        DrawLevelBlocks(e.Graphics)
        DrawPuzzleBlocks(e.Graphics)

        If MouseIn = True Then
            e.Graphics.FillRectangle(Me.GridBrush, New Rectangle(GridHoverX, GridHoverY, 30, 30))
        End If

        If IsEdited Then
            Me.Text = "BloxyLevelEditor-[*" & Me.Filename & "]"
        Else
            Me.Text = "BloxyLevelEditor-[" & Me.Filename & "]"
        End If
    End Sub

    Private Sub DrawGrid(graphics As Graphics)
        For iVertical As Integer = 0 To 480 Step 30
            graphics.DrawLine(Me.GridPen, iVertical, 0, iVertical, 750)
        Next

        For iHorizontal As Integer = 0 To 750 Step 30
            graphics.DrawLine(Me.GridPen, 0, iHorizontal, 480, iHorizontal)
        Next
    End Sub

    Private Sub DrawLevelBlocks(graphics As Graphics)
        For Each Rect In LevelBlocks
            Dim MapX As Integer = CInt(Rect.X / 30)
            Dim MapY As Integer = CInt(Rect.Y / 30)

            graphics.FillRectangle(LevelBlockBrush, Rect)

            'Draw Borders
            If LevelBlocksVisited(MapY, MapX + 1) = False Then
                graphics.DrawLine(Pens.White, MapX * 30 + 30, MapY * 30, MapX * 30 + 30, MapY * 30 + 30)
            End If

            If LevelBlocksVisited(MapY, MapX - 1) = False Then
                graphics.DrawLine(Pens.White, MapX * 30, MapY * 30, MapX * 30, MapY * 30 + 30)
            End If

            If LevelBlocksVisited(MapY + 1, MapX) = False Then
                graphics.DrawLine(Pens.White, MapX * 30, MapY * 30 + 30, MapX * 30 + 30, MapY * 30 + 30)
            End If

            If LevelBlocksVisited(MapY - 1, MapX) = False Then
                graphics.DrawLine(Pens.White, MapX * 30, MapY * 30, MapX * 30 + 30, MapY * 30)
            End If
        Next
    End Sub

    Private Sub DrawPuzzleBlocks(graphics As Graphics)
        For Each PuzzleBlock As PuzzleBlock In PuzzleBlocks
            PuzzleBlock.Draw(graphics)
        Next
    End Sub

    Private Sub LvlEditor_MouseEnter(sender As Object, e As EventArgs) Handles LvlEditor.MouseEnter
        MouseIn = True
    End Sub

    Private Sub LvlEditor_MouseLeave(sender As Object, e As EventArgs) Handles LvlEditor.MouseLeave
        MouseIn = False
        LvlEditor.Invalidate()
    End Sub

    Private Sub ButtonUnselect()
        If SelectedButton IsNot Nothing Then
            SelectedButton.FlatAppearance.BorderColor = Color.Black
            SelectedButton.FlatAppearance.BorderSize = 1
        End If
    End Sub

    Private Sub ButtonSelect(sender As Object)
        SelectedButton = CType(sender, Button)
        SelectedButton.FlatAppearance.BorderColor = Color.Red
        SelectedButton.FlatAppearance.BorderSize = 2
    End Sub

    Private Sub MouseTool_Click(sender As Object, e As EventArgs) Handles MouseTool.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.MouseTool
    End Sub

    Private Sub EraserTool_Click(sender As Object, e As EventArgs) Handles EraserTool.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.EraserTool
    End Sub

    Private Sub MoveTool_Click(sender As Object, e As EventArgs) Handles MoveTool.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.MoveTool
    End Sub

    Private Sub EmptyTool_Click(sender As Object, e As EventArgs) Handles EmptyTool.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.EmptyTool
    End Sub

    Private Sub BlueBlockButton_Click(sender As Object, e As EventArgs) Handles BlueBlockButton.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.BlockTool
        SelectedTextureId = 1
        SelectedTextureKey = "blocks\blue"
    End Sub

    Private Sub CyanBlockButton_Click(sender As Object, e As EventArgs) Handles CyanBlockButton.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.BlockTool
        SelectedTextureId = 2
        SelectedTextureKey = "blocks\cyan"
    End Sub

    Private Sub OrangeBlockButton_Click(sender As Object, e As EventArgs) Handles OrangeBlockButton.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.BlockTool
        SelectedTextureId = 3
        SelectedTextureKey = "blocks\orange"
    End Sub

    Private Sub GreenBlockButton_Click(sender As Object, e As EventArgs) Handles GreenBlockButton.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.BlockTool
        SelectedTextureId = 4
        SelectedTextureKey = "blocks\green"
    End Sub

    Private Sub RedBlockButton_Click(sender As Object, e As EventArgs) Handles RedBlockButton.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.BlockTool
        SelectedTextureId = 5
        SelectedTextureKey = "blocks\red"
    End Sub

    Private Sub YellowBlockButton_Click(sender As Object, e As EventArgs) Handles YellowBlockButton.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.BlockTool
        SelectedTextureId = 6
        SelectedTextureKey = "blocks\yellow"
    End Sub

    Private Sub PurpleBlockButton_Click(sender As Object, e As EventArgs) Handles PurpleBlockButton.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.BlockTool
        SelectedTextureId = 7
        SelectedTextureKey = "blocks\purple"
    End Sub

    Private Sub PinkBlockButton_Click(sender As Object, e As EventArgs) Handles PinkBlockButton.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.BlockTool
        SelectedTextureId = 8
        SelectedTextureKey = "blocks\pink"
    End Sub

    Private Sub BrownBlockButton_Click(sender As Object, e As EventArgs) Handles BrownBlockButton.Click
        ButtonUnselect()
        ButtonSelect(sender)
        SelectedTool = BloxyTool.BlockTool
        SelectedTextureId = 9
        SelectedTextureKey = "blocks\brown"
    End Sub

    Private Sub LvlEditor_Click(sender As Object, e As EventArgs) Handles LvlEditor.Click
        Dim MouseCoordinates As Point = LvlEditor.PointToClient(MousePosition)
        Dim X As Integer = MouseCoordinates.X - (MouseCoordinates.X Mod 30)
        Dim Y As Integer = MouseCoordinates.Y - (MouseCoordinates.Y Mod 30)
        Dim MapX As Integer = CInt(X / 30)
        Dim MapY As Integer = CInt(Y / 30)

        Select Case SelectedTool
            Case BloxyTool.EmptyTool
                If LevelBlocksVisited(MapY, MapX) = False Then
                    IsEdited = True
                    LevelBlocks.Add(New Rectangle(X, Y, 30, 30))
                    LevelBlocksVisited(MapY, MapX) = True
                End If
            Case BloxyTool.EraserTool
                If PuzzleBlocksVisited(MapY, MapX) IsNot Nothing Then
                    Dim PuzzleBlockParent As PuzzleBlock = PuzzleBlocksVisited(MapY, MapX).Parent
                    IsEdited = True

                    For Each PuzzleBlockRect As PuzzleBlockRectangle In PuzzleBlockParent.Body
                        Dim nX As Integer = CInt(PuzzleBlockRect.X / 30)
                        Dim nY As Integer = CInt(PuzzleBlockRect.Y / 30)
                        PuzzleBlocksVisited(nY, nX) = Nothing
                    Next

                    PuzzleBlocks.Remove(PuzzleBlockParent)
                ElseIf LevelBlocksVisited(MapY, MapX) = True Then
                    IsEdited = True
                    LevelBlocksVisited(MapY, MapX) = False
                    For Each LevelBlock As Rectangle In LevelBlocks
                        If LevelBlock.X = X AndAlso LevelBlock.Y = Y Then
                            LevelBlocks.Remove(LevelBlock)
                            Exit For
                        End If
                    Next
                End If
            Case BloxyTool.BlockTool
                Dim Connected As Boolean = False
                If PuzzleBlocksVisited(MapY, MapX) Is Nothing Then
                    IsEdited = True
                    Dim BlockRect As PuzzleBlockRectangle = New PuzzleBlockRectangle(X, Y, SelectedTextureId, SelectedTextureKey)
                    BlockRect.Id = NextPuzzleBlockRectangleId
                    NextPuzzleBlockRectangleId = NextPuzzleBlockRectangleId + 1
                    PuzzleBlocksVisited(MapY, MapX) = BlockRect

                    Dim BlockLeft As PuzzleBlockRectangle = Nothing
                    Dim BlockTop As PuzzleBlockRectangle = Nothing
                    Dim BlockRight As PuzzleBlockRectangle = Nothing
                    Dim BlockBottom As PuzzleBlockRectangle = Nothing

                    If MapX - 1 >= 0 Then
                        BlockLeft = PuzzleBlocksVisited(MapY, MapX - 1)
                    End If

                    If MapY - 1 >= 0 Then
                        BlockTop = PuzzleBlocksVisited(MapY - 1, MapX)
                    End If

                    If MapX + 1 <= 15 Then
                        BlockRight = PuzzleBlocksVisited(MapY, MapX + 1)
                    End If


                    If MapY + 1 <= 24 Then
                        BlockBottom = PuzzleBlocksVisited(MapY + 1, MapX)
                    End If

                    If BlockLeft IsNot Nothing AndAlso BlockLeft.TextureId = SelectedTextureId Then
                        Connected = True
                        BlockRect.Parent = BlockLeft.Parent
                        BlockRect.Parent.AddPuzzleBlockRectangle(BlockRect)
                    End If

                    If BlockRight IsNot Nothing AndAlso BlockRight.TextureId = SelectedTextureId Then
                        If Connected = True AndAlso BlockRect.Parent.Id <> BlockRight.Parent.Id Then
                            PuzzleBlocks.Remove(BlockRight.Parent)
                            For Each PuzzleBlockRect In BlockRight.Parent.Body
                                PuzzleBlockRect.Parent = BlockRect.Parent
                                BlockRect.Parent.AddPuzzleBlockRectangle(PuzzleBlockRect)
                            Next
                        Else
                            Connected = True
                            BlockRect.Parent = BlockRight.Parent
                            BlockRect.Parent.AddPuzzleBlockRectangle(BlockRect)
                        End If
                    End If

                    If BlockTop IsNot Nothing AndAlso BlockTop.TextureId = SelectedTextureId Then
                        If Connected = True AndAlso BlockRect.Parent.Id <> BlockTop.Parent.Id Then
                            PuzzleBlocks.Remove(BlockTop.Parent)
                            For Each PuzzleBlockRect In BlockTop.Parent.Body
                                PuzzleBlockRect.Parent = BlockRect.Parent
                                BlockRect.Parent.AddPuzzleBlockRectangle(PuzzleBlockRect)
                            Next
                        Else
                            Connected = True
                            BlockRect.Parent = BlockTop.Parent
                            BlockRect.Parent.AddPuzzleBlockRectangle(BlockRect)
                        End If
                    End If

                    If BlockBottom IsNot Nothing AndAlso BlockBottom.TextureId = SelectedTextureId Then
                        If Connected = True AndAlso BlockRect.Parent.Id <> BlockBottom.Parent.Id Then
                            PuzzleBlocks.Remove(BlockBottom.Parent)
                            For Each PuzzleBlockRect In BlockBottom.Parent.Body
                                PuzzleBlockRect.Parent = BlockRect.Parent
                                BlockRect.Parent.AddPuzzleBlockRectangle(PuzzleBlockRect)
                            Next
                        Else
                            Connected = True
                            BlockRect.Parent = BlockBottom.Parent
                            BlockRect.Parent.AddPuzzleBlockRectangle(BlockRect)
                        End If
                    End If

                    If Connected = False Then
                        Dim NewPuzzleBlock As PuzzleBlock = New PuzzleBlock(NextPuzzleBlockId, SelectedButton.BackgroundImage, SelectedTextureKey, SelectedTextureId)
                        NewPuzzleBlock.AddPuzzleBlockRectangle(BlockRect)
                        BlockRect.Parent = NewPuzzleBlock
                        PuzzleBlocks.Add(NewPuzzleBlock)
                        NextPuzzleBlockId = NextPuzzleBlockId + 1
                    End If
                End If
        End Select

        DebugWindow.Render(PuzzleBlocksVisited)
    End Sub

    Private Sub LvlEditor_MouseDown(sender As Object, e As MouseEventArgs) Handles LvlEditor.MouseDown
        If SelectedTool = BloxyTool.MoveTool Then
            'Find PuzzleBlock that needs to be moved
            Dim MouseCoordinates As Point = LvlEditor.PointToClient(MousePosition)

            For Each PuzzleBlockRef As PuzzleBlock In PuzzleBlocks
                For Each PuzzleBlockRect As PuzzleBlockRectangle In PuzzleBlockRef.Body
                    Dim TestRectangle As Rectangle = New Rectangle(PuzzleBlockRect.X, PuzzleBlockRect.Y, 30, 30)
                    If TestRectangle.Contains(MouseCoordinates) Then
                        SelectedPuzzleBlock = PuzzleBlockRef

                        For Each SelectedPuzzleBlockRect As PuzzleBlockRectangle In SelectedPuzzleBlock.Body
                            SelectedPuzzleBlockRect.DiffX = MouseCoordinates.X - SelectedPuzzleBlockRect.X
                            SelectedPuzzleBlockRect.DiffY = MouseCoordinates.Y - SelectedPuzzleBlockRect.Y
                            SelectedPuzzleBlockRect.SnapX = SelectedPuzzleBlockRect.X
                            SelectedPuzzleBlockRect.SnapY = SelectedPuzzleBlockRect.Y
                            PuzzleBlocksVisited(CInt(SelectedPuzzleBlockRect.Y / 30), CInt(SelectedPuzzleBlockRect.X / 30)) = Nothing
                        Next

                        Exit Sub
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub LvlEditor_MouseUp(sender As Object, e As MouseEventArgs) Handles LvlEditor.MouseUp
        If SelectedPuzzleBlock IsNot Nothing Then
            For Each SelectedPuzzleBlockRect As PuzzleBlockRectangle In SelectedPuzzleBlock.Body
                SelectedPuzzleBlockRect.X = SelectedPuzzleBlockRect.SnapX
                SelectedPuzzleBlockRect.Y = SelectedPuzzleBlockRect.SnapY
                PuzzleBlocksVisited(CInt(SelectedPuzzleBlockRect.Y / 30), CInt(SelectedPuzzleBlockRect.X / 30)) = SelectedPuzzleBlockRect
            Next
        End If

        SelectedPuzzleBlock = Nothing
    End Sub

    Private Sub LvlEditor_MouseMove(sender As Object, e As MouseEventArgs) Handles LvlEditor.MouseMove
        Dim MouseCoordinates As Point
        MouseCoordinates = LvlEditor.PointToClient(MousePosition)
        GridHoverX = MouseCoordinates.X - (MouseCoordinates.X Mod 30)
        GridHoverY = MouseCoordinates.Y - (MouseCoordinates.Y Mod 30)

        If SelectedPuzzleBlock IsNot Nothing Then
            IsEdited = True
            For Each SelectedPuzzleBlockRect As PuzzleBlockRectangle In SelectedPuzzleBlock.Body
                SelectedPuzzleBlockRect.SnapX = SelectedPuzzleBlockRect.X
                SelectedPuzzleBlockRect.SnapY = SelectedPuzzleBlockRect.Y

                Dim ModX As Integer = SelectedPuzzleBlockRect.SnapX Mod 30
                Dim ModY As Integer = SelectedPuzzleBlockRect.SnapY Mod 30

                If ModX <= 15 Then
                    SelectedPuzzleBlockRect.SnapX = SelectedPuzzleBlockRect.SnapX - ModX
                Else
                    SelectedPuzzleBlockRect.SnapX = SelectedPuzzleBlockRect.SnapX + (30 - ModX)
                End If

                If ModY <= 15 Then
                    SelectedPuzzleBlockRect.SnapY = SelectedPuzzleBlockRect.SnapY - ModY
                Else
                    SelectedPuzzleBlockRect.SnapY = SelectedPuzzleBlockRect.SnapY + (30 - ModY)
                End If

                SelectedPuzzleBlockRect.X = MouseCoordinates.X - SelectedPuzzleBlockRect.DiffX
                SelectedPuzzleBlockRect.Y = MouseCoordinates.Y - SelectedPuzzleBlockRect.DiffY
            Next
        End If

        LvlEditor.Invalidate()
    End Sub

    Private Sub NewButton_Click(sender As Object, e As EventArgs) Handles NewButton.Click
        IsEdited = True
        PuzzleBlocks.Clear()
        LevelBlocks.Clear()
        Me.Filename = "Untitled"
        Me.IsEdited = True
        IsSaved = False
        BloxyLevelBlueprint = Nothing

        For I As Integer = 0 To 24 Step 1
            For J As Integer = 0 To 15 Step 1
                LevelBlocksVisited(I, J) = False
                PuzzleBlocksVisited(I, J) = Nothing
            Next
        Next

        LvlEditor.Invalidate()
    End Sub

    Private Sub OpenButton_Click(sender As Object, e As EventArgs) Handles OpenButton.Click
        IsEdited = False
        IsSaved = True

        If Me.OpenDialogWindow.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Me.FullFilename = Me.OpenDialogWindow.FileName
        Me.Filename = System.IO.Path.GetFileName(Me.FullFilename)
        Me.Text = "BloxyLevelEditor-[" & Me.Filename & "]"

        Dim json As String = System.IO.File.ReadAllText(Me.FullFilename)
        BloxyLevelBlueprint = Newtonsoft.Json.JsonConvert.DeserializeObject(Of JsonBlueprints.Bloxy.LevelJsonBlueprint)(json)

        LevelIndexTextBox.Text = BloxyLevelBlueprint.Index

        Me.LevelBlocks.Clear()
        For I As Integer = 0 To 24 Step 1
            For J As Integer = 0 To 15 Step 1
                Me.LevelBlocksVisited(I, J) = False
            Next
        Next

        For Each LvlBlock As JsonBlueprints.Bloxy.PointBlueprint In BloxyLevelBlueprint.Map
            Me.LevelBlocks.Add(New Rectangle(LvlBlock.X, LvlBlock.Y, 30, 30))
            Me.LevelBlocksVisited(CInt(LvlBlock.Y / 30), CInt(LvlBlock.X / 30)) = True
        Next

        Me.PuzzleBlocks.Clear()
        For I As Integer = 0 To 24 Step 1
            For J As Integer = 0 To 15 Step 1
                Me.PuzzleBlocksVisited(I, J) = Nothing
            Next
        Next

        For Each PuzzleBlockBlueprint As JsonBlueprints.Bloxy.BlockJsonBlueprint In BloxyLevelBlueprint.Blocks
            Dim PuzzleBlockTexture As Image = GetTextureByTextureKey(PuzzleBlockBlueprint.Texture)
            Dim PuzzleBlockTextureId As Integer = GetTextureIdByTextureKey(PuzzleBlockBlueprint.Texture)
            Dim NewPuzzleBlock As PuzzleBlock = New PuzzleBlock(PuzzleBlockBlueprint.Id, PuzzleBlockTexture, PuzzleBlockBlueprint.Texture, PuzzleBlockTextureId)

            For Each PuzzleBlockRectBlueprint As JsonBlueprints.Bloxy.PointBlueprint In PuzzleBlockBlueprint.Body
                Dim aX As Integer = PuzzleBlockBlueprint.X + 30 * PuzzleBlockRectBlueprint.X
                Dim aY As Integer = PuzzleBlockBlueprint.Y + 30 * PuzzleBlockRectBlueprint.Y
                Dim vX As Integer = CInt(aX / 30)
                Dim vY As Integer = CInt(aY / 30)

                Dim NewPuzzleBlockRect As PuzzleBlockRectangle = New PuzzleBlockRectangle(aX, aY, PuzzleBlockTextureId, PuzzleBlockBlueprint.Texture)
                NewPuzzleBlock.Body.Add(NewPuzzleBlockRect)
                NewPuzzleBlockRect.Parent = NewPuzzleBlock
                Me.PuzzleBlocksVisited(vY, vX) = NewPuzzleBlockRect
            Next

            Me.PuzzleBlocks.Add(NewPuzzleBlock)
        Next

        Me.LvlEditor.Invalidate()
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        IsEdited = False

        If IsSaved = False Then
            If Me.SaveDialogWindow.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If

            Me.FullFilename = Me.SaveDialogWindow.FileName
            Me.Filename = System.IO.Path.GetFileName(FullFilename)
        End If

        If BloxyLevelBlueprint Is Nothing Then
            BloxyLevelBlueprint = New JsonBlueprints.Bloxy.LevelJsonBlueprint
            BloxyLevelBlueprint.Id = GetRandomString(32)
            BloxyLevelBlueprint.Index = 0

            If LevelIndexTextBox.Text.Trim().Length > 0 Then
                BloxyLevelBlueprint.Index = CInt(LevelIndexTextBox.Text.Trim())
            End If
        End If

        BloxyLevelBlueprint.Map.Clear()
        For Each LvlBlock As Rectangle In LevelBlocks
            Dim LvlBlockBlueprint As JsonBlueprints.Bloxy.PointBlueprint = New JsonBlueprints.Bloxy.PointBlueprint
            LvlBlockBlueprint.X = LvlBlock.X
            LvlBlockBlueprint.Y = LvlBlock.Y
            BloxyLevelBlueprint.Map.Add(LvlBlockBlueprint)
        Next

        BloxyLevelBlueprint.Blocks.Clear()
        For Each PuzzleBlockRef As PuzzleBlock In PuzzleBlocks
            Dim PuzzleBlockBlueprint As JsonBlueprints.Bloxy.BlockJsonBlueprint = PuzzleBlockRef.ToJsonBlueprint()
            BloxyLevelBlueprint.Blocks.Add(PuzzleBlockBlueprint)
        Next

        BloxyLevelBlueprint.Completed = False
        BloxyLevelBlueprint.BestTime = Nothing

        Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(BloxyLevelBlueprint, Newtonsoft.Json.Formatting.Indented)
        System.IO.File.WriteAllText(FullFilename, json)

        Me.Text = "BloxyLevelEditor-[" & Me.Filename & "]"
        IsSaved = True
    End Sub

    Private Function GetRandomString(ByVal iLength As Integer) As String
        Dim sResult As String = ""
        Dim sLetters = "0123456789abcdef"
        Dim rdm As New Random()

        For i As Integer = 1 To iLength
            sResult &= sLetters.Chars(rdm.Next(0, 15))
        Next

        Return sResult
    End Function

    Private Function GetTextureByTextureKey(Texture As String) As Image
        Dim Tex As Image = Nothing
        Me.TexturesMap.TryGetValue(Texture, Tex)
        Return Tex
    End Function

    Private Function GetTextureIdByTextureKey(Texture As String) As Integer
        Dim Tex As Integer = 0
        Me.TexturesMapId.TryGetValue(Texture, Tex)
        Return Tex
    End Function

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
