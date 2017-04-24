<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BloxyLevelEditor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BloxyLevelEditor))
        Me.NewButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.OpenButton = New System.Windows.Forms.Button()
        Me.BlueBlockButton = New System.Windows.Forms.Button()
        Me.CyanBlockButton = New System.Windows.Forms.Button()
        Me.RedBlockButton = New System.Windows.Forms.Button()
        Me.GreenBlockButton = New System.Windows.Forms.Button()
        Me.YellowBlockButton = New System.Windows.Forms.Button()
        Me.OrangeBlockButton = New System.Windows.Forms.Button()
        Me.PurpleBlockButton = New System.Windows.Forms.Button()
        Me.BrownBlockButton = New System.Windows.Forms.Button()
        Me.PinkBlockButton = New System.Windows.Forms.Button()
        Me.MouseTool = New System.Windows.Forms.Button()
        Me.EraserTool = New System.Windows.Forms.Button()
        Me.MoveTool = New System.Windows.Forms.Button()
        Me.EmptyTool = New System.Windows.Forms.Button()
        Me.LvlEditor = New LevelEditor.LevelEditorPane()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LevelIndexTextBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'NewButton
        '
        Me.NewButton.Location = New System.Drawing.Point(498, 12)
        Me.NewButton.Name = "NewButton"
        Me.NewButton.Size = New System.Drawing.Size(75, 23)
        Me.NewButton.TabIndex = 1
        Me.NewButton.Text = "New"
        Me.NewButton.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(498, 70)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(75, 23)
        Me.SaveButton.TabIndex = 2
        Me.SaveButton.Text = "Save"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'OpenButton
        '
        Me.OpenButton.Location = New System.Drawing.Point(498, 41)
        Me.OpenButton.Name = "OpenButton"
        Me.OpenButton.Size = New System.Drawing.Size(75, 23)
        Me.OpenButton.TabIndex = 3
        Me.OpenButton.Text = "Open"
        Me.OpenButton.UseVisualStyleBackColor = True
        '
        'BlueBlockButton
        '
        Me.BlueBlockButton.BackgroundImage = CType(resources.GetObject("BlueBlockButton.BackgroundImage"), System.Drawing.Image)
        Me.BlueBlockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BlueBlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BlueBlockButton.Location = New System.Drawing.Point(495, 191)
        Me.BlueBlockButton.Name = "BlueBlockButton"
        Me.BlueBlockButton.Size = New System.Drawing.Size(40, 40)
        Me.BlueBlockButton.TabIndex = 5
        Me.BlueBlockButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BlueBlockButton.UseVisualStyleBackColor = True
        '
        'CyanBlockButton
        '
        Me.CyanBlockButton.BackgroundImage = CType(resources.GetObject("CyanBlockButton.BackgroundImage"), System.Drawing.Image)
        Me.CyanBlockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CyanBlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CyanBlockButton.Location = New System.Drawing.Point(541, 191)
        Me.CyanBlockButton.Name = "CyanBlockButton"
        Me.CyanBlockButton.Size = New System.Drawing.Size(40, 40)
        Me.CyanBlockButton.TabIndex = 6
        Me.CyanBlockButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CyanBlockButton.UseVisualStyleBackColor = True
        '
        'RedBlockButton
        '
        Me.RedBlockButton.BackgroundImage = CType(resources.GetObject("RedBlockButton.BackgroundImage"), System.Drawing.Image)
        Me.RedBlockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.RedBlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RedBlockButton.Location = New System.Drawing.Point(495, 283)
        Me.RedBlockButton.Name = "RedBlockButton"
        Me.RedBlockButton.Size = New System.Drawing.Size(40, 40)
        Me.RedBlockButton.TabIndex = 8
        Me.RedBlockButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.RedBlockButton.UseVisualStyleBackColor = True
        '
        'GreenBlockButton
        '
        Me.GreenBlockButton.BackgroundImage = CType(resources.GetObject("GreenBlockButton.BackgroundImage"), System.Drawing.Image)
        Me.GreenBlockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GreenBlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GreenBlockButton.Location = New System.Drawing.Point(495, 237)
        Me.GreenBlockButton.Name = "GreenBlockButton"
        Me.GreenBlockButton.Size = New System.Drawing.Size(40, 40)
        Me.GreenBlockButton.TabIndex = 7
        Me.GreenBlockButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.GreenBlockButton.UseVisualStyleBackColor = True
        '
        'YellowBlockButton
        '
        Me.YellowBlockButton.BackgroundImage = CType(resources.GetObject("YellowBlockButton.BackgroundImage"), System.Drawing.Image)
        Me.YellowBlockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.YellowBlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.YellowBlockButton.Location = New System.Drawing.Point(541, 283)
        Me.YellowBlockButton.Name = "YellowBlockButton"
        Me.YellowBlockButton.Size = New System.Drawing.Size(40, 40)
        Me.YellowBlockButton.TabIndex = 10
        Me.YellowBlockButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.YellowBlockButton.UseVisualStyleBackColor = True
        '
        'OrangeBlockButton
        '
        Me.OrangeBlockButton.BackgroundImage = CType(resources.GetObject("OrangeBlockButton.BackgroundImage"), System.Drawing.Image)
        Me.OrangeBlockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.OrangeBlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OrangeBlockButton.Location = New System.Drawing.Point(541, 237)
        Me.OrangeBlockButton.Name = "OrangeBlockButton"
        Me.OrangeBlockButton.Size = New System.Drawing.Size(40, 40)
        Me.OrangeBlockButton.TabIndex = 9
        Me.OrangeBlockButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.OrangeBlockButton.UseVisualStyleBackColor = True
        '
        'PurpleBlockButton
        '
        Me.PurpleBlockButton.BackgroundImage = CType(resources.GetObject("PurpleBlockButton.BackgroundImage"), System.Drawing.Image)
        Me.PurpleBlockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PurpleBlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PurpleBlockButton.Location = New System.Drawing.Point(541, 329)
        Me.PurpleBlockButton.Name = "PurpleBlockButton"
        Me.PurpleBlockButton.Size = New System.Drawing.Size(40, 40)
        Me.PurpleBlockButton.TabIndex = 12
        Me.PurpleBlockButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.PurpleBlockButton.UseVisualStyleBackColor = True
        '
        'BrownBlockButton
        '
        Me.BrownBlockButton.BackgroundImage = CType(resources.GetObject("BrownBlockButton.BackgroundImage"), System.Drawing.Image)
        Me.BrownBlockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BrownBlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BrownBlockButton.Location = New System.Drawing.Point(495, 375)
        Me.BrownBlockButton.Name = "BrownBlockButton"
        Me.BrownBlockButton.Size = New System.Drawing.Size(40, 40)
        Me.BrownBlockButton.TabIndex = 11
        Me.BrownBlockButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BrownBlockButton.UseVisualStyleBackColor = True
        '
        'PinkBlockButton
        '
        Me.PinkBlockButton.BackgroundImage = CType(resources.GetObject("PinkBlockButton.BackgroundImage"), System.Drawing.Image)
        Me.PinkBlockButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PinkBlockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PinkBlockButton.Location = New System.Drawing.Point(495, 329)
        Me.PinkBlockButton.Name = "PinkBlockButton"
        Me.PinkBlockButton.Size = New System.Drawing.Size(40, 40)
        Me.PinkBlockButton.TabIndex = 13
        Me.PinkBlockButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.PinkBlockButton.UseVisualStyleBackColor = True
        '
        'MouseTool
        '
        Me.MouseTool.BackgroundImage = CType(resources.GetObject("MouseTool.BackgroundImage"), System.Drawing.Image)
        Me.MouseTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.MouseTool.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.MouseTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MouseTool.Location = New System.Drawing.Point(495, 99)
        Me.MouseTool.Name = "MouseTool"
        Me.MouseTool.Size = New System.Drawing.Size(40, 40)
        Me.MouseTool.TabIndex = 14
        Me.MouseTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.MouseTool.UseVisualStyleBackColor = True
        '
        'EraserTool
        '
        Me.EraserTool.BackgroundImage = CType(resources.GetObject("EraserTool.BackgroundImage"), System.Drawing.Image)
        Me.EraserTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.EraserTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EraserTool.Location = New System.Drawing.Point(541, 99)
        Me.EraserTool.Name = "EraserTool"
        Me.EraserTool.Size = New System.Drawing.Size(40, 40)
        Me.EraserTool.TabIndex = 15
        Me.EraserTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.EraserTool.UseVisualStyleBackColor = True
        '
        'MoveTool
        '
        Me.MoveTool.BackgroundImage = CType(resources.GetObject("MoveTool.BackgroundImage"), System.Drawing.Image)
        Me.MoveTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.MoveTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MoveTool.Location = New System.Drawing.Point(541, 145)
        Me.MoveTool.Name = "MoveTool"
        Me.MoveTool.Size = New System.Drawing.Size(40, 40)
        Me.MoveTool.TabIndex = 16
        Me.MoveTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.MoveTool.UseVisualStyleBackColor = True
        '
        'EmptyTool
        '
        Me.EmptyTool.BackgroundImage = CType(resources.GetObject("EmptyTool.BackgroundImage"), System.Drawing.Image)
        Me.EmptyTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.EmptyTool.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EmptyTool.Location = New System.Drawing.Point(495, 145)
        Me.EmptyTool.Name = "EmptyTool"
        Me.EmptyTool.Size = New System.Drawing.Size(40, 40)
        Me.EmptyTool.TabIndex = 17
        Me.EmptyTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.EmptyTool.UseVisualStyleBackColor = True
        '
        'LvlEditor
        '
        Me.LvlEditor.BackgroundImage = CType(resources.GetObject("LvlEditor.BackgroundImage"), System.Drawing.Image)
        Me.LvlEditor.Location = New System.Drawing.Point(12, 12)
        Me.LvlEditor.Name = "LvlEditor"
        Me.LvlEditor.Size = New System.Drawing.Size(480, 750)
        Me.LvlEditor.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(498, 466)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Level Index:"
        '
        'LevelIndexTextBox
        '
        Me.LevelIndexTextBox.Location = New System.Drawing.Point(498, 482)
        Me.LevelIndexTextBox.Name = "LevelIndexTextBox"
        Me.LevelIndexTextBox.Size = New System.Drawing.Size(83, 20)
        Me.LevelIndexTextBox.TabIndex = 19
        '
        'BloxyLevelEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(593, 775)
        Me.Controls.Add(Me.LevelIndexTextBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.EmptyTool)
        Me.Controls.Add(Me.MoveTool)
        Me.Controls.Add(Me.EraserTool)
        Me.Controls.Add(Me.MouseTool)
        Me.Controls.Add(Me.PinkBlockButton)
        Me.Controls.Add(Me.PurpleBlockButton)
        Me.Controls.Add(Me.BrownBlockButton)
        Me.Controls.Add(Me.YellowBlockButton)
        Me.Controls.Add(Me.OrangeBlockButton)
        Me.Controls.Add(Me.RedBlockButton)
        Me.Controls.Add(Me.GreenBlockButton)
        Me.Controls.Add(Me.CyanBlockButton)
        Me.Controls.Add(Me.BlueBlockButton)
        Me.Controls.Add(Me.OpenButton)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.NewButton)
        Me.Controls.Add(Me.LvlEditor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "BloxyLevelEditor"
        Me.Text = "Bloxy LevelEditor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LvlEditor As LevelEditor.LevelEditorPane
    Friend WithEvents NewButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents OpenButton As System.Windows.Forms.Button
    Friend WithEvents BlueBlockButton As System.Windows.Forms.Button
    Friend WithEvents CyanBlockButton As System.Windows.Forms.Button
    Friend WithEvents RedBlockButton As System.Windows.Forms.Button
    Friend WithEvents GreenBlockButton As System.Windows.Forms.Button
    Friend WithEvents YellowBlockButton As System.Windows.Forms.Button
    Friend WithEvents OrangeBlockButton As System.Windows.Forms.Button
    Friend WithEvents PurpleBlockButton As System.Windows.Forms.Button
    Friend WithEvents BrownBlockButton As System.Windows.Forms.Button
    Friend WithEvents PinkBlockButton As System.Windows.Forms.Button
    Friend WithEvents MouseTool As System.Windows.Forms.Button
    Friend WithEvents EraserTool As System.Windows.Forms.Button
    Friend WithEvents MoveTool As System.Windows.Forms.Button
    Friend WithEvents EmptyTool As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LevelIndexTextBox As System.Windows.Forms.TextBox

End Class
