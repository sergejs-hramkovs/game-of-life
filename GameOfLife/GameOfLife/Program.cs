using GameOfLife;

Engine engine = new();
RulesApplier applier = new();
Library library = new();
Render render = new();
FileIO file = new();
InputController processor = new();
FieldOperations field = new(library, render, processor);

engine.StartGame(render, file, field, library, applier, engine, processor);
engine.RunGame();