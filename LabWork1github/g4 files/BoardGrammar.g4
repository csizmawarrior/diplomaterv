grammar BoardGrammar;

program: statementList;
statementList: boardCreation statement*;
statement: playerPlacement
         | monsterPlacement
         | trapPlacement
         ;
//TODO: szimbólumtábla
typeName: ID;
place: x COMMA y;

x: COORDINATE;
y: COORDINATE;
boardCreation: BOARD (nameDeclaration)? place SEMI ;
playerPlacement: PLAYER place SEMI | PLAYER NAME_T EQUALS ID place SEMI ;
monsterPlacement: MONSTER  typeName (nameDeclaration)? (COMMA partnerDeclaration)? place SEMI;
trapPlacement: TRAP  typeName (nameDeclaration)? (COMMA partnerDeclaration)? place SEMI;
nameDeclaration: NAME_T EQUALS ID;
partnerDeclaration: PARTNER EQUALS ID;

SEMI: ';';
COMMA: ',';
COORDINATE: [0-9]+;
BOARD: 'board';
PARTNER: 'partner';
PLAYER: 'player';
NAME_T: 'name';
MONSTER: 'monster';
TRAP: 'trap';
EQUALS: '=';
ID: [a-zA-Z][a-zA-Z0-9_]* ;
WS: (' ' | '\t' | '\n' | '\r') -> skip;
