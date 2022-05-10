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
boardCreation: BOARD place SEMI ;
playerPlacement: PLAYER place SEMI | PLAYER NAME_T EQUALS ID place SEMI ;
monsterPlacement: MONSTER typeName  place COMMA SEMI | BOARD typeName NAME_T EQUALS ID SEMI place;
trapPlacement: TRAP typeName  place COMMA SEMI | BOARD typeName NAME_T EQUALS ID place SEMI;

SEMI: ';';
COMMA: ',';
COORDINATE: [0-9]+;
BOARD: 'board';
PLAYER: 'player';
NAME_T: 'name';
MONSTER: 'monster';
TRAP: 'trap';
EQUALS: '=';
ID: [a-zA-Z][a-zA-Z0-9_]* ;
WS: (' ' | '\t' | '\n' | '\r') -> skip;
