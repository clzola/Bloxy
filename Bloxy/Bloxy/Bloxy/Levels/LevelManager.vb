Namespace Bloxy
    Namespace Levels
        Public Class LevelManager
            Public NumberOfLevels As Integer
            Public CurrentLevel As Level
            Private NextLevelIndex As Integer
            Private NextLevelFilename As String
            Private BlockIdDictionary As Dictionary(Of String, Integer)

            Private Shared SharedInstance As LevelManager = New LevelManager()

            Public Shared ReadOnly Property Instance As LevelManager
                Get
                    Return SharedInstance
                End Get
            End Property

            Private Sub New()
                NumberOfLevels = My.Computer.FileSystem.GetFiles("Levels").Count

                BlockIdDictionary = New Dictionary(Of String, Integer)

                BlockIdDictionary.Add("blocks\blue", 2)
                BlockIdDictionary.Add("blocks\red", 3)
                BlockIdDictionary.Add("blocks\green", 4)
                BlockIdDictionary.Add("blocks\yellow", 5)
                BlockIdDictionary.Add("blocks\pink", 6)
                BlockIdDictionary.Add("blocks\purple", 7)
                BlockIdDictionary.Add("blocks\orange", 8)
                BlockIdDictionary.Add("blocks\cyan", 9)
                BlockIdDictionary.Add("blocks\brown", 10)
            End Sub

            Sub SetNextLevel(LvlFilename As String)
                Me.NextLevelFilename = LvlFilename
            End Sub

            Public Function GetBlockMapId(Key As String) As Integer
                Dim Id As Integer
                BlockIdDictionary.TryGetValue(Key, Id)
                Return Id
            End Function

            Public Function GetNextLevel() As Level
                Dim Lvl As Level = New Level(NextLevelFilename)
                Lvl.LoadLevel(NextLevelFilename)
                CurrentLevel = Lvl
                Return Lvl
            End Function
        End Class
    End Namespace
End Namespace

