#pragma strict

// EXAMPLE WITH CAMERA UPSIDEDOWN
function OnPreCull () {
	camera.ResetWorldToCameraMatrix ();
	camera.ResetProjectionMatrix ();
	camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(Vector3 (1, -1, 1));
}
 
function OnPreRender () {
	GL.SetRevertBackfacing (true);
}
 
function OnPostRender () {
	GL.SetRevertBackfacing (false);
}
 
@script RequireComponent (Camera)