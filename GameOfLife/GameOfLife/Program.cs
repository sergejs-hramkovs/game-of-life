using GameOfLife;

Engine engine = new Engine();
RulesApplier applier = new RulesApplier();
Render render = new Render(engine, applier);
FileIO file = new FileIO();

engine.StartGame(render, file);

Field field = new Field(engine.Length, engine.Width);

engine.RunGame(field);