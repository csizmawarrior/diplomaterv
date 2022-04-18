grammar BoardGrammar;

program: statementList;
statementList: boardCreation statement*;
statement: playerPlacement
         | monsterPlacement
         | trapPlacement
         ;

typeName: ID;
place: x COMMA y;

x: COORDINATE;
y: COORDINATE;
boardCreation: BOARD  place SEMI;
playerPlacement: PLAYER place SEMI ;
monsterPlacement: MONSTER place COMMA typeName SEMI ;
trapPlacement: TRAP place COMMA typeName SEMI ;

SEMI: ';';
COMMA: ',';
COORDINATE: [0-9]+;
BOARD: 'board';
PLAYER: 'player';
MONSTER: 'monster';
TRAP: 'trap';
ID: [a-zA-Z][a-zA-Z0-9_]* ;
WS: (' ' | '\t' | '\n' | '\r') -> skip;
