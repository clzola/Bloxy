Public Class MapDebug
    Public Sub Render(A(,) As PuzzleBlockRectangle)
        Me.RichTextBox1.Clear()
        For I As Integer = 0 To 24 Step 1
            For J As Integer = 0 To 15 Step 1
                Dim PBR As PuzzleBlockRectangle = A(I, J)
                If PBR IsNot Nothing Then
                    RichTextBox1.Text = RichTextBox1.Text & " " & PBR.Parent.Id
                Else
                    RichTextBox1.Text = RichTextBox1.Text & " 0"
                End If
            Next
            RichTextBox1.Text = RichTextBox1.Text & vbNewLine
        Next
    End Sub
End Class