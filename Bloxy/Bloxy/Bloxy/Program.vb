#If WINDOWS Or XBOX Then
Imports Newtonsoft.Json

Module Program
    ''' <summary>
    ''' The main entry point for the application.
    ''' </summary>
    Sub Main(ByVal args As String())
        Using game As New Bloxy.BloxyGame()
            game.Run()
        End Using
    End Sub
End Module

#End If
