Namespace Bloxy
    Public Class BloxyGame
        Inherits Microsoft.Xna.Framework.Game

        Public Shared ReadOnly Width As Integer = 480
        Public Shared ReadOnly Height As Integer = 810

        Private WithEvents graphics As GraphicsDeviceManager
        Private WithEvents spriteBatch As SpriteBatch

        Private TexManager As Resource.TextureManager
        Private SndManager As Resource.SoundManager
        Private GmStateManager As GameStates.GameStateManager
        Private LvlManager As Levels.LevelManager
        Private FntManager As Resource.FontManager
        Private LvlManager2 As Levels.LevelManager2

        Public Sub New()
            graphics = New GraphicsDeviceManager(Me)
            Content.RootDirectory = "Content"

            Me.graphics.PreferredBackBufferWidth = BloxyGame.Width
            Me.graphics.PreferredBackBufferHeight = BloxyGame.Height

            Me.TexManager = Resource.TextureManager.Instance
            Me.TexManager.SetContentManager(Me.Content)
            Me.TexManager.SetGameReference(Me)

            Me.SndManager = Resource.SoundManager.Instance
            Me.SndManager.SetContentManager(Me.Content)
            Me.SndManager.SetGameReference(Me)

            Me.GmStateManager = GameStates.GameStateManager.Instance

            'Me.LvlManager = Levels.LevelManager.Instance
            Me.LvlManager2 = Levels.LevelManager2.Instance

            Me.FntManager = Resource.FontManager.Instance
            Me.FntManager.SetGameReference(Me)
            Me.FntManager.SetContentManager(Me.Content)

            ' Set FPS to 60
            Me.TargetElapsedTime = TimeSpan.FromSeconds(1.0F / 60.0F)
            Me.IsFixedTimeStep = True
        End Sub

        Protected Overrides Sub Initialize()
            MyBase.Initialize()
            Me.IsMouseVisible = True

            Me.GmStateManager.Register(New GameStates.SplashGameState())
            Me.GmStateManager.Register(New GameStates.MenuGameState())
            Me.GmStateManager.Register(New GameStates.HelpGameState())
            Me.GmStateManager.Register(New GameStates.LevelsGameState())
            Me.GmStateManager.Register(New GameStates.LevelGameState())
            Me.GmStateManager.Initialize(Me)
            Me.GmStateManager.Enter(GameStates.GameState.Splash)
        End Sub

        Protected Overrides Sub LoadContent()
            spriteBatch = New SpriteBatch(GraphicsDevice)

            TexManager.LoadTextureFromFile("images\background")
            TexManager.LoadTextureFromFile("images\bar")
            TexManager.LoadTextureFromFile("images\splash")
            TexManager.LoadTextureFromFile("images\help1")
            TexManager.LoadTextureFromFile("images\help2")
            TexManager.LoadTextureFromFile("images\toast")

            TexManager.LoadTextureFromFile("buttons\btnplay")
            TexManager.LoadTextureFromFile("buttons\btnplay_hover")
            TexManager.LoadTextureFromFile("buttons\btnhelp")
            TexManager.LoadTextureFromFile("buttons\btnhelp_hover")
            TexManager.LoadTextureFromFile("buttons\btnquit")
            TexManager.LoadTextureFromFile("buttons\btnquit_hover")

            TexManager.LoadTextureFromFile("buttons\rarrow")
            TexManager.LoadTextureFromFile("buttons\rarrow_glow")
            TexManager.LoadTextureFromFile("buttons\larrow")
            TexManager.LoadTextureFromFile("buttons\larrow_glow")
            TexManager.LoadTextureFromFile("buttons\menu")
            TexManager.LoadTextureFromFile("buttons\menu_glow")
            TexManager.LoadTextureFromFile("buttons\reset")
            TexManager.LoadTextureFromFile("buttons\reset_glow")

            TexManager.LoadTextureFromFile("blocks\blue")
            TexManager.LoadTextureFromFile("blocks\cyan")
            TexManager.LoadTextureFromFile("blocks\yellow")
            TexManager.LoadTextureFromFile("blocks\red")
            TexManager.LoadTextureFromFile("blocks\brown")
            TexManager.LoadTextureFromFile("blocks\green")
            TexManager.LoadTextureFromFile("blocks\pink")
            TexManager.LoadTextureFromFile("blocks\purple")
            TexManager.LoadTextureFromFile("blocks\orange")

            SndManager.LoadSoundFromFile("sounds\btnclck")
            SndManager.LoadSoundFromFile("sounds\btnhover")

            FntManager.LoadFontFromFile("fonts\lvlbuttons")

            Dim BlackPixelTexture As Texture2D = New Texture2D(GraphicsDevice, 1, 1)
            BlackPixelTexture.SetData(New Color() {Color.Black})
            TexManager.LoadTextureFromMemory("static\blackpixel", BlackPixelTexture)

            Dim WhitePixelTexture As Texture2D = New Texture2D(GraphicsDevice, 1, 1)
            WhitePixelTexture.SetData(New Color() {Color.White})
            TexManager.LoadTextureFromMemory("static\whitepixel", WhitePixelTexture)

            Dim LevelBodyPixelTexture As Texture2D = New Texture2D(GraphicsDevice, 1, 1)
            LevelBodyPixelTexture.SetData(New Color() {Color.White * 0.25F})
            TexManager.LoadTextureFromMemory("static\levelbody", LevelBodyPixelTexture)

            Dim LevelButtonPixelTexture As Texture2D = New Texture2D(GraphicsDevice, 1, 1)
            LevelButtonPixelTexture.SetData(New Color() {Color.LightSkyBlue})
            TexManager.LoadTextureFromMemory("static\btnlevel_back", LevelButtonPixelTexture)

            Dim LevelButtonPixelHoverTexture As Texture2D = New Texture2D(GraphicsDevice, 1, 1)
            LevelBodyPixelTexture.SetData(New Color() {Color.White * 0.25F})
            TexManager.LoadTextureFromMemory("static\btnlevel_hover", LevelButtonPixelHoverTexture)

            Dim LevelButtonPixelBorderTexture As Texture2D = New Texture2D(GraphicsDevice, 1, 1)
            LevelButtonPixelHoverTexture.SetData(New Color() {Color.White})
            TexManager.LoadTextureFromMemory("static\btnlevel_border", LevelButtonPixelHoverTexture)

            Dim LevelButtonPixelFinishedTexture As Texture2D = New Texture2D(GraphicsDevice, 1, 1)
            LevelButtonPixelFinishedTexture.SetData(New Color() {Color.CadetBlue})
            TexManager.LoadTextureFromMemory("static\btnlevel_finished", LevelButtonPixelFinishedTexture)

            Me.LvlManager2.LoadLevels()
        End Sub

        Protected Overrides Sub UnloadContent()
            ' Unload any non ContentManager content here
            If Levels.LevelManager2.Instance.CurrentLevel IsNot Nothing Then
                Levels.LevelManager2.Instance.CurrentLevel.Save()
            End If
        End Sub

        Protected Overrides Sub Update(ByVal gameTime As GameTime)
            MyBase.Update(gameTime)
            GmStateManager.Update(gameTime)
        End Sub

        Protected Overrides Sub Draw(ByVal gameTime As GameTime)
            GraphicsDevice.Clear(Color.CornflowerBlue)

            MyBase.Draw(gameTime)
            GmStateManager.Draw(Me.spriteBatch, gameTime)
        End Sub

    End Class
End Namespace

