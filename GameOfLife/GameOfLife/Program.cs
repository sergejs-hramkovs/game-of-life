using GameOfLife;

Engine engine = new();
RulesApplier applier = new();
Library library = new();
Render render = new();
FileIO file = new();
InputProcessor processor = new();
FieldOperations field = new(library, engine, applier, render, processor);


engine.StartGame(render, file, field, library, applier, engine, processor);
engine.RunGame();