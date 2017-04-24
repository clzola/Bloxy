Namespace Bloxy
    Namespace Levels
        Public Class LevelManager2
            Private mCount As Integer
            Private mLevels As List(Of Level2)
            Private mLvlDirectory As IO.DirectoryInfo
            Private mCurrentLevel As Level2
            Private Shared SharedInstance As LevelManager2 = New LevelManager2

            Public Shared ReadOnly Property Instance As LevelManager2
                Get
                    Return LevelManager2.SharedInstance
                End Get
            End Property

            Public ReadOnly Property Count As Integer
                Get
                    Return Me.mCount
                End Get
            End Property

            Public ReadOnly Property Levels As List(Of Level2)
                Get
                    Return Me.mLevels
                End Get
            End Property

            Public Property CurrentLevel As Level2
                Get
                    Return Me.mCurrentLevel
                End Get
                Set(value As Level2)
                    Me.mCurrentLevel = value
                End Set
            End Property

            Private Sub New()
                Me.mCount = 0
                Me.mLevels = New List(Of Level2)
                Me.mLvlDirectory = New IO.DirectoryInfo("Levels")
                Me.mCurrentLevel = Nothing
            End Sub

            Public Sub LoadLevels()
                Dim LvlFileInfos = Me.mLvlDirectory.GetFiles()
                For Each LvlFileInfo As IO.FileInfo In LvlFileInfos
                    Dim Json As String = IO.File.ReadAllText(LvlFileInfo.FullName)
                    Dim NextLevel As Level2 = New Level2(LvlFileInfo.FullName, Json)
                    Me.mLevels.Add(NextLevel)
                Next

                Me.mLevels.Sort()
                Dim PrevLevel As Level2 = Nothing
                For Each Lvl As Level2 In Me.mLevels
                    If PrevLevel IsNot Nothing Then
                        PrevLevel.NextLevel = Lvl
                    End If

                    PrevLevel = Lvl
                Next
            End Sub
        End Class
    End Namespace
End Namespace

