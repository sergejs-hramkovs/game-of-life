using GameOfLife;

Engine engine = new Engine();
RulesApplier applier = new RulesApplier();
Render render = new Render(engine, applier);
FileIO file = new FileIO();
Field field = new Field();

engine.StartGame(render, file, field);
engine.RunGame();