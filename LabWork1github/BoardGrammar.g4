grammar BoardGrammar;

program: statementList;
statementList: statement*;
statement: boardCreation
        | playerPlacement
        | monsterPlacement
        | trapPlacement
        ;

typeName: ID;
place: x ',' y;

x: COORDINATE;
y: COORDINATE;
boardCreation: BOARD  place ';';
playerPlacement: PLAYER place ';' ;
monsterPlacement: MOSNTER place typeName ';' ;
trapPlacement: TRAP place typeName ';' ;


SEMI: ';';
COMMA: ',';
COORDINATE: [0-9]+;

BOARD: 'board';
PLAYER: 'player';
MONSTER: 'monster';
TRAP: 'trap';
ID: [a-zA-Z][a-zA-Z0-9_]* ;
WS: (' ' | '\t' | '\n' | '\r') -> skip;
