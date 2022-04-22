using GameOfLife;

Engine engine = new Engine();
RulesApplier applier = new RulesApplier();
Library library = new Library();
Render render = new Render(engine, applier, library);
FileIO file = new FileIO();
Field field = new Field();

engine.StartGame(render, file, field, library);
engine.RunGame();