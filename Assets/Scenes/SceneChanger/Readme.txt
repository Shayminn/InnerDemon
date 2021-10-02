Scene Changer

Developed by Ryan Leung (c) 2021

-------------
-------------

Instructions: 
	
	Setup: 
		- File -> Build Settings -> Drag & Drop "DontDestroyOnLoad" scene as the first scene
		- Type in your first scene name in the ScreenChanger prefabs' script "Scene Changer" (FirstSceneName)
	
	Usage:
		- SceneChanger.Instance.ChangeScene(scene index) or ChangeScene(scene name) -> To switch scene
		- SceneChanger.Instance.ReloadScene() -> Reload current scene

	Extra:
		- Comment out [InitializeOnLoad] in StartingSceneOnPlay to stop the editor from starting in the DontDestroyOnLoad scene

		- Add DontDestroyOnLoad script to other game objects that may need it

-------------
-------------

Any bugs or suggestions, send me an email at cygnusdoodle@gmail.com

