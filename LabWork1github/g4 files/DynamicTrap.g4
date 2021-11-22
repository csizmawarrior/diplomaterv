grammar DynamicTrap;


definition: statementList* ;
name: ID;

statementList: nameDeclaration statement*;
statement: damageDeclaration ';'
        | moveDeclaration ';'
		| ifexpression ';'
		| whileexpression ';'
		| damageAmountDeclaration ';' 
		| healAmountDeclaration ';'
		| teleportDeclare ';'
		| spawnDeclare ';'
        ;


nameDeclaration: NAME_T EQUALS name ';' ;
damageAmountDeclaration: DAMAGE EQUALS NUMBER;
healAmountDeclaration: HEAL EQUALS NUMBER;
teleportDeclare: TELEPORT_PLACE EQUALS place;
spawnDeclare: SPAWN_PLACE EQUALS place;
damageDeclaration: DAMAGE DIRECTION | DAMAGE DIRECTION damageAmountDeclaration | DAMAGE DIRECTION distanceDeclare | DAMAGE DIRECTION distanceDeclare damageAmountDeclaration
					| DAMAGE RANDOM	| DAMAGE TO place | DAMAGE TO place damageAmountDeclaration | DAMAGE TO character | DAMAGE TO character damageAmountDeclaration ;
healDeclaration: HEAL DIRECTION | HEAL DIRECTION healAmountDeclaration | HEAL DIRECTION distanceDeclare | HEAL DIRECTION distanceDeclare healAmountDeclaration
					| HEAL RANDOM	| HEAL TO place | HEAL TO place healAmountDeclaration | HEAL TO character | HEAL TO character healAmountDeclaration ;
spawnDeclaration: SPAWN MONSTER name TO place | SPAWN RANDOM | SPAWN MONSTER name;
teleport: TELEPORT_T character TO place | TELEPORT_T character RANDOM | TELEPORT_T;
moveDeclaration: MOVE DIRECTION | MOVE TO place | MOVE DIRECTION distanceDeclare | MOVE TO PLAYER | MOVE TO RANDOM ;

distanceDeclare: DISTANCE EQUALS NUMBER;
ifexpression: IF PARENTHESISSTART expression PARENTHESISCLOSE block;
whileexpression: WHILE PARENTHESISSTART expression PARENTHESISCLOSE block;

block: BRACKETCLOSE statement* BRACKETCLOSE;
character: PLAYER | ME | MONSTER | TRAP;
possibleAttributes: HEALTH | PLACE_T ATTRIBUTE X | PLACE_T ATTRIBUTE Y | DAMAGE | HEAL 
					| TELEPORT_PLACE ATTRIBUTE X | TELEPORT_PLACE ATTRIBUTE Y | SPAWN_PLACE ATTRIBUTE X | SPAWN_PLACE ATTRIBUTE Y; 


place: x ',' y;
x: NUMBER;
y: NUMBER;

expression: expression operation expression | PARENTHESISSTART expression PARENTHESISCLOSE | ABSOLUTE expression ABSOLUTE | something | NEGATE expression;
something: character | NUMBER | ROUND | possibleAttributes | NOTHING;

operation: ATTRIBUTE | NUMCONNECTER | BOOLCONNECTER | COMPARE | ALIVE | IS NEAR;

TELEPORT_PLACE: 'teleport_place';
SPAWN_PLACE: 'spawn_place';
DISTANCE: 'distance';
DAMAGE: 'damage';
DIRECTION: 'F' | 'L' | 'R' | 'B';
IN: 'in';
TRAP: 'trap';
MONSTER: 'monster';
ROUND: 'round';
ME: 'me';
IF: 'if';
RANDOM: 'random';
TO: 'to';
PLACE_T: 'place';
NEAR: 'near';
IS: 'is';
ON: 'ON';
WHILE: 'while';
HEALTH: 'HP';
ALIVE: 'alive';
MOVE: 'move';
SPAWN: 'spawn';
TELEPORT_T: 'teleport';
HEAL: 'heal';
NAME_T: 'name';
PLAYER: 'player';
EFFECT_T: 'effect';

EQUALS: '=' ;
ABSOLUTE: '|';
EXPRESSIONCONNECTER: '||' | '&&' ;
COMPARE: '<' | '>' | '==' | '!=' ;
NUMOPERATION: '+' | '-' | '*' | '/' | '%' ;
PARENTHESISSTART: '(';
PARENTHESISCLOSE: ')';
BRACKETCLOSE: '}';
BRACKETSTART: '{';
COLON: ':';
SEMI: ';';
ATTRIBUTE: '.';
COMMA: ',';
NUMBER: [0-9]+;
ID: [a-zA-Z][a-zA-Z0-9_]* ;
WS: (' ' | '\t' | '\n' | '\r') -> skip;
