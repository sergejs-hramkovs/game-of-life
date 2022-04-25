using GameOfLife;

Engine engine = new();
RulesApplier applier = new();
Library library = new();
Render render = new();
FileIO file = new();
Field field = new();

engine.StartGame(render, file, field, library, applier, engine);
engine.RunGame();