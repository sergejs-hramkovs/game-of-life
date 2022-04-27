using GameOfLife;

Engine engine = new();
RulesApplier applier = new();
Library library = new();
Render render = new();
FileIO file = new();
Field field = new();
InputProcessor processor = new(engine, file, render);

engine.StartGame(render, file, field, library, applier, engine, processor);
engine.RunGame();