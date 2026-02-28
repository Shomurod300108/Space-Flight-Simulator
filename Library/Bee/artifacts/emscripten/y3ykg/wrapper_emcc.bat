
@echo off
"C:\Program Files\Unity\Hub\Editor\2021.3.45f2\Editor\Data\PlaybackEngines\WebGLSupport\BuildTools\Emscripten\emscripten\emcc.bat" %* < nul
exit %ERRORLEVEL%
